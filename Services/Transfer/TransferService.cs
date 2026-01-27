using CoinGecko.Net.Clients;
using CoinGecko.Net.Interfaces;
using CryptoExchange.Net.Interfaces.Clients;
using Microsoft.VisualBasic;
using Models.Request;
using Models.Response;
using Providers.Account;
using System.Globalization;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Services.Transfer
{
    public class TransferService : ITransferService
    {
        private readonly ICoinGeckoRestClient _restClient;
        private readonly IAccountProvider _accountProvider;

        private static readonly Dictionary<string, string> CurrencyMap =
            new(StringComparer.OrdinalIgnoreCase)
        {
            // Fiat
            { "usd", "usd" },
            { "dollar", "usd" },
            { "eur", "eur" },
            { "euro", "eur" },
            { "php", "php" },
            { "peso", "php" },
            { "jpy", "jpy" },
            { "yen", "jpy" },
            { "gbp", "gbp" },
            { "pound", "gbp" },
            { "sgd", "sgd" },
            { "aud", "aud" },
            { "cad", "cad" },
            { "chf", "chf" },
            { "cny", "cny" },

            // Crypto
            { "btc", "bitcoin" },
            { "eth", "ethereum" },
            { "usdt", "tether" },
            { "bnb", "binancecoin"},
            { "xrp", "ripple"},
            { "usdc", "usd-coin" },
            { "sol", "solana" },
            { "trx", "tron" },
            { "steth", "staked-ether" },
            { "doge", "dogecoin" },
            { "figr_heloc", "figure-heloc" },
            { "ada", "cardano" },
            { "wsteth", "wrapped-steth" },
        };

        public TransferService(ICoinGeckoRestClient restClient, IAccountProvider accountProvider)
        {
            _restClient = restClient;
            _accountProvider = accountProvider;
        }

        public async Task<decimal?> Conversion(ConversionRequest request)
        {
            var from = request.From.ToLower();          // CoinGecko ID: e.g., "bitcoin"
            var to = request.To.ToLower();            // Target currency: "usd", "btc", "eth", etc.

            if(!CurrencyMap.TryGetValue(from, out var id))
               throw new ArgumentException($"Unsupported currency: {from}");

            var result = await _restClient.Api.GetPricesAsync(
                ids: new[] { id },
                quoteAssets: new[] { to }
            );

            if (!result.Success || result.Data == null)
                return null;

            if (!result.Data.TryGetValue(id, out var quotes))
                return null;

            if (!quotes.TryGetValue(to, out var price))
                return null;

            return price;
        }

        public async Task<object> GetMarket(string currency)
        {
            var market = await _restClient.Api.GetMarketsAsync(currency);
            return market;
        }

        public async Task<CryptoToFiatResponse> CryptoToFiat(string userId, CryptoToFiatRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Asset))
                throw new ArgumentException("Asset is required");

            if (string.IsNullOrWhiteSpace(request.Account))
                throw new ArgumentException("Fiat currency is required");

            var url = $"https://api.coinpaprika.com/v1/tickers/{request.Asset}?quotes={request.Account.ToUpper()}";

            using var http = new HttpClient();

            var response = await http.GetAsync(url);
            if (!response.IsSuccessStatusCode)
                throw new Exception("Failed to fetch price from CoinPaprika");

            var json = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(json);
            var price = doc
                .RootElement
                .GetProperty("quotes")
                .GetProperty(request.Account.ToUpper())
                .GetProperty("price")
                .GetDecimal();
            
            var convertedAmount = request.Amount * price;

            // Get Wallet (crypto)
            string symbol = request.Asset.Split('-')[0].ToUpper();
            var userWallet = await _accountProvider.GetAccountByIdAndCurrencyCode(userId, request.WalletId, symbol);
            if (userWallet == null)
                throw new Exception("Wallet not found");

            // Get Bank (fiat)
            var userBank = await _accountProvider.GetAccountByIdAndCurrencyCode(userId, request.BankId, request.Account);
            if (userBank == null)
                throw new Exception("Bank account not found");

            // Deduct crypto
            userWallet.Balance -= request.Amount;
            await _accountProvider.UpdateAmount(userId, userWallet.Id, userWallet.Balance);

            // Add fiat
            userBank.Balance += convertedAmount;
            await _accountProvider.UpdateAmount(userId, userBank.Id, userBank.Balance);

            return new CryptoToFiatResponse
            {
                ConvertedAmount = convertedAmount,
                Currency = userBank.CurrencyCode,
                NewBankBalance = userBank.Balance,
                BankAccountId = userBank.Id
            };
        }

        public async Task<FiatToCryptoResponse> FiatToCrypto(string userId, FiatToCryptoRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Asset))
                throw new ArgumentException("Asset is required");

            if (string.IsNullOrWhiteSpace(request.Account))
                throw new ArgumentException("Fiat currency is required");

            var url = $"https://api.coinpaprika.com/v1/tickers/{request.Asset}?quotes={request.Account.ToUpper()}";

            using var http = new HttpClient();

            var response = await http.GetAsync(url);
            if (!response.IsSuccessStatusCode)
                throw new Exception("Failed to fetch price from CoinPaprika");

            var json = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(json);
            var price = doc
                .RootElement
                .GetProperty("quotes")
                .GetProperty(request.Account.ToUpper())
                .GetProperty("price")
                .GetDecimal();

            var cryptoAmount = request.Amount / price;

            // Get Bank (fiat)
            var userBank = await _accountProvider.GetAccountByIdAndCurrencyCode(userId, request.BankId, request.Account);
            if (userBank == null)
                throw new Exception("Bank account not found");

            // Get Wallet (crypto)
            string symbol = request.Asset.Split('-')[0].ToUpper();
            var userWallet = await _accountProvider.GetAccountByIdAndCurrencyCode(userId, request.WalletId, symbol);
            if (userWallet == null)
                throw new Exception("Wallet not found");

            // Deduct fiat
            userBank.Balance -= request.Amount;
            await _accountProvider.UpdateAmount(userId, userBank.Id, userBank.Balance);

            // Add crypto
            userWallet.Balance += cryptoAmount;
            await _accountProvider.UpdateAmount(userId, userWallet.Id, userWallet.Balance);

            return new FiatToCryptoResponse
            {
                ConvertedAmount = cryptoAmount,
                Currency = userWallet.CurrencyCode,
                NewWalletBalance = userWallet.Balance,
                WalletId = userWallet.Id
            };
        }

        public async Task<CryptoToCryptoResponse> CryptoToCrypto(string userId, CryptoToCryptoRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.FromAsset))
                throw new ArgumentException("From Asset is required");

            if (string.IsNullOrWhiteSpace(request.ToAsset))
                throw new ArgumentException("To Asset is required");

            var url = $"https://api.freecryptoapi.com/v1/getData?symbol={request.FromAsset}+{request.ToAsset}";

            using var http = new HttpClient();
            http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", "8cutnx4jhrrqgsdsunyf");

            var response = await http.GetAsync(url);
            if (!response.IsSuccessStatusCode)
                throw new Exception("Failed to fetch price");

            var json = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);

            var symbols = doc.RootElement.GetProperty("symbols");

            var from = symbols
                .EnumerateArray()
                .First(x => x.GetProperty("symbol").GetString() == request.FromAsset.ToUpper());

            var to = symbols
                .EnumerateArray()
                .First(x => x.GetProperty("symbol").GetString() == request.ToAsset.ToUpper());

            decimal fromBtc = decimal.Parse(
                from.GetProperty("last").GetString(),
                CultureInfo.InvariantCulture
            );

            decimal toBtc = decimal.Parse(
                to.GetProperty("last").GetString(),
                CultureInfo.InvariantCulture
            );

            decimal convertedAmount = request.Amount * fromBtc / toBtc;

            // Wallet updates
            var fromWallet = await _accountProvider.GetAccountByIdAndCurrencyCode(
                userId, request.FromWalletId, request.FromAsset
            );
            if (fromWallet == null)
                throw new Exception("From wallet not found");

            if (fromWallet.Balance < request.Amount)
                throw new Exception("Insufficient balance");

            var toWallet = await _accountProvider.GetAccountByIdAndCurrencyCode(
                userId, request.ToWalletId, request.ToAsset
            );
            if (toWallet == null)
                throw new Exception("To wallet not found");

            fromWallet.Balance -= request.Amount;
            toWallet.Balance += convertedAmount;

            await _accountProvider.UpdateAmount(userId, fromWallet.Id, fromWallet.Balance);
            await _accountProvider.UpdateAmount(userId, toWallet.Id, toWallet.Balance);

            return new CryptoToCryptoResponse
            {
                ConvertedAmount = convertedAmount,
                FromAsset = request.FromAsset,
                ToAsset = request.ToAsset,
                NewFromBalance = fromWallet.Balance,
                NewToBalance = toWallet.Balance
            };
        }
    }
}

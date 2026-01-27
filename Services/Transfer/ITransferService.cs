
using Models.Request;
using Models.Response;

namespace Services.Transfer
{
    public interface ITransferService
    {
        Task<decimal?> Conversion(ConversionRequest request);
        Task<CryptoToCryptoResponse> CryptoToCrypto(string userId, CryptoToCryptoRequest request);
        Task<CryptoToFiatResponse> CryptoToFiat(string userId, CryptoToFiatRequest request);
        Task<FiatToCryptoResponse> FiatToCrypto(string userId, FiatToCryptoRequest request);
        Task<object> GetMarket(string currency);
    }
}

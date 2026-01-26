
using Models.Request;
using Models.Response;

namespace Services.Transfer
{
    public interface ITransferService
    {
        Task<decimal?> Conversion(ConversionRequest request);
        Task<CryptoToFiatResponse> CryptoToFiat(string userId, CryptoToFiatRequest request);
        Task<object> GetMarket(string currency);
    }
}

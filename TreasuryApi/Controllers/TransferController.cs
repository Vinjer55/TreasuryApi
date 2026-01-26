using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Request;
using Services.Transfer;

namespace TreasuryApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TransferController : ControllerBase
    {
        private readonly ITransferService _transferService;
        public TransferController(ITransferService transferService)
        {
            _transferService = transferService;
        }

        [HttpPost]
        public async Task<IActionResult> Conversion(ConversionRequest request)
        {
            // Get user id from token (example: from "sub" or "userId" claim)
            var userId = User.FindFirst("UserId")?.Value;
            var corporationId = User.FindFirst("CorpId")?.Value;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized("User ID not found in token");

            if (!string.IsNullOrEmpty(userId))
            {
                var result = await _transferService.Conversion(request);
                return Ok(result);
            }

            return Unauthorized("Invalid authentication method.");
        }

        [HttpPost("Market")]
        public async Task<IActionResult> GetMarket([FromBody] string currency)
        {
            // Get user id from token (example: from "sub" or "userId" claim)
            var userId = User.FindFirst("UserId")?.Value;
            var corporationId = User.FindFirst("CorpId")?.Value;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized("User ID not found in token");

            if (!string.IsNullOrEmpty(userId))
            {
                var result = await _transferService.GetMarket(currency);
                return Ok(result);
            }

            return Unauthorized("Invalid authentication method.");
        }

        [HttpPost("Crypto-To-Fiat")]
        public async Task<IActionResult> CryptoToFiat(CryptoToFiatRequest request)
        {
            // Get user id from token (example: from "sub" or "userId" claim)
            var userId = User.FindFirst("UserId")?.Value;
            var corporationId = User.FindFirst("CorpId")?.Value;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized("User ID not found in token");

            if (!string.IsNullOrEmpty(userId))
            {
                var result = await _transferService.CryptoToFiat(userId, request);
                return Ok(result);
            }

            return Unauthorized("Invalid authentication method.");
        }

    }
}

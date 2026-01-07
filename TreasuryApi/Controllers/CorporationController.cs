using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Request;
using Services.Corporation;

namespace TreasuryApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CorporationController : ControllerBase
    {
        private readonly ICorporationService _corporationService;
        public CorporationController(ICorporationService corporationService)
        {
            _corporationService = corporationService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCorporation(CreateCorpRequest request)
        {
            // Get user id from token (example: from "sub" or "userId" claim)
            var userId = User.FindFirst("UserId")?.Value;
            var corporationId = User.FindFirst("CorpId")?.Value;

            if (!string.IsNullOrEmpty(corporationId))
            {
                return BadRequest("You already have a Corporation");
            }

            if (!string.IsNullOrEmpty(userId))
            {
                var corpId = await _corporationService.CreateCorp(userId, request);
                return Ok(corpId);
            }

            return Unauthorized("Invalid authentication method.");
        }

        [HttpGet("Users")]
        public async Task<IActionResult> GetOrgUsers()
        {
            // Get user id from token (example: from "sub" or "userId" claim)
            var userId = User.FindFirst("UserId")?.Value;
            var corporationId = User.FindFirst("CorpId")?.Value;

            if (string.IsNullOrEmpty(corporationId))
            {
                return BadRequest("You don't have corporation");
            }

            if (!string.IsNullOrEmpty(userId))
            {
                var users = await _corporationService.GetCorpUsers(corporationId);
                return Ok(users);
            }

            return Unauthorized("Invalid authentication method.");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCorp()
        {
            // Get user id from token (example: from "sub" or "userId" claim)
            var userId = User.FindFirst("UserId")?.Value;
            var corporationId = User.FindFirst("CorpId")?.Value;

            if (string.IsNullOrEmpty(corporationId))
            {
                return BadRequest("You don't have corporation");
            }

            if (!string.IsNullOrEmpty(userId))
            {
                await _corporationService.DeleteCorp(userId, corporationId);
                return Ok(new { Message = "Corporation deleted successfully" });
            }

            return Unauthorized("Invalid authentication method.");
        }
    }
}

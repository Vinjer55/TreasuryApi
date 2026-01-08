using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Request;
using Services.Account;

namespace TreasuryApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount(CreateAccountRequest request)
        {
            // Get user id from token (example: from "sub" or "userId" claim)
            var userId = User.FindFirst("UserId")?.Value;

            if (!string.IsNullOrEmpty(userId))
            {
                var accountId = await _accountService.CreateAccount(userId, request);
                return Ok(accountId);
            }

            return Unauthorized("Invalid authentication method.");
        }

        [HttpGet]
        public async Task<IActionResult> GetAccounts()
        {
            // Get user id from token (example: from "sub" or "userId" claim)
            var userId = User.FindFirst("UserId")?.Value;

            if (!string.IsNullOrEmpty(userId))
            {
                var result = await _accountService.GetAccounts(userId);
                return Ok(result);
            }

            return Unauthorized("Invalid authentication method.");

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAccount(int id, UpdateAccount card)
        {
            // Get user id from token (example: from "sub" or "userId" claim)
            var userId = User.FindFirst("UserId")?.Value;

            // Validate Id
            if (id == null)
                return BadRequest("Id is required");

            if (!string.IsNullOrEmpty(userId))
            {
                await _accountService.UpdateAccount(userId, id, card);
                return Ok("Account updated successfully");
            }

            return Unauthorized("Invalid authentication method.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            // Get user id from token (example: from "sub" or "userId" claim)
            var userId = User.FindFirst("UserId")?.Value;

            if (!string.IsNullOrEmpty(userId))
            {
                await _accountService.DeleteAccount(id);
                return Ok(new { Message = "Account deleted successfully." });
            }

            return Unauthorized("Invalid authentication method.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccountById(int id)
        {
            // Get user id from token (example: from "sub" or "userId" claim)
            var userId = User.FindFirst("UserId")?.Value;

            var result = await _accountService.GetAccountById(id);
            return Ok(result);
        }
    }
}

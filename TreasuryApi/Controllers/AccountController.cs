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
    }
}

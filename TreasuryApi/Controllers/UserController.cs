using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TreasuryApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        //get User Info
        [HttpGet("User")]
        public async Task<IActionResult> GetUser()
        {
            // Get user id from token (example: from "sub" or "userId" claim)
            var userId = User.FindFirst("userId")?.Value;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized("User ID not found in token");

            // Simulate successful login / fetch user data
            return Ok(new { UserId = userId });
        }
    }
}

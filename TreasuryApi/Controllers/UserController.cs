using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.DBModel;
using Models.Request;
using Services.User;

namespace TreasuryApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        //get User Info
        [HttpGet()]
        public async Task<IActionResult> GetUser()
        {
            // Get user id from token (example: from "sub" or "userId" claim)
            var userId = User.FindFirst("userId")?.Value;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized("User ID not found in token");

            AppUser user = await _userService.GetUserById(int.Parse(userId));
            user.Password = string.Empty;

            // Simulate successful login / fetch user data
            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUser request)
        {
            var userId = User.FindFirst("UserId")?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("User ID not found in token");

            if (id != int.Parse(userId))
                return Forbid("You can only update your own profile");

            await _userService.UpdateUser(id, request);
            return Ok(new { Message = "User updated successfully" });
        }
    }
}

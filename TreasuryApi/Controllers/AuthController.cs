using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Request;
using Services.User;
using TreasuryApi.Service.Auth;

namespace TreasuryApi.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;

        public AuthController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }
        // POST api/<AuthController>
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var token = await _authService.LoginAuth(request);

            // Simulate successful login
            return Ok(token);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> CreateUser([FromBody] RegisterRequest user)
        {
            var newUserId = await _userService.CreateUser(user);
            return Ok(newUserId);
        }
    }
}

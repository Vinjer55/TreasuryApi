using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using TreasuryApi.Service.Auth;

namespace TreasuryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }
        // POST api/<AuthController>
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var token = await authService.LoginAuth(request);

            // Simulate successful login
            return Ok(token);
        }
    }
}

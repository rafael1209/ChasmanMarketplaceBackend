using MarketplaceBackend.Interfaces;
using MarketplaceBackend.Models;
using MarketplaceBackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MarketplaceBackend.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController(IUserRepository userService) : ControllerBase
    {
        private readonly IUserRepository _userService = userService;

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLogin loginModel)
        {
            if (string.IsNullOrEmpty(loginModel.Email) || string.IsNullOrEmpty(loginModel.Password))
                return BadRequest(new { error = "Email and password are required." });

            try
            {
                var user = await _userService.ValidateUserCredentialsAsync(loginModel.Email, loginModel.Password);

                if (user == null)
                    return Unauthorized(new { error = "Invalid email or password." });

                return Ok(new
                {
                    Email = user.Email,
                    Username = user.Username
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error processing login.", details = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegister registerModel)
        {
            if (string.IsNullOrEmpty(registerModel.Email) || string.IsNullOrEmpty(registerModel.Password))
                return BadRequest(new { error = "Email and password are required." });

            try
            {
                var result = await _userService.RegisterUserAsync(registerModel.Email, registerModel.Password, registerModel.Username);
                if (!result.Success)
                    return BadRequest(new { error = result.ErrorMessage });

                return Ok(new
                {
                    Message = "User registered successfully.",
                    result.User
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error processing registration.", details = ex.Message });
            }
        }
    }
}

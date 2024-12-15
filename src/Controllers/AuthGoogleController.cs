using MarketplaceBackend.Interfaces;
using MarketplaceBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace MarketplaceBackend.Controllers;

[ApiController]
[Route("api/v1/auth/google")]
public class AuthGoogleController(IGoogleAuthService googleAuthService, UserService userService) : ControllerBase
{
    private readonly IGoogleAuthService _googleAuthService = googleAuthService;
    private readonly UserService _userService = userService;

    [HttpGet("url")]
    public IActionResult GetAuthUrl()
    {
        try
        {
            var url = _googleAuthService.GetGoogleAuthUrl();
            return Ok(new { url });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Error retrieving authorization URL.", details = ex.Message });
        }
    }

    [HttpGet("callback")]
    public async Task<IActionResult> GoogleCallback([FromQuery] string code)
    {
        if (string.IsNullOrEmpty(code))
            return BadRequest(new { error = "Authorization code is required." });

        try
        {
            var result = await _googleAuthService.HandleGoogleCallbackAsync(code);

            if (result == null)
                return BadRequest(new { error = "Invalid authorization code or failed to authenticate." });

            var user = await _userService.GetOrCreateUserByEmailAsync(result);

            return Ok(new { user });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Error processing authorization.", details = ex.Message });
        }
    }
}
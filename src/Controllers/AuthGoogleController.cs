using MarketplaceBackend.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MarketplaceBackend.Controllers;

[ApiController]
[Route("api/v1/auth/google")]
public class AuthGoogleController(IGoogleAuthService googleAuthService) : ControllerBase
{
    private readonly IGoogleAuthService _googleAuthService = googleAuthService;

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

            return Ok(new
            {
                result.AuthToken,
                result.Email,
                result.Username,
                result.AvatarUri
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Error processing authorization.", details = ex.Message });
        }
    }
}
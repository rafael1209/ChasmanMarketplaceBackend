using Google.Apis.Auth;
using MarketplaceBackend.DTOs;

namespace MarketplaceBackend.Interfaces
{
    public interface IGoogleAuthService
    {
        Uri GetGoogleAuthUrl();
        Task<GoogleJsonWebSignature.Payload?> HandleGoogleCallbackAsync(string code);
    }
}

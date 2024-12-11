using MarketplaceBackend.DTOs;

namespace MarketplaceBackend.Interfaces
{
    public interface IGoogleAuthService
    {
        Uri GetGoogleAuthUrl();
        Task<GoogleAuthResultDto?> HandleGoogleCallbackAsync(string code);
    }
}

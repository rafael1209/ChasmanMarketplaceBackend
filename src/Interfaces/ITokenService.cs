namespace MarketplaceBackend.Interfaces
{
    public interface ITokenService
    {
        string GenerateAccessToken(string userId, string email);

        string GenerateRefreshToken();
    }
}

namespace MarketplaceBackend.DTOs
{
    public class GoogleAuthResultDto
    {
        public string? AuthToken { get; set; }
        public string? Email { get; set; }
        public string? Username { get; set; }
        public string? AvatarUri { get; set; }
    }
}

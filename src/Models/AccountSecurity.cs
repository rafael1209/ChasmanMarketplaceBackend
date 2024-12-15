
using MongoDB.Bson.Serialization.Attributes;

namespace MarketplaceBackend.Models;

public class AccountSecurity
{
    [BsonElement("passwordHash")]
    public string? PasswordHash { get; set; }

    [BsonElement("authToken")]
    public string? AuthToken { get; set; }

    [BsonElement("refreshToken")]
    public string? RefreshToken { get; set; }

    [BsonElement("refreshTokenExpiryUtc")]
    public DateTime? RefreshTokenExpirationUtc { get; set; }

    [BsonElement("lastLogin")]
    public DateTime? LastLoginUtc { get; set; }
}
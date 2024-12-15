using MongoDB.Bson.Serialization.Attributes;

namespace MarketplaceBackend.Models;

public class ProfileInfo
{
    [BsonElement("profilePicture")]
    public string? ProfilePictureUrl { get; set; }
}
using MongoDB.Bson.Serialization.Attributes;

namespace MarketplaceBackend.Models;

public class UserPreferences
{
    [BsonElement("wishlist")]
    public List<string>? Wishlist { get; set; }
}
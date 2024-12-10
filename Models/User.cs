using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MarketplaceBackend.Models
{
    public class User
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("username")]
        public required string Username { get; set; }

        [BsonElement("email")]
        public required string Email { get; set; }

        [BsonElement("passwordHash")]
        public string? PasswordHash { get; set; }

        [BsonElement("firstName")]
        public required string FirstName { get; set; }

        [BsonElement("lastName")]
        public string? LastName { get; set; }

        [BsonElement("role")]
        public string Role { get; set; }

        [BsonElement("phoneNumber")]
        public string? PhoneNumber { get; set; }

        [BsonElement("createdAt")]
        public DateTime CreatedAtUtc { get; set; }

        [BsonElement("lastLogin")]
        public DateTime? LastLoginUtc { get; set; }

        [BsonElement("wishlist")]
        public List<string>? Wishlist { get; set; }

        [BsonElement("accountBalance")]
        public decimal AccountBalance { get; set; }

        [BsonElement("isActive")]
        public bool IsActive { get; set; }

        [BsonElement("isVerified")]
        public bool IsVerified { get; set; }

        [BsonElement("googleId")]
        public string GoogleId { get; set; }

        [BsonElement("facebookId")]
        public string FacebookId { get; set; }

        [BsonElement("profilePicture")]
        public string ProfilePictureUrl { get; set; }
    }
}

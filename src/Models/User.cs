using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MarketplaceBackend.Models
{
    public class User
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("email")]
        public required string Email { get; set; }

        [BsonElement("firstName")]
        public required string FirstName { get; set; }

        [BsonElement("lastName")]
        public required string LastName { get; set; }

        [BsonElement("createdAtUtc")]
        public DateTime CreatedAtUtc { get; set; }

        [BsonElement("personalInfo")]
        public PersonalInfo? PersonalData { get; set; }

        [BsonElement("profileInfo")]
        public ProfileInfo? ProfileData { get; set; }

        [BsonElement("accountSecurity")]
        public AccountSecurity? SecurityData { get; set; }

        [BsonElement("financialInfo")]
        public FinancialInfo? FinancialData { get; set; }

        [BsonElement("preferences")]
        public UserPreferences? PreferencesData { get; set; }
    }
}
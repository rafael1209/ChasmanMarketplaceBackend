
using MongoDB.Bson.Serialization.Attributes;

namespace MarketplaceBackend.Models;

public class FinancialInfo
{
    [BsonElement("accountBalance")]
    public decimal AccountBalance { get; set; }
}
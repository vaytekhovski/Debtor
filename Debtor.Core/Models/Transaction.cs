using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Debtor.Core.Models;

[BsonIgnoreExtraElements]
public class Transaction
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    [BsonElement("userFromId")]
    public string? UserFromId { get; set; }
    [BsonElement("userToI")]
    public string? UserToId { get; set; }
    [BsonElement("jointId")]
    public string? JointId { get; set; }
    [BsonElement("amount")]
    public decimal Amount { get; set; } = 0;
    [BsonElement("status")]
    public string Status { get; set; } = string.Empty;

}
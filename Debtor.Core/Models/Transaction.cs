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
    public User? UserFrom { get; set; }
    [BsonElement("userToI")]
    public string? UserToId { get; set; }
    public User? UserTo { get; set; }
    [BsonElement("jointId")]
    public string? JointId { get; set; }
    public Joint? Joint { get; set; }
    [BsonElement("amount")]
    public decimal Amount { get; set; } = 0;
    [BsonElement("description")]
    public string Description { get; set; } = string.Empty;
    [BsonElement("status")]
    public string Status { get; set; } = string.Empty;
    [BsonElement("type")]
    public string Type { get; set; } = string.Empty;
    [BsonElement("date")]
    public DateTime Date { get; set; } = new DateTime();

}
using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Debtor.Core.Models;

[BsonIgnoreExtraElements]
public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    [BsonElement("name")]
    public string Name { get; set; } = string.Empty;
    [BsonElement("transactions")]
    public List<Transaction> transactions = new List<Transaction>();
}
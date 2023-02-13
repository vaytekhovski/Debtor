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
    [BsonElement("login")]
    public string? Login { get; set; }
    [BsonElement("password")]
    public string? Password { get; set; }
    [BsonElement("transactions")]
    public List<Transaction> transactions = new List<Transaction>();
}
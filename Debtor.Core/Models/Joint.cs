using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Debtor.Core.Models;

[BsonIgnoreExtraElements]
public class Joint
{
    public Joint()
    {
        this.Id = ObjectId.GenerateNewId().ToString();
    }

    [BsonId]
    public string? Id { get; set; }
    [BsonElement("name")]
    public string Name { get; set; } = string.Empty;
    [BsonElement("description")]
    public string Description { get; set; } = string.Empty;
    [BsonElement("participants")]
    public List<User> Participants = new List<User>();
    [BsonElement("amount")]
    public decimal Amount { get; set; } = 0;

}
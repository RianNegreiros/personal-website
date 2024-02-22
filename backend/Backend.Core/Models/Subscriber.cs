using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Backend.Core.Models;

public class Subscriber
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("email")]
    public string Email { get; set; }

    [BsonElement("isSubscribed")]
    public bool IsSubscribed { get; set; } = true;
}

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Backend.Core.Models;

public class Comment
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    [BsonElement("content")]
    public string Content { get; set; }
    [BsonElement("author")]
    public string Author { get; set; }
    [BsonElement("postId")]
    public string PostId { get; set; }
    [BsonElement("createdAt")]
    public DateTime CreatedAt { get; set; }
}

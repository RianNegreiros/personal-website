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
    public User Author { get; set; }

    [BsonElement("postId")]
    public string PostId { get; set; }

    [BsonElement("postSlug")]
    public string PostSlug { get; set; }

    [BsonElement("replies")]
    public List<Comment> Replies { get; set; } = new List<Comment>();

    [BsonElement("createdAt")]
    [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
    public DateTime CreatedAt { get; set; }

    [BsonElement("updatedAt")]
    [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
    public DateTime UpdatedAt { get; set; }
}
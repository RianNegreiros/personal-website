using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Backend.Core.Models;

public class Post
{
  [BsonId]
  [BsonRepresentation(BsonType.ObjectId)]
  public string Id { get; set; }
  [BsonElement("title")]
  public string Title { get; set; }
  [BsonElement("summary")]
  public string Summary { get; set; }
  [BsonElement("content")]
  public string Content { get; set; }
  [BsonElement("author")]
  public User Author { get; set; }

  [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
  public DateTime CreatedAt { get; } = DateTime.UtcNow;

  [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
  public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

  [BsonElement("comments")]
  public List<Comment> Comments { get; set; } = new List<Comment>();
}
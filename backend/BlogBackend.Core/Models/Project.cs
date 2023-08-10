using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BlogBackend.Core.Models;

public class Project
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    [BsonElement("title")]
    public string Title { get; set; }
    [BsonElement("url")]
    public string Url { get; set; }
    [BsonElement("overview")]
    public string Overview { get; set; }
    [BsonElement("imageUrl")]
    public string ImageUrl { get; set; }
}
using MongoDB.Bson;

namespace backend.Core.Models;

public class User
{
    public ObjectId Id { get; set; }
        
    public string Username { get; set; }
        
    public string PasswordHash { get; set; }
}
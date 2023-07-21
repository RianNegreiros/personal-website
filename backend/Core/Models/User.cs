﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backend.Core.Models;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
        
    public string Username { get; set; }
        
    public string PasswordHash { get; set; }
}
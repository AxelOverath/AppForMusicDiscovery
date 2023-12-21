﻿using MongoDB.Bson;

namespace MusicDiscoveryApp;

public class User
{
    public ObjectId Id { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Username { get; set; }
    public DateTime DateOfBirth { get; set; }
}

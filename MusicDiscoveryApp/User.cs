using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MusicDiscoveryApp;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string? Email { get; set; }
    public string? Password { get; set; }
}


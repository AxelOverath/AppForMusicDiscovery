using MongoDB.Driver;
using MusicDiscoveryApp;

namespace MusicDiscoveryApp
{
    internal class Database
    {
        private static IMongoCollection<User> collection;

        static Database()
        {
            const string connectionUri = "mongodb://caelanstraus:PASSWORD@ac-rg0cquc-shard-00-00.lntmq6w.mongodb.net:27017,ac-rg0cquc-shard-00-01.lntmq6w.mongodb.net:27017,ac-rg0cquc-shard-00-02.lntmq6w.mongodb.net:27017/?ssl=true&replicaSet=atlas-dkat5c-shard-0&authSource=admin&retryWrites=true&w=majority";
            var settings = MongoClientSettings.FromConnectionString(connectionUri);
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);

string databaseName = "HormonyHuntDB";
string collectionName = "users";

var client = new MongoClient(settings);
var db = client.GetDatabase(databaseName);
var collection = db.GetCollection<User>(collectionName);

var user = new User { Name = "Jefke", Password = "PASSWORD123" };

await collection.InsertOneAsync(user);

var results = await collection.FindAsync(_ => true);

foreach (var result in results.ToList())
{
    Console.WriteLine($"{result.Name} {result.Password}");
}
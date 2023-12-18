using MongoDB.Driver;
using MusicDiscoveryApp;

const string connectionString = "mongodb+srv://caelanstraus:PASSWORD@harmonyhuntcluster.lntmq6w.mongodb.net/?retryWrites=true&w=majority";
var settings = MongoClientSettings.FromConnectionString(connectionString);
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
using MongoDB.Bson;
using MongoDB.Driver;

namespace MusicDiscoveryApp
{
    internal class Database
    {
        private static IMongoCollection<User> collection;

        static Database()
        {
            const string connectionUri = "mongodb://caelanstraus:PASS@ac-q6o5f9r-shard-00-00.laimuxx.mongodb.net:27017,ac-q6o5f9r-shard-00-01.laimuxx.mongodb.net:27017,ac-q6o5f9r-shard-00-02.laimuxx.mongodb.net:27017/?ssl=true&replicaSet=atlas-ldwoui-shard-0&authSource=admin&retryWrites=true&w=majority"; 
            var settings = MongoClientSettings.FromConnectionString(connectionUri);
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);

            string databaseName = "HarmonyHDB";
            string collectionName = "users";
             
            var client = new MongoClient(settings);

            try
            {
                var result = client.GetDatabase("Test").RunCommand<BsonDocument>(new BsonDocument("ping", 1));
                Console.WriteLine("Pinged your deployment. You successfully connected to MongoDB!");
                System.Diagnostics.Debug.WriteLine("Pinged your deployment. You successfully connected to MongoDB!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            var db = client.GetDatabase(databaseName);
            collection = db.GetCollection<User>(collectionName);
        }

        public static IMongoCollection<User> UsersCollection
        {
            get { return collection; }
        }

        public static async Task InsertUserAsync(User user)
        {
            await collection.InsertOneAsync(user);
        }
    }
}
using MongoDB.Driver;
using System.Threading.Tasks;

namespace MusicDiscoveryApp
{
    internal class Database
    {
        private static IMongoCollection<User> collection;

        static Database()
        {
            // Move the database setup logic to a static constructor
            const string connectionString = "mongodb+srv://caelanstraus:PASSWORD@harmonyhuntcluster.lntmq6w.mongodb.net/?retryWrites=true&w=majority";
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);

            string databaseName = "HormonyHDB";
            string collectionName = "users";

            var client = new MongoClient(settings);
            var db = client.GetDatabase(databaseName);
            collection = db.GetCollection<User>(collectionName);
        }

        public static IMongoCollection<User> UsersCollection
        {
            get { return collection; }
        }

        public static async Task<bool> IsUserExistsAsync(string username)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Name, username);
            var result = await collection.Find(filter).FirstOrDefaultAsync();
            return result != null;
        }

        public static async Task InsertUserAsync(User user)
        {
            await collection.InsertOneAsync(user);
        }
    }
}

﻿using MongoDB.Bson;
using MongoDB.Driver;

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
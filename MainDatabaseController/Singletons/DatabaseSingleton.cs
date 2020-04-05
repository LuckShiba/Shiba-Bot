using ConfigurationController.Models;
using MongoDB.Driver;

namespace MainDatabaseController.Singletons {
    public static class DatabaseSingleton {
        internal static IMongoDatabase database;
        public static void Connect(ShibaConfigModel shibaConfig) {
            MongoClient client = new MongoClient(shibaConfig.MongoConnectionString);
            database = client.GetDatabase("shiba_db");
        }
    }
}

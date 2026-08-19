using MongoDB.Driver;

namespace SmartExamSystem.Database
{
    public class MongoDBConnection
    {
        private static string connectionString =
        "mongodb+srv://examadmin:Exam12345@cluster0.pcmfii8.mongodb.net/?appName=Cluster0";


        private static MongoClient client =
            new MongoClient(connectionString);


        public static IMongoDatabase GetDatabase()
        {
            return client.GetDatabase("SmartExamDB");
        }
    }
}
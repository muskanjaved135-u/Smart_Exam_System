using MongoDB.Driver;
using SmartExamSystem.Database;
using SmartExamSystem.Models;

namespace SmartExamSystem.Services
{
    public class UserService
    {

        private readonly IMongoCollection<UserModel> users;


        public UserService()
        {
            var database = MongoDBConnection.GetDatabase();

            users = database.GetCollection<UserModel>("Users");
        }



        public void RegisterUser(UserModel user)
        {
            users.InsertOne(user);
        }

        public UserModel LoginUser(string email, string password)
        {
            var user = users.Find(
                x => x.Email == email &&
                     x.Password == password
            ).FirstOrDefault();


            return user;
        }

    }
}
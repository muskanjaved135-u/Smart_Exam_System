using MongoDB.Driver;
using SmartExamSystem.Database;
using SmartExamSystem.Models;
using System.Collections.Generic;


namespace SmartExamSystem.Services
{

    public class ResultService
    {

        private IMongoCollection<ResultModel> results;


        public ResultService()
        {

            var database =
            MongoDBConnection.GetDatabase();


            results =
            database.GetCollection<ResultModel>("Results");

        }



        public void SaveResult(ResultModel result)
        {

            results.InsertOne(result);

        }



        public void AddResult(ResultModel result)
        {

            results.InsertOne(result);

        }



        public List<ResultModel> GetResults()
        {

            return results.Find(x => true).ToList();

        }



        public List<ResultModel> GetStudentResults(string email)
        {

            return results
            .Find(x => x.StudentEmail == email)
            .ToList();

        }

    }

}
using MongoDB.Driver;
using SmartExamSystem.Database;
using SmartExamSystem.Models;
using System.Collections.Generic;


namespace SmartExamSystem.Services
{

    public class ExamService
    {


        private IMongoCollection<ExamModel> exams;



        public ExamService()
        {

            var db =
            MongoDBConnection.GetDatabase();


            exams =
            db.GetCollection<ExamModel>("Exams");

        }




        public void AddExam(ExamModel exam)
        {

            exams.InsertOne(exam);

        }



        public List<ExamModel> GetExams()
        {

            return exams.Find(x => true).ToList();

        }


    }

}
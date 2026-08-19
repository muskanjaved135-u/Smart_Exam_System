using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SmartExamSystem.Models
{
    public class ResultModel
    {

        public int ResultID { get; set; }
        

       
        public int TotalMarks { get; set; }
        public int ObtainedMarks { get; set; }
      
        public string Status { get; set; }
    
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }


        [BsonElement("ExamName")]
        public string ExamName { get; set; }
   
     


        [BsonElement("StudentName")]
        public string StudentName { get; set; }


        [BsonElement("StudentEmail")]
        public string StudentEmail { get; set; }


        [BsonElement("StudentRoll")]
        public string StudentRoll { get; set; }


        [BsonElement("TotalQuestions")]
        public int TotalQuestions { get; set; }


        [BsonElement("CorrectAnswers")]
        public int CorrectAnswers { get; set; }


        [BsonElement("Marks")]
        public int Marks { get; set; }


        [BsonElement("Percentage")]
        public double Percentage { get; set; }


        [BsonElement("Date")]
        public DateTime Date { get; set; }

    }
}
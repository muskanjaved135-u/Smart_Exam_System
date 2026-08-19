using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SmartExamSystem.Models
{
    [BsonIgnoreExtraElements] // Yeh line lagane se 'Subject' wala crash foran khatam ho jayega!
    public class ExamModel
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string ExamName { get; set; }

        public int TimeDuration { get; set; }

        public int TotalQuestions { get; set; }
    }
}
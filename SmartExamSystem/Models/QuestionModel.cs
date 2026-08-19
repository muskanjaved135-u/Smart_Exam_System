using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SmartExamSystem.Models
{
    [BsonIgnoreExtraElements]
    public class QuestionModel
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string ExamName { get; set; }

        public string QuestionText { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
        public string CorrectAnswer { get; set; }
    }
}
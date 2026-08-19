namespace SmartExamSystem.Models
{
    public class UserModel
    {
        [MongoDB.Bson.Serialization.Attributes.BsonId]
        [MongoDB.Bson.Serialization.Attributes.BsonIgnoreIfDefault]
        public object Id { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        // 🔥 Ye fields aapki model class me lazmi honi chahiye:
        [MongoDB.Bson.Serialization.Attributes.BsonIgnoreIfNull]
        public string RollNumber { get; set; }

        [MongoDB.Bson.Serialization.Attributes.BsonIgnoreIfNull]
        public string DateOfBirth { get; set; }

        [MongoDB.Bson.Serialization.Attributes.BsonIgnoreIfNull]
        public string Semester { get; set; }
    }
}
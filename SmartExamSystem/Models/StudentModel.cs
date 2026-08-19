public class StudentModel
{
    [MongoDB.Bson.Serialization.Attributes.BsonId]
    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreIfDefault]
    public object Id { get; set; }

    public string RollNumber { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }

    // 🔥 FIX 1: Naye students ke liye DOB property
    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreIfNull]
    public string DateOfBirth { get; set; }

    // 🔥 FIX 2: Purane database records ke liye Semester property (BsonIgnoreIfNull lazmi lagayein)
    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreIfNull]
    public string Semester { get; set; }
}
using MongoDB.Driver;
using SmartExamSystem.Database;
using SmartExamSystem.Models;
using System;
using System.Collections.Generic;

namespace SmartExamSystem.Services
{
    public class QuestionService
    {
        private IMongoCollection<QuestionModel> questions;

        public QuestionService()
        {
            try
            {
                var db = MongoDBConnection.GetDatabase();
                questions = db.GetCollection<QuestionModel>("Questions");
            }
            catch
            {
                questions = null;
            }
        }

        public void AddQuestion(QuestionModel question)
        {
            if (questions != null)
            {
                questions.InsertOne(question);
            }
        }

        // BIULKUL CLEAN SINGLE GETQUESTIONS FUNCTION WITH EXCEPTION PROTECTION
        public List<QuestionModel> GetQuestions()
        {
            try
            {
                if (questions == null)
                {
                    return new List<QuestionModel>();
                }
                return questions.Find(x => true).ToList();
            }
            catch
            {
                return new List<QuestionModel>();
            }
        }

        public long GetQuestionCountForExam(string examName)
        {
            try
            {
                if (questions == null) return 0;
                return questions.CountDocuments(q => q.ExamName == examName);
            }
            catch
            {
                return 0;
            }
        }
    }
}
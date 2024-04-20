using SQLite;

using EvaluationApp.Helpers;
using EvaluationApp.Models;

namespace EvaluationApp.Services
{
    public class DatabaseService : IDatabaseService
    {
        SQLiteAsyncConnection db;

        public DatabaseService()
        {

        }

        async Task Init()
        {
            if (db is not null)
                return;

            db = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);

            await db.CreateTablesAsync<Exam,Question>();
        }

        public async Task<List<Exam>> GetExams()
        {
            await Init();
            return await db.Table<Exam>().ToListAsync();
        }
        public async Task<Exam> GetExamById(int examId)
        {
            await Init();
            return await db.FindAsync<Exam>(examId);
        }

        public async Task<int> AddExam(Exam exam)
        {
            await Init();
            return await db.InsertAsync(exam);
        }

        public async Task<int> UpdateExam(Exam exam)
        {
            await Init();
            return await db.UpdateAsync(exam);
        }

        public async Task<int> DeleteExam(Exam exam)
        {
            await Init();
            return await db.DeleteAsync(exam);
        }


        public async Task<List<Question>> GetQuestions(int examId)
        {
            await Init();
            return await db.Table<Question>().Where(x => x.ExamId == examId).ToListAsync();
        }

        public async Task<Question> GetQuestionById(int questionId)
        {
            await Init();
            return await db.FindAsync<Question>(questionId);
        }

        public async Task<int> AddQuestion(Question question)
        {
            await Init();
            return await db.InsertAsync(question);
        }

        public async Task<int> UpdateQuestion(Question question)
        {
            await Init();
            return await db.UpdateAsync(question);
        }

        public async Task<int> DeleteQuestion(Question question)
        {
            await Init();
            return await db.DeleteAsync(question);
        }
    }
}

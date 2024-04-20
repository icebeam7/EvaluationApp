using EvaluationApp.Models;

namespace EvaluationApp.Services
{
    public interface IDatabaseService
    {
        Task<List<Exam>> GetExams();
        Task<Exam> GetExamById(int examId);
        Task<int> AddExam(Exam exam);
        Task<int> UpdateExam(Exam exam);
        Task<int> DeleteExam(Exam exam);

        Task<List<Question>> GetQuestions(int examId);
        Task<Question> GetQuestionById(int questionId);
        Task<int> AddQuestion(Question question);
        Task<int> UpdateQuestion(Question question);
        Task<int> DeleteQuestion(Question question);
    }
}

using SQLite;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvaluationApp.Models
{
    public class Exam
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Topic { get; set; }

        [Ignore]
        public List<Question> Questions { get; set; }
    }
}

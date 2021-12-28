using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eqirl.Models
{

        [Table("Exams")]
        public class Exam
        {

            [Key]
            public int ID { get; set; }
            public string ExamName { get; set; }

            public ICollection<QuizQestion> QuizQestions { get; set; }
        }

        [Table("QuizQuestions")]
        public class QuizQestion
        {

            [Key]
            public int ID { get; set; }

            [ForeignKey("Exam")]
            public int FKDeptID { get; set; }
            public string Question { get; set; }
            public string Option1 { get; set; }

            public string Option2 { get; set; }
            public string Option3 { get; set; }

            public string Option4 { get; set; }

            public int CorrectAnswer { get; set; }

            public int Answer { get; set; }
            public Exam Exam { get; set; }
        }

    
}

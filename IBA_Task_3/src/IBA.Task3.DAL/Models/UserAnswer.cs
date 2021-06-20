using System.ComponentModel.DataAnnotations;

namespace IBA.Task3.DAL.Models
{
    public class UserAnswer : Entity
    {
        public Test Test { get; set; }

        public Question Question { get; set; }

        [Required]
        public int AnswerId { get; set; }
        public Answer Answer { get; set; }

        [Required]
        public int UserId { get; set; }
        public User User { get; set; }

        public UserAnswer() { }

        public UserAnswer(int answerId, int userId)
        {
            AnswerId = answerId;
            UserId = userId;
        }
    }
}

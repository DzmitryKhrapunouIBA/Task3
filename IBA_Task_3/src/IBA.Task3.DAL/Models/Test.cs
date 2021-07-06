using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace IBA.Task3.DAL.Models
{
    public class Test : Entity
    {
        [Column("Name")]
        public string Name { get; set; }

        [Column("UserId")]
        public int UserId { get; set; }

        [Column("Attempts")]
        public int Attempts { get; set; }

        public IEnumerable<Question> Questions { get; set; }
        public IEnumerable<Answer> Answers { get; set; }

        public Test() { }

        public Test(string name, int userId, int attempts)
        {
            Name = name;
            UserId = userId;
            Attempts = attempts;
            Questions = new List<Question>();
            Answers = new List<Answer>();
        }
    }
}

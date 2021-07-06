using System.Collections.Generic;

namespace IBA.Task3.DAL.Models
{
    public class Question : Entity
    {
        public string Name { get; set; }
        public int TestId { get; set; }
        public Test Test { get; set; }
        public IEnumerable<Answer> Answers { get; set; }

        public Question() { }

        public Question(Test test, string name)
        {
            Test = test;
            Name = name;
            Answers = new List<Answer>();
        }
    }
}

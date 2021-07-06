using System.Collections.Generic;

namespace IBA.Task3.DAL.Models
{
    public class TestAssignment : Entity
    {
        public int TestId { get; set; }

        public int UserId { get; set; }

        public Test Test { get; set; }
        public User User { get; set; }

        public IEnumerable<Test> Tests { get; set; }

        public TestAssignment() { }

        public TestAssignment(User user)
        {
            User = user;
            Tests = new List<Test>();
        }
    }
}

using System.Collections.Generic;

namespace IBA.Task3.DAL.Models
{
    public class TestResult : Entity
    {
        public int TestId { get; set; }
        public Test Test { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int Result { get; set; }
        public IEnumerable<UserAnswer> UserAnswers { get; set; }


        public TestResult() { }

        public TestResult(int testId, int userId, int result)
        {
            TestId = testId;
            UserId = userId;
            Result = result;
            UserAnswers = new List<UserAnswer>();
        }
    }
}

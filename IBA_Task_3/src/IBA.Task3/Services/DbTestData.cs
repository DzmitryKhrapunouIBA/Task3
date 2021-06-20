using IBA.Task3.DAL.Models;

namespace IBA.Task3.Services
{
    public class DbTestData
    {
        public void workWithDb(DAL.Context.AppContext db)
        {
            /*
            User user1 = new User("Дмитрий", "SR", "HR", "Dima", "1234") { Id = -1 };
            User user2 = new User("Александра", "Vtch", "Kuku", "AlexLass", "1234") { Id = -2 };
            Test test1 = new Test("Космос", user2.Id, 3);
            TestAssignment testAssignment = new TestAssignment(test1, user1);
            Question question1 = new Question(test1, "Название второй планеты?");
            Answer answer1 = new Answer(question1, "Венера", true);
            Answer answer2 = new Answer(question1, "Земля", false);
            Answer answer3 = new Answer(question1, "Марс", false);
            Answer answer4 = new Answer(question1, "Меркурий", false);
            db.Users.Add(user1);
            db.Users.Add(user2);
            db.Tests.Add(test1);
            db.TestAssignments.Add(testAssignment);
            db.Questions.Add(question1);
            db.Answers.Add(answer1);
            db.Answers.Add(answer2);
            db.Answers.Add(answer3);
            db.Answers.Add(answer4);
            db.SaveChanges();

            Question question2 = new Question(test1, "Почему луна светит?");
            Answer ans1 = new Answer(question2, "Излучает свет", false);
            Answer ans2 = new Answer(question2, "Светят инопланетяне", false);
            Answer ans3 = new Answer(question2, "Отражает солнечный свет", true);
            Answer ans4 = new Answer(question2, "Луна - это голограмма", false);
            db.Questions.Add(question2);
            db.Answers.Add(ans1);
            db.Answers.Add(ans2);
            db.Answers.Add(ans3);
            db.Answers.Add(ans4);
            db.SaveChanges();
            */
        }
    }
}

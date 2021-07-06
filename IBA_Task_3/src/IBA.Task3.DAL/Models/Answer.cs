namespace IBA.Task3.DAL.Models
{
    public class Answer : Entity
    {
        public string Name { get; set; }
        public bool CorrectAnswer { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }

        public Answer() { }

        public Answer(Question question, string name, bool correctAnswer)
        {
            Question = question;
            Name = name;
            CorrectAnswer = correctAnswer;
        }
    }
}

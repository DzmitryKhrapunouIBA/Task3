namespace IBA.Task3.DAL.Models
{
    public class Answer : Entity
    {
        public string Name { get; set; }
        public bool CorrectAnswer { get; set; }
        public Question Question { get; set; }

        public int QuestionId { get; set; }

        public Answer() { }

        public Answer(int questionId, string name, bool correctAnswer)
        {
            QuestionId = questionId;
            Name = name;
            CorrectAnswer = correctAnswer;
        }
    }
}

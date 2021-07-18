using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace IBA.Task3
{
    public class TestBase
    {
        [Required]
        [JsonProperty("name")]
        public string Name { get; set; }
    }
    public class TestModel : TestBase
    {
        [JsonProperty("attempts")]
        public int Attempts { get; set; }
    }

    public class TestCreateModel : TestModel
    {
        public string UserName { get; set; }
    }

    public class TestUpdateModel : TestBase
    {
        [Required]
        [JsonProperty("question")]
        public string Question { get; set; }

        [Required]
        [JsonProperty("correctAnswer")]
        public int CorrectAnswer { get; set; }

        [Required]
        [JsonProperty("answers")]
        public string[] Answers { get; set; }
    }

    public class TestViewModel : TestModel
    {
        [Required]
        [JsonProperty("numberOfQuestions")]
        public int NumberOfQuestions { get; set; }
    }
}
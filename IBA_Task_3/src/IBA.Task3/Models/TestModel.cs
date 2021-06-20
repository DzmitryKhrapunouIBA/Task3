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

    public class TestCreateModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        public string UserName { get; set; }

        [JsonProperty("attempts")]
        public int Attempts { get; set; }
    }

    public class TestUpdateModel : TestBase
    {
        [Required]
        [JsonProperty("question")]
        public string Question { get; set; }

        [Required]
        [JsonProperty("answer1")]
        public string Answer1 { get; set; }

        [Required]
        [JsonProperty("answer2")]
        public string Answer2 { get; set; }

        [Required]
        [JsonProperty("answer3")]
        public string Answer3 { get; set; }

        [Required]
        [JsonProperty("answer4")]
        public string Answer4 { get; set; }

        [Required]
        [JsonProperty("correctAnswer")]
        public int CorrectAnswer { get; set; }
    }
}
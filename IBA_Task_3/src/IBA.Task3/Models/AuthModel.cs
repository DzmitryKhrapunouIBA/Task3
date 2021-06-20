using System.ComponentModel.DataAnnotations;

namespace IBA.Task3
{
    public class AuthModel
    {
        [Required]
        [MinLength(4)]
        public string Login { get; set; }

        [Required]
        [MinLength(4)]
        public string Password { get; set; }
    }
}
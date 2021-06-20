using System.ComponentModel.DataAnnotations;

namespace IBA.Task3
{
    public class RegistrationModel : AuthModel
    {
        [Required]
        public string Lastname { get; set; }
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Разные пароли. Попробуйте еще")]
        public string PasswordConfirm { get; set; }
    }
}
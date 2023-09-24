using System.ComponentModel.DataAnnotations;

namespace MVCSimpleCRM.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Adres email")]
        [Required(ErrorMessage = "Podanie loginu jest wymagane")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Podanie hasła jest wymagane")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

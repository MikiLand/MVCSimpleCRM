using System.ComponentModel.DataAnnotations;

namespace MVCSimpleCRM.ViewModels
{
    public class EditAccountViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Imię użytkownika jast wymagane!")]
        public string Name { get; set; }

        [Display(Name = "Surname")]
        [Required(ErrorMessage = "Nazwisko użytkownika jast wymagane!")]
        public string Surname { get; set; }

        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Adres email jest wymagany!")]
        public string Email { get; set; }

        /*[Required]
        [DataType(DataType.Password)]*/
        public string PasswordHash { get; set; }

        /*
        [Display(Name = "Confirm password")]
        [Required(ErrorMessage = "Potwierdzenie hasła jest wymagane!")]
        [DataType(DataType.Password)]
        [Compare("PasswordHash", ErrorMessage = "Podane hasła nie są identyczne!")]
        public string ConfirmPassword { get; set; }*/
    }
}

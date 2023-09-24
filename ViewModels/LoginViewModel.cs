using System.ComponentModel.DataAnnotations;

namespace MVCSimpleCRM.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email address is required")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string PasswordHash { get; set; }
    }
}

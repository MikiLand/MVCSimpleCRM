﻿using System.ComponentModel.DataAnnotations;

namespace MVCSimpleCRM.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "UserName")]
        [Required(ErrorMessage = "Nazwa użytkownika jast wymagana!")]
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

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Podanie hasła jest wymagane!")]
        [DataType(DataType.Password)]
        public string PasswordHash { get; set; }

        [Display(Name = "Confirm password")]
        [Required(ErrorMessage = "Potwierdzenie hasła jest wymagane!")]
        [DataType(DataType.Password)]
        [Compare("PasswordHash", ErrorMessage = "Podane hasła nie są identyczne!")]
        public string ConfirmPassword { get; set; }

    }
}

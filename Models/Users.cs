﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace MVCSimpleCRM.Models
{
    public class Users
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsTACConfirmed { get; set; }
        public int IsAdmin { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}

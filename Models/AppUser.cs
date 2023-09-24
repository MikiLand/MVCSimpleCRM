using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace MVCSimpleCRM.Models
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool IsTACConfirmed { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}

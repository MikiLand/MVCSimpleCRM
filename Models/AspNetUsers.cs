using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace MVCSimpleCRM.Models
{
    public class AspNetUsers
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        //[DataType(DataType.Password)]
        public string PasswordHash { get; set; }
        public DateTime CreateDate { get; set; }
        //public string ConfirmPassword { get; set; }
    }
}

using MVCSimpleCRM.Models;
using System.ComponentModel.DataAnnotations;

namespace MVCSimpleCRM.ViewModels
{
    public class DetailAccountViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime CreateDate { get; set; }
        public List<Tasks> Tasks { get; set; }
    }
}

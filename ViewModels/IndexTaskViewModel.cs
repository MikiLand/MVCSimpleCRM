using MVCSimpleCRM.Models;
using System.ComponentModel.DataAnnotations;

namespace MVCSimpleCRM.ViewModels
{
    public class IndexTaskViewModel
    {
        public List<Tasks> Tasks { get; set; }
        public List<AspNetUsers> Users { get; set; }
    }
}
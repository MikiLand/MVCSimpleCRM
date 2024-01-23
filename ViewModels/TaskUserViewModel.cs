using System.ComponentModel.DataAnnotations.Schema;

namespace MVCSimpleCRM.ViewModels
{
    public class TaskUserViewModel
    {
        public int Id { get; set; }
        public int IdTask { get; set; }
        public string IdUser { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCSimpleCRM.Models
{
    public class TaskUsers
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("TaskID")]
        public int IdTask { get; set; }
        [ForeignKey("UserID")]
        public int IdUser { get; set; }
    }
}

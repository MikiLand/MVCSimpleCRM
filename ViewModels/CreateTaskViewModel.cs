using System.ComponentModel.DataAnnotations;

namespace MVCSimpleCRM.ViewModels
{
    public class CreateTaskViewModel
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string CreatorStatus { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime DueDate { get; set; }
        //[ForeignKey("UserID")]
        public string IDUserCreate { get; set; }
    }
}

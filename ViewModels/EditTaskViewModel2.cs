using MVCSimpleCRM.Models;

namespace MVCSimpleCRM.ViewModels
{
    public class EditTaskViewModel2
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string CreatorStatus { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime DueDate { get; set; }
        //[ForeignKey("UserID")]
        public string IDUserCreate { get; set; }
        public List<TaskUserViewModel> TaskPositionUsers { get; set; }
        //public List<TaskUsers> TaskPositionUsers { get; set; }
    }
}

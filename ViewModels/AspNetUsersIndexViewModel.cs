namespace MVCSimpleCRM.ViewModels
{
    public class AspNetUsersIndexViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsChecked {  get; set; }
    }
}

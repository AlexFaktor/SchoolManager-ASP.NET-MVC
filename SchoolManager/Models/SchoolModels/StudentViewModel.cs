namespace SchoolManager.Models.SchoolModels
{
    public class StudentViewModel
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public int GroupId { get; set; }
    }
}

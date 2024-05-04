namespace SchoolManager.Models.SchoolModels
{
    public class GroupViewModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int CourseId { get; set; }
        public required List<StudentViewModel> Students { get; set; }
    }
}

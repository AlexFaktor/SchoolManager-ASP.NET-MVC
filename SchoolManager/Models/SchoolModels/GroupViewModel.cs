namespace SchoolManager.Models.SchoolModels
{
    public class GroupViewModel
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public int CourseId { get; set; }
        public List<StudentViewModel> Students { get; set; }
    }
}

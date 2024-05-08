using SchoolManager.Database.Entity;

namespace SchoolManager.Models.ViewModels.SchoolVM
{
    public class SchoolIndexVM
    {
        public List<CourseRecord> Courses { get; set; } = new List<CourseRecord>();
        public List<GroupRecord> Groups { get; set; } = new List<GroupRecord>();
        public List<StudentRecord> Students { get; set; } = new List<StudentRecord>();

        public Guid? CourseId { get; set; } = Guid.Empty;
        public Guid? GroupId { get; set; } = Guid.Empty;
        public Guid? StudentId { get; set; } = Guid.Empty;
    }
}

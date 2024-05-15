using SchoolManager.Database.Entity;
using SchoolManager.Models.SchoolModels;

namespace SchoolManager.Models.ViewModels.GroupVM
{
    public class CreateGroupVM
    {
        public GroupRecord Group { get; set; }

        public List<CourseRecord> Courses { get; }

        public CreateGroupVM(List<CourseRecord> courses)
        {
            Group = new GroupRecord();
            Courses = courses;
        }

        public CreateGroupVM()
        {
            Group = new GroupRecord();
            Courses = new List<CourseRecord>();
        }
    }
}

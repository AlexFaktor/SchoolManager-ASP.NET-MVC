using SchoolManager.Database.Entity;
using SchoolManager.Models.SchoolModels;

namespace SchoolManager.Models.ViewModels.SchoolVM
{
    public class SchoolCreateGroupVM
    {
        public GroupRecord Group { get; set; }

        public List<CourseRecord> Courses { get; }

        public SchoolCreateGroupVM(List<CourseRecord> courses)
        {
            Group = new GroupRecord();
            Courses = courses;
        }

        public SchoolCreateGroupVM()
        {
            Group = new GroupRecord();
            Courses = new List<CourseRecord>();
        }
    }
}

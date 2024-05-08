using SchoolManager.Database.Entity;

namespace SchoolManager.Models.ViewModels.SchoolVM
{
    public class SchoolCreateCourseVM()
    {
        public CourseRecord Course { get; set; } = new CourseRecord();
    }
}

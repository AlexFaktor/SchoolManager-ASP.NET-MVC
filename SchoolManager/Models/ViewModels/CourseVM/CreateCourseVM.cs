using SchoolManager.Database.Entity;

namespace SchoolManager.Models.ViewModels.CourseVM
{
    public class CreateCourseVM()
    {
        public CourseRecord Course { get; set; } = new CourseRecord();
    }
}

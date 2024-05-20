using SchoolManager.Database.Entity;

namespace SchoolManager.Models.SchoolModels
{
    public class CourseVM(CourseRecord record)
    {
        public CourseRecord Record { get; } = record;
    }
}

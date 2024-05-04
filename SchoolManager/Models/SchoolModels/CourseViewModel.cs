using SchoolManager.Database.Entity;

namespace SchoolManager.Models.SchoolModels
{
    public class CourseViewModel(CourseRecord record)
    {
        CourseRecord Record { get; } = record;
    }
}

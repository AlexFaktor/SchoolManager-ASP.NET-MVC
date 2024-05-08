using SchoolManager.Database.Entity;

namespace SchoolManager.Models.ViewModels.SchoolVM
{
    public class SchoolEditCourseVM
    {
        public CourseRecord RecordCourse { get; set; }
        public CourseRecord NewCourse { get; set; }

        public SchoolEditCourseVM(CourseRecord record)
        {
            RecordCourse = record;

            NewCourse = new CourseRecord()
            { 
                Id = record.Id,
                Groups = record.Groups,
            };
        }
    }
}

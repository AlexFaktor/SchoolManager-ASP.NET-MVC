using SchoolManager.Database.Entity;

namespace SchoolManager.Models.ViewModels.CourseVM
{
    public class EditCourseVM
    {
        public CourseRecord RecordCourse { get; set; }
        public CourseRecord NewCourse { get; set; }

        public EditCourseVM(CourseRecord record)
        {
            RecordCourse = record;

            NewCourse = new CourseRecord()
            {
                Id = record.Id,
                Groups = record.Groups,
            };
        }

        public EditCourseVM()
        {
            RecordCourse = new CourseRecord();
            NewCourse = new CourseRecord();
        }
    }
}

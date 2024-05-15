using SchoolManager.Database.Entity;

namespace SchoolManager.Models.ViewModels.GroupVM
{
    public class EditGroupVM
    {
        public GroupRecord RecordGroup { get; set; }
        public GroupRecord NewGroup { get; set; }

        public List<CourseRecord> Courses { get; }

        public EditGroupVM(GroupRecord record, List<CourseRecord> courses)
        {
            RecordGroup = record;
            Courses = courses;

            NewGroup = new GroupRecord()
            {
                Id = record.Id,
                CourseId = record.CourseId,
                Course = record.Course,
                Students = record.Students,
            };
        }

        public EditGroupVM() { }
    }
}

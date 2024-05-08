using SchoolManager.Database.Entity;

namespace SchoolManager.Models.ViewModels.SchoolVM
{
    public class SchoolEditGroupVM
    {
        public GroupRecord RecordGroup { get; set; }
        public GroupRecord NewGroup { get; set; }

        public List<CourseRecord> Courses { get; }

        public SchoolEditGroupVM(GroupRecord record, List<CourseRecord> courses)
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
    }
}

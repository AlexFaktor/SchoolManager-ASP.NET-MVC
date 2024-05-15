using SchoolManager.Database.Entity;

namespace SchoolManager.Models.ViewModels.GroupVM
{
    public class EditGroupVM
    {
        public GroupRecord RecordGroup { get; set; }
        public GroupRecord NewGroup { get; set; }


        public EditGroupVM(GroupRecord record)
        {
            RecordGroup = record;

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

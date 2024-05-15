using SchoolManager.Database.Entity;

namespace SchoolManager.Models.ViewModels.StudentVM
{
    public class EditStudentVM
    {
        public StudentRecord RecordStudent { get; set; }
        public StudentRecord NewStudent { get; set; }

        public List<GroupRecord> Groups { get; }

        public EditStudentVM(StudentRecord record, List<GroupRecord> groups)
        {
            RecordStudent = record;
            Groups = groups;

            NewStudent = new StudentRecord()
            {
                Id = record.Id,
                GroupId = record.GroupId,
                Group = record.Group,
            };
        }

        public EditStudentVM() { }
    }
}

using SchoolManager.Database.Entity;

namespace SchoolManager.Models.ViewModels.StudentVM
{
    public class EditStudentVM
    {
        public StudentRecord RecordStudent { get; set; }
        public StudentRecord NewStudent { get; set; }


        public EditStudentVM(StudentRecord record)
        {
            RecordStudent = record;

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

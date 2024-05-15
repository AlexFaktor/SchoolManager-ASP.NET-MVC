using SchoolManager.Database.Entity;

namespace SchoolManager.Models.ViewModels.StudentVM
{
    public class CreateStudentVM
    {
        public StudentRecord Student { get; set; }

        public List<GroupRecord> Groups { get; set; }

        public CreateStudentVM(List<GroupRecord> groups)
        {
            Student = new StudentRecord();
            Groups = groups;
        }
        public CreateStudentVM()
        {
            Student = new StudentRecord();
            Groups = new List<GroupRecord>();
        }
    }
}

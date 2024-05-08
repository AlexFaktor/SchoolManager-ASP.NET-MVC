using SchoolManager.Database.Entity;

namespace SchoolManager.Models.ViewModels.SchoolVM
{
    public class SchoolCreateStudentVM
    {
        public StudentRecord Student { get; set; }

        public List<GroupRecord> Groups { get; set; }

        public SchoolCreateStudentVM(List<GroupRecord> groups)
        {
            Student = new StudentRecord();
            Groups = groups;
        }
        public SchoolCreateStudentVM()
        {
            Student = new StudentRecord();
            Groups = new List<GroupRecord>();
        }
    }
}

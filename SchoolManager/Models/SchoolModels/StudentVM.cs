using SchoolManager.Database.Entity;

namespace SchoolManager.Models.SchoolModels
{
    public class StudentVM(StudentRecord record)
    {
        public StudentRecord Record { get; } = record;
    }
}

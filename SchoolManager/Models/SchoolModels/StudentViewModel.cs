using SchoolManager.Database.Entity;

namespace SchoolManager.Models.SchoolModels
{
    public class StudentViewModel(StudentRecord record)
    {
        StudentRecord Record { get; } = record;
    }
}

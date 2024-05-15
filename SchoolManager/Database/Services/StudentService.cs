using Microsoft.EntityFrameworkCore;
using SchoolManager.Database.Entity;
using SchoolManager.Resources.Interface;

namespace SchoolManager.Database.Services
{
    public class StudentService(SchoolDbContext db) : IEntityService
    {
        private readonly SchoolDbContext _db = db;

        public void AddStudentRecord(StudentRecord studentRecord)
        {
            _db.Students.Add(studentRecord);
            _db.SaveChanges();
        }

        public List<StudentRecord> GetStudents() => [.. _db.Students];
        public List<StudentRecord> GetStudents(Guid groupId) => [.. _db.Students.Where(s =>s.GroupId == groupId)];

        public StudentRecord? GetStudent(Guid id) => _db.Students.FirstOrDefault(s => s.Id == id);

        public async Task<List<StudentRecord>> GetStudentsAsync() => await _db.Students.ToListAsync();

        public bool UpdateStudent(StudentRecord studentRecord)
        {
            var existingStudent = _db.Students.FirstOrDefault(s => s.Id == studentRecord.Id);
            if (existingStudent != null)
            {
                existingStudent.Name = studentRecord.Name;
                existingStudent.Surname = studentRecord.Surname;
                existingStudent.GroupId = studentRecord.GroupId;
                existingStudent.Group = studentRecord.Group;

                return true;
            }
            return false;
        }

        public bool DeleteStudent(StudentRecord studentRecord)
        {
            if (_db.Students.Any(g => g.Id == studentRecord.Id))
            {
                _db.Students.Remove(_db.Students.First(g => g.Id == studentRecord.Id));
                _db.SaveChanges();
                return true;
            }
            return false;
        }

        public bool DeleteStudent(Guid id)
        {
            if (_db.Students.Any(s => s.Id == id))
            {
                _db.Students.Remove(_db.Students.First(s => s.Id == id));
                _db.SaveChanges();
                return true;
            }
            return false;
        }
    }
}

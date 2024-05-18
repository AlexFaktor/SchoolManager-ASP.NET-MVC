using Microsoft.EntityFrameworkCore;
using SchoolManager.Database.Entity;
using SchoolManager.Resources.Interface;

namespace SchoolManager.Database.Services
{
    public class StudentService : IEntityServiceExtended<StudentRecord>
    {
        private readonly SchoolDbContext _db;

        public StudentService(SchoolDbContext db)
        {
            _db = db;
        }

        public bool Add(StudentRecord record)
        {
            _db.Students.Add(record);
            _db.SaveChanges();
            return true;
        }

        public List<StudentRecord> GetAll() => _db.Students.ToList();

        public List<StudentRecord> GetAll(Guid ownerId) => _db.Students.Where(s => s.GroupId == ownerId).ToList();

        public async Task<List<StudentRecord>> GetAllAsync() => await _db.Students.ToListAsync();

        public StudentRecord? Get(Guid id) => _db.Students.FirstOrDefault(s => s.Id == id);

        public StudentRecord? Get(string name) => _db.Students.FirstOrDefault(s => s.Name == name);

        public bool Update(StudentRecord studentRecord)
        {
            var existingStudent = _db.Students.FirstOrDefault(s => s.Id == studentRecord.Id);
            if (existingStudent != null)
            {
                existingStudent.Name = studentRecord.Name;
                existingStudent.Surname = studentRecord.Surname;
                existingStudent.GroupId = studentRecord.GroupId;
                existingStudent.Group = studentRecord.Group;
                _db.SaveChanges();
                return true;
            }
            return false;
        }

        public bool Delete(Guid id)
        {
            var student = _db.Students.FirstOrDefault(s => s.Id == id);
            if (student != null)
            {
                _db.Students.Remove(student);
                _db.SaveChanges();
                return true;
            }
            return false;
        }

        public bool Delete(StudentRecord studentRecord)
        {
            return Delete(studentRecord.Id);
        }
    }
}

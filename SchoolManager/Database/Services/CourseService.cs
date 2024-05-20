using Microsoft.EntityFrameworkCore;
using SchoolManager.Database.Entity;
using SchoolManager.Resources.Interface;

namespace SchoolManager.Database.Services
{
    public class CourseService : IEntityService<CourseRecord>
    {
        private readonly SchoolDbContext _db;

        public CourseService(SchoolDbContext db)
        {
            _db = db;
        }

        public bool Add(CourseRecord courseRecord)
        {
            if (Get(courseRecord.Name) != null)
                return false;

            _db.Courses.Add(courseRecord);
            _db.SaveChanges();
            return true;
        }

        public List<CourseRecord> GetAll() => _db.Courses.ToList();

        public async Task<List<CourseRecord>> GetAllAsync() => await _db.Courses.ToListAsync();

        public CourseRecord? Get(Guid id) => _db.Courses.FirstOrDefault(c => c.Id == id);

        public CourseRecord? Get(string name) => _db.Courses.FirstOrDefault(c => c.Name == name);

        public bool Update(CourseRecord courseRecord)
        {
            if (Get(courseRecord.Name) != null)
                return false;

            var existingCourse = _db.Courses.FirstOrDefault(c => c.Id == courseRecord.Id);
            if (existingCourse != null)
            {
                existingCourse.Name = courseRecord.Name;
                existingCourse.Groups = courseRecord.Groups;
                _db.SaveChanges();
                return true;
            }
            return false;
        }

        public bool Delete(Guid id)
        {
            var course = _db.Courses.FirstOrDefault(c => c.Id == id);
            if (course != null)
            {
                _db.Courses.Remove(course);
                _db.SaveChanges();
                return true;
            }
            return false;
        }

        public bool Delete(CourseRecord courseRecord)
        {
            return Delete(courseRecord.Id);
        }
    }
}

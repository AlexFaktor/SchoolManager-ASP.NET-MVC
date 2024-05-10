using Microsoft.EntityFrameworkCore;
using SchoolManager.Database.Entity;
using SchoolManager.Resources.Interface;

namespace SchoolManager.Database.Services
{
    public class CourseService(SchoolDbContext db) : IEntityService
    {
        private readonly SchoolDbContext _db = db;

        public void AddCourseRecord(CourseRecord courseRecord) => _db.Courses.Add(courseRecord);

        public List<CourseRecord> GetCourses() => [.. _db.Courses];
        public async Task<List<CourseRecord>> GetCoursesAsync() => await _db.Courses.ToListAsync();

        public bool UpdateCourse(CourseRecord courseRecord)
        {
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

        public bool DeleteCourse(CourseRecord courseRecord)
        {
            if (_db.Courses.Any(c => c.Id == courseRecord.Id))
            {
                _db.Courses.Remove(_db.Courses.First(c => c.Id == courseRecord.Id));
                return true;
            }
            return false;
        }

        public bool DeleteCourse(Guid id)
        {
            if (_db.Courses.Any(c => c.Id == id))
            {
                _db.Courses.Remove(_db.Courses.First(c => c.Id == id));
                return true;
            }
            return false;
        }
    }
}

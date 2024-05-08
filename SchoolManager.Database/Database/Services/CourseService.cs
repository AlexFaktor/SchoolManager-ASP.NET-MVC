using SchoolManager.Database.Database;
using SchoolManager.Database.Entity;

namespace SchoolManager.Database.Services
{
    public class CourseService(SchoolDbContext db)
    {
        private readonly SchoolDbContext _db = db;

        public void AddCourseRecord(CourseRecord courseRecord) => _db.Courses.Add(courseRecord);

        public List<CourseRecord> GetCourses() => [.. _db.Courses];

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
            if(_db.Courses.Any(c => c.Id == id))
            {
                _db.Courses.Remove(_db.Courses.First(c => c.Id == id));
                return true;
            }
            return false;
        }
    }
}

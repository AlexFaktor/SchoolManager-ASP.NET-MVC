using SchoolManager.Database.Services;
using SchoolManager.Resources.Interface;

namespace SchoolManager.Database
{
    public class SchoolRepository : ISchoolRepository
    {
        private readonly SchoolDbContext _db;
        public CourseService CourseService { get; }
        public GroupService GroupService { get; }
        public StudentService StudentService { get; }

        public SchoolRepository(SchoolDbContext context)
        {
            _db = context;
            CourseService = new CourseService(_db);
            GroupService = new GroupService(_db);
            StudentService = new StudentService(_db);
        }

        public void DbSaveChanges() => _db.SaveChanges();
        public void DbSaveChangesAsync() => _db.SaveChangesAsync();
    }
}



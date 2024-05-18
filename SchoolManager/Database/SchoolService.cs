using SchoolManager.Database.Services;
using SchoolManager.Resources.Interface;

namespace SchoolManager.Database
{
    public class SchoolService : ISchoolService<CourseService, GroupService, StudentService>
    {
        private readonly SchoolDbContext _db;
        public CourseService Course { get; }
        public GroupService Group { get; }
        public StudentService Student { get; }

        public SchoolService(SchoolDbContext context)
        {
            _db = context;
            Course = new CourseService(_db);
            Group = new GroupService(_db);
            Student = new StudentService(_db);
        }
    }
}

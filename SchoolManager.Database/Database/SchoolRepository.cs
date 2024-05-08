using SchoolManager.Database.Services;

namespace SchoolManager.Database.Database
{
    public class SchoolRepository(SchoolDbContext context)
    {
        private readonly SchoolDbContext _db = context;

        public CourseService CourseService { get; } = new CourseService(context);
        public GroupService GroupService { get; } = new GroupService(context);
        public StudentService StudentService { get; } = new StudentService(context);

        public void DbSaveChanges() => _db.SaveChanges();
    }
}



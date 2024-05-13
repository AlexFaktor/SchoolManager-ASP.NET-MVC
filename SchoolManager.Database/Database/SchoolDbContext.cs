using Microsoft.EntityFrameworkCore;
using SchoolManager.Database.Entity;
using SchoolManager.Database.Entity.Configurations;

namespace SchoolManager.Database
{
    public class SchoolDbContext : DbContext
    {
        public DbSet<CourseRecord> Courses { get; set; }
        public DbSet<GroupRecord> Groups { get; set; }
        public DbSet<StudentRecord> Students { get; set; }

        public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CourseConfiguration());
            modelBuilder.ApplyConfiguration(new GroupConfiguration());
            modelBuilder.ApplyConfiguration(new StudentConfiguration());

            if (!modelBuilder.Model.GetEntityTypes().Any())
            {
                // Adding test data
                // modelBuilder.Entity<CourseRecord>().HasData(CreateTestData());
            }

            base.OnModelCreating(modelBuilder);
        }

        private CourseRecord[] CreateTestData()
        {
            var courseId1 = Guid.NewGuid();
            var courseId2 = Guid.NewGuid();

            var course1 = new CourseRecord
            {
                Id = courseId1,
                Name = "Course 1"
            };

            var group1 = new GroupRecord
            {
                Id = Guid.NewGuid(),
                Name = "Group 1",
                CourseId = courseId1
            };

            var student1 = new StudentRecord { Id = Guid.NewGuid(), Name = "Ethan", Surname = "Andrews", GroupId = group1.Id };
            var student2 = new StudentRecord { Id = Guid.NewGuid(), Name = "Olivia", Surname = "Thompson", GroupId = group1.Id };

            course1.Groups.Add(group1);

            var course2 = new CourseRecord
            {
                Id = courseId2,
                Name = "Course 2"
            };

            var group2 = new GroupRecord
            {
                Id = Guid.NewGuid(),
                Name = "Group 2",
                CourseId = courseId2
            };

            var student3 = new StudentRecord { Id = Guid.NewGuid(), Name = "Lucas", Surname = "Mitchell", GroupId = group2.Id };
            var student4 = new StudentRecord { Id = Guid.NewGuid(), Name = "Emma", Surname = "Carter", GroupId = group2.Id };

            course2.Groups.Add(group2);

            return new CourseRecord[] { course1, course2 };
        }
    }
}



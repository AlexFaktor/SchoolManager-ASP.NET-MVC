using Microsoft.EntityFrameworkCore;
using SchoolManager.Database.Entity;
using SchoolManager.Database.Entity.Configurations;
using SchoolManager.Database.Tools;

namespace SchoolManager.Database
{
    public class SchoolDbContext : DbContext
    {
        public DbSet<CourseRecord> Courses { get; set; }
        public DbSet<GroupRecord> Groups { get; set; }
        public DbSet<StudentRecord> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CourseConfiguration());
            modelBuilder.ApplyConfiguration(new GroupConfiguration());
            modelBuilder.ApplyConfiguration(new StudentConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        readonly string path = FileHelper.GetDbPath();
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder
            .UseSqlite($"Data Source={path + "SchoolASPdb.db"};Foreign Keys=False");
    }
}



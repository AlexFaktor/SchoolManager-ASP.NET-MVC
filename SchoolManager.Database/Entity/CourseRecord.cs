namespace SchoolManager.Database.Entity
{
    public class CourseRecord
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<GroupRecord> Groups { get; set; } = [];
    }
}

namespace SchoolManager.Database.Entity
{
    public class GroupRecord
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public Guid? CourseId { get; set; }
        public CourseRecord? Course { get; set; }

        public ICollection<StudentRecord> Students { get; set; } = [];
    }
}

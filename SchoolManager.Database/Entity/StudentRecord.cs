namespace SchoolManager.Database.Entity
{
    public class StudentRecord
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;

        public Guid? GroupId { get; set; }
        public GroupRecord? Group { get; set; }
    }
}

using SchoolManager.Database.Database;
using SchoolManager.Database.Entity;

namespace SchoolManager.Database.Services
{
    public class GroupService(SchoolDbContext db)
    {
        private readonly SchoolDbContext _db = db;

        public void AddGroupRecord(GroupRecord groupRecord) => _db.Groups.Add(groupRecord);

        public List<GroupRecord> GetGroups() => [.. _db.Groups];

        public bool UpdateGroup(GroupRecord groupRecord)
        {
            var existingGroup = _db.Groups.FirstOrDefault(g => g.Id == groupRecord.Id);
            if (existingGroup != null)
            {
                existingGroup.Name = groupRecord.Name;
                existingGroup.CourseId = groupRecord.CourseId;
                existingGroup.Course = groupRecord.Course;
                existingGroup.Students = groupRecord.Students;
                _db.SaveChanges();
                return true;
            }
            return false;
        }


        public bool DeleteGroup(GroupRecord groupRecord)
        {
            if (_db.Groups.Any(g => g.Id == groupRecord.Id))
            {
                _db.Groups.Remove(_db.Groups.First(g => g.Id == groupRecord.Id));
                return true;
            }
            return false;
        }
    }
}

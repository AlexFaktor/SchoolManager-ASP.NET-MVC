using Microsoft.EntityFrameworkCore;
using SchoolManager.Database.Entity;
using SchoolManager.Resources.Interface;

namespace SchoolManager.Database.Services
{
    public class GroupService(SchoolDbContext db) : IEntityService
    {
        private readonly SchoolDbContext _db = db;

        public void AddGroupRecord(GroupRecord groupRecord)
        {
            _db.Groups.Add(groupRecord);
            _db.SaveChanges();
        }

        public List<GroupRecord> GetGroups() => [.. _db.Groups];
        public List<GroupRecord> GetGroups(Guid courseId) => [.. _db.Groups.Where(g => g.CourseId == courseId)];

        public GroupRecord? GetGroup(Guid id) => _db.Groups.FirstOrDefault(g => g.Id == id);

        public async Task<List<GroupRecord>> GetGroupsAsync() => await _db.Groups.ToListAsync();

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

        public bool DeleteGroup(Guid id)
        {
            if (_db.Groups.Any(g => g.Id == id))
            {
                _db.Groups.Remove(_db.Groups.First(g => g.Id == id));
                return true;
            }
            return false;
        }
    }
}

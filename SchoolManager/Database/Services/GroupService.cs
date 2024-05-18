using Microsoft.EntityFrameworkCore;
using SchoolManager.Database.Entity;
using SchoolManager.Resources.Interface;

namespace SchoolManager.Database.Services
{
    public class GroupService : IEntityServiceExtended<GroupRecord>
    {
        private readonly SchoolDbContext _db;

        public GroupService(SchoolDbContext db)
        {
            _db = db;
        }

        public bool Add(GroupRecord groupRecord)
        {
            if (Get(groupRecord.Name) != null)
                return false;

            _db.Groups.Add(groupRecord);
            _db.SaveChanges();
            return true;
        }

        public List<GroupRecord> GetAll() => _db.Groups.ToList();

        public List<GroupRecord> GetAll(Guid ownerId) => _db.Groups.Where(s => s.CourseId == ownerId).ToList();

        public async Task<List<GroupRecord>> GetAllAsync() => await _db.Groups.ToListAsync();

        public GroupRecord? Get(Guid id) => _db.Groups.FirstOrDefault(g => g.Id == id);

        public GroupRecord? Get(string name) => _db.Groups.FirstOrDefault(g => g.Name == name);

        public bool Update(GroupRecord groupRecord)
        {
            if (Get(groupRecord.Name) != null)
                return false;

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

        public bool Delete(Guid id)
        {
            var group = _db.Groups.FirstOrDefault(g => g.Id == id);
            if (group != null)
            {
                _db.Groups.Remove(group);
                _db.SaveChanges();
                return true;
            }
            return false;
        }

        public bool Delete(GroupRecord groupRecord)
        {
            return Delete(groupRecord.Id);
        }
    }
}

using SchoolManager.Database.Entity;

namespace SchoolManager.Models.SchoolModels
{
    public class GroupVM(GroupRecord record)
    {
        public GroupRecord Record { get; } = record;
    }
}

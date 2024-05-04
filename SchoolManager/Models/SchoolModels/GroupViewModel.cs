using SchoolManager.Database.Entity;

namespace SchoolManager.Models.SchoolModels
{
    public class GroupViewModel(GroupRecord record)
    {
        GroupRecord Record { get; } = record;
    }
}

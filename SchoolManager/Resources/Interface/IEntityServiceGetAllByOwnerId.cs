using SchoolManager.Database.Entity.Base;

namespace SchoolManager.Resources.Interface
{
    public interface IEntityServiceGetAllByOwnerId<T> where T : SchoolRecord
    {
        List<T> GetAll(Guid ownerId);
    }
}

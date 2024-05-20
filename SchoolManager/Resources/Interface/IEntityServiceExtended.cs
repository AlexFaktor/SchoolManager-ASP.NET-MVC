using SchoolManager.Database.Entity.Base;

namespace SchoolManager.Resources.Interface
{
    public interface IEntityServiceExtended<T> : IEntityService<T>, IEntityServiceGetAllByOwnerId<T> where T : SchoolRecord 
    {
    }
}

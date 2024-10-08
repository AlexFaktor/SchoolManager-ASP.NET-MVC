﻿using SchoolManager.Database.Entity.Base;

namespace SchoolManager.Resources.Interface
{
    public interface IEntityService<T> where T : SchoolRecord
    {
        bool Add(T entity);
        List<T> GetAll();
        Task<List<T>> GetAllAsync();
        T? Get(Guid id);
        T? Get(string name);
        bool Update(T entity);
        bool Delete(Guid id);
        bool Delete(T entity);
    }
}

﻿using N5Domain.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace N5Domain.Repositories
{
    public interface IRepository<T> where T : Entity
    {
        Task<T> GetById(int id);
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);
    }
}
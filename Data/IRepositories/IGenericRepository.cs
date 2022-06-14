using BankNTProject.Domain.Entities;
using System;
using System.Collections.Generic;

namespace BankNTProject.Data.IRepositories
{
    public interface IGenericRepository<TSource> where TSource : Auditable
    {
        TSource Create(TSource entity);
        TSource Get(Func<TSource, bool> predicate);
        TSource Update(Guid id, TSource entity);
        bool Delete(Guid id);
        IEnumerable<TSource> GetAll();
    }
}

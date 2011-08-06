using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BetTeamsBattle.Data.Repositories.Base
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> Get(LinqSpec<T> filterSpecification);
        bool Exists(LinqSpec<T> filterSpecification);
        void Add(T entity);
        void SaveChanges();
    }
}
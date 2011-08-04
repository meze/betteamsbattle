using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LinqSpecs;

namespace BetTeamsBattle.Data.Repositories.Base
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> Get(Specification<T> filterSpecification);
        bool Exists(Specification<T> filterSpecification);
        void Add(T entity);
        void SaveChanges();
    }
}
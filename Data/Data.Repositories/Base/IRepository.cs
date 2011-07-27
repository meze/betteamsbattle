using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LinqSpecs;

namespace BetTeamsBattle.Data.Repositories.Base
{
    public interface IRepository<T>
    {
        IQueryable<T> FindAll(Specification<T> filterSpecification);
        void Save();
    }
}
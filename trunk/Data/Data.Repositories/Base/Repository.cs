using System.Collections.Generic;
using System.Linq;
using BetTeamsBattle.Data.Model;
using BetTeamsBattle.Data.Repositories.Base.Interfaces;
using BetTeamsBattle.Data.Repositories.ContextScope;
using Ninject;

namespace BetTeamsBattle.Data.Repositories.Base
{
    //ToDo: put here a link to an article why IQueryable-based (limited to Select, OrderBy and ToList/Single/First) approach was chosen
    internal class Repository<T> : ContextManagerBase, IRepository<T> where T : class
    {
        public IQueryable<T> Get(LinqSpec<T> filterSpecification)
        {
            return Context.CreateObjectSet<T>().Where(filterSpecification);
        }

        public bool Exists(LinqSpec<T> filterSpecification)
        {
            return Context.CreateObjectSet<T>().Where(filterSpecification).Any();
        }

        public void Add(T entity)
        {
            Context.CreateObjectSet<T>().AddObject(entity);
        }
    }
}
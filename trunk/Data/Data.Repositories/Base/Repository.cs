using System.Collections.Generic;
using System.Linq;
using BetTeamsBattle.Data.Model;
using BetTeamsBattle.Data.Repositories.Base.Interfaces;
using Ninject;

namespace BetTeamsBattle.Data.Repositories.Base
{
    //ToDo: put here a link to an article why IQueryable-based (limited to Select, OrderBy and ToList/Single/First) approach was chosen
    internal class Repository<T> : ContextManagerBase, IRepository<T> where T : class
    {
        public IQueryable<T> All()
        {
            return Context.CreateObjectSet<T>();
        }

        public IQueryable<T> Get(LinqSpec<T> filterSpecification)
        {
            return Context.CreateObjectSet<T>().Where(filterSpecification);
        }

        public T Single(LinqSpec<T> filterSpecification)
        {
            return Get(filterSpecification).Single();
        }

        public T SingleOrDefault(LinqSpec<T> filterSpecification)
        {
            return Get(filterSpecification).SingleOrDefault();
        }

        public T First(LinqSpec<T> filterSpecification)
        {
            return Get(filterSpecification).First();
        }

        public T FirstOrDefault(LinqSpec<T> filterSpecification)
        {
            return Get(filterSpecification).FirstOrDefault();
        }

        public bool Exists(LinqSpec<T> filterSpecification)
        {
            return Get(filterSpecification).Any();
        }

        public void Add(T entity)
        {
            Context.CreateObjectSet<T>().AddObject(entity);
        }
    }
}
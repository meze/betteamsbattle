using System.Collections.Generic;
using System.Linq;
using BetTeamsBattle.Data.Model;
using LinqSpecs;
using Ninject;

namespace BetTeamsBattle.Data.Repositories.Base
{
    //ToDo: put here a link to an article why IQueryable-based (limited to Select, OrderBy and ToList/Single/First) approach was chosen
    internal class Repository<T> : IRepository<T> where T : class
    {
        [Inject]
        public ModelContext Context { get; set; }

        public IQueryable<T> Get(Specification<T> filterSpecification)
        {
            return Context.CreateObjectSet<T>().Where(filterSpecification.IsSatisfiedBy());
        }

        public bool Exists(Specification<T> filterSpecification)
        {
            return Context.CreateObjectSet<T>().Where(filterSpecification.IsSatisfiedBy()).Any();
        }

        public void Add(T entity)
        {
            Context.CreateObjectSet<T>().AddObject(entity);
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }
    }
}
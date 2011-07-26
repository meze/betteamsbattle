using System.Collections.Generic;
using System.Linq;
using BetTeamsBattle.Data.Model;
using LinqSpecs;

namespace BetTeamsBattle.Data.Repositories.Base
{
    //ToDo: put here a link to an article why IQueryable-based (limited to Select, OrderBy and ToList/Single/First) approach was chosen
    internal class Repository<T> : IRepository<T> where T : class
    {
        protected ModelContext Context { get; set; }

        public IQueryable<T> All(Specification<T> filterSpecification)
        {
            return Context.CreateObjectSet<T>().Where(filterSpecification.IsSatisfiedBy());
        }

        public void Save()
        {
            Context.SaveChanges();
        }
    }
}
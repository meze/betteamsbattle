using System.Collections.Generic;
using System.Linq;
using BetTeamsBattle.Data.Model;
using BetTeamsBattle.Data.Repositories.Interfaces;
using LinqSpecs;

namespace BetTeamsBattle.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected ModelContext Context { get; set; }

        public T Single(Specification<T> filterSpecification)
        {
            return Context.CreateObjectSet<T>().Where(filterSpecification.IsSatisfiedBy()).Single();
        }

        public IEnumerable<T> GetEnumerable(Specification<T> filterSpecification)
        {
            return Context.CreateObjectSet<T>().Where(filterSpecification.IsSatisfiedBy()).AsEnumerable();
        }

        public void Save()
        {
            Context.SaveChanges();
        }
    }
}
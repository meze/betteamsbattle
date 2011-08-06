using System.Collections.Generic;
using System.Linq;
using BetTeamsBattle.Data.Model;
using Ninject;

namespace BetTeamsBattle.Data.Repositories.Base
{
    //ToDo: put here a link to an article why IQueryable-based (limited to Select, OrderBy and ToList/Single/First) approach was chosen
    internal class Repository<T> : IRepository<T> where T : class
    {
        private ModelContext _context;
        [Inject]
        public ModelContext Context
        {
            get
            {
                var currentContext = ContextScope.ContextScope.CurrentContext;
                if (currentContext != null)
                    return currentContext;
                return _context;
            }
            set { _context = value; }
        }

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

        public void SaveChanges()
        {
            Context.SaveChanges();
        }
    }
}
using System.Linq;

namespace BetTeamsBattle.Data.Repositories.Base.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> All();
        IQueryable<T> Get(LinqSpec<T> filterSpecification);
        T Single(LinqSpec<T> filterSpecification);
        T SingleOrDefault(LinqSpec<T> filterSpecification);
        T First(LinqSpec<T> filterSpecification);
        T FirstOrDefault(LinqSpec<T> filterSpecification);
        bool Any(LinqSpec<T> filterSpecification);
        void Add(T entity);
    }
}
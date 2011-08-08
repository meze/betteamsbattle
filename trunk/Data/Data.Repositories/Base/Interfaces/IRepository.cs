using System.Linq;

namespace BetTeamsBattle.Data.Repositories.Base.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> Get(LinqSpec<T> filterSpecification);
        bool Exists(LinqSpec<T> filterSpecification);
        void Add(T entity);
    }
}
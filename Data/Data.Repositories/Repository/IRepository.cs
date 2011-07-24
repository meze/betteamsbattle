using System.Collections.Generic;
using LinqSpecs;

namespace BetTeamsBattle.Data.Repositories.Interfaces
{
    public interface IRepository<T>
    {
        T Single(Specification<T> filterSpecification);
        IEnumerable<T> GetEnumerable(Specification<T> filterSpecification);
        void Save();
    }
}
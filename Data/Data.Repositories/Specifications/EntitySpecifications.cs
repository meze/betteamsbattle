using System.Collections.Generic;
using System.Linq;
using BetTeamsBattle.Data.Model.Interfaces;

namespace BetTeamsBattle.Data.Repositories.Specifications
{
    public class EntitySpecifications
    {
        public static LinqSpec<T> EntityIdIsEqualTo<T>(long id) where T : IEntity
        {
            return LinqSpec.For<T>(e => e.Id == id).Flatten<T, IEntity>();
        }

        public static LinqSpec<T> EntityIdIsContainedIn<T>(IEnumerable<long> ids) where T : IEntity
        {
            return LinqSpec.For<T>(e => ids.Contains(e.Id)).Flatten<T, IEntity>();
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using BetTeamsBattle.Data.Model.Interfaces;
using BetTeamsBattle.Data.Repositories;

namespace BetTeamsBattle.Data.Model.Specifications
{
    public class EntitySpecifications
    {
        public static LinqSpec<T> IdIsEqualTo<T>(long id) where T : IEntity
        {
            return LinqSpec.For<T>(e => e.Id == id).Flatten<T, IEntity>();
        }

        public static LinqSpec<T> IdIsContainedIn<T>(IEnumerable<long> ids) where T : IEntity
        {
            return LinqSpec.For<T>(e => ids.Contains(e.Id)).Flatten<T, IEntity>();
        }

        public static LinqSpec<T> IdIsNotContainedIn<T>(IEnumerable<long> ids) where T : IEntity
        {
            return LinqSpec.For<T>(e => !ids.Contains(e.Id)).Flatten<T, IEntity>();
        }
    }
}
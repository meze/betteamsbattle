using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BetTeamsBattle.Data.Model.Interfaces;
using BetTeamsBattle.Data.Repositories;

namespace BetTeamsBattle.Data.Repositories.Specific.Entity
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
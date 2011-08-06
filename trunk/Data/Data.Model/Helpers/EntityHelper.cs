using System.Linq;
using System.Collections.Generic;
using BetTeamsBattle.Data.Model.Interfaces;

namespace BetTeamsBattle.Data.Model.Helpers
{
    public class EntityHelper
    {
         public static long GetId(IEntity entity)
         {
             return entity.Id;
         }

        public static IEnumerable<long> GetIds(IEnumerable<IEntity> entities)
        {
            return entities.Select(e => e.Id).AsEnumerable();
        }
    }
}
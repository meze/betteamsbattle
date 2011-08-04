using BetTeamsBattle.Data.Model.Interfaces;
using LinqSpecs;

namespace BetTeamsBattle.Data.Repositories.Specifications
{
    public class EntitySpecifications
    {
         public static Specification<T> EntityIdIsEqual<T>(long id) where T : IEntity
         {
             return new AdHocSpecification<T>(e => e.Id == id);
         }
    }
}
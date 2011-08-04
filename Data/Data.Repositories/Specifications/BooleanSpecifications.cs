using LinqSpecs;

namespace BetTeamsBattle.Data.Repositories.Specifications
{
    public class BooleanSpecifications<T>
    {
         public static Specification<T> True()
         {
             return new AdHocSpecification<T>(e => true);
         }
    }
}
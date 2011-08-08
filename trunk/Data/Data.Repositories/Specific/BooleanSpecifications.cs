namespace BetTeamsBattle.Data.Repositories.Specific
{
    public class BooleanSpecifications<T>
    {
         public static LinqSpec<T> True()
         {
             return LinqSpec.For<T>(e => true);
         }
    }
}
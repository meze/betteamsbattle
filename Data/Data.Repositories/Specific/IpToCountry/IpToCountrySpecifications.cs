namespace BetTeamsBattle.Data.Repositories.Specific.IpToCountry
{
    public class IpToCountrySpecifications
    {
        public static LinqSpec<Model.Entities.IpToCountry> IpIsInRange(long ipNumber)
        {
            return LinqSpec.For<Model.Entities.IpToCountry>(ip => ipNumber >= ip.IpStart && ipNumber <= ip.IpEnd);
        }
    }
}
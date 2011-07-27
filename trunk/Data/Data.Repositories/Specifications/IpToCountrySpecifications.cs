using BetTeamsBattle.Data.Model.Entities;
using LinqSpecs;

namespace BetTeamsBattle.Data.Repositories.Specifications
{
    public class IpToCountrySpecifications
    {
        public static Specification<IpToCountry> IpIsInRange(long ipNumber)
        {
            return new AdHocSpecification<IpToCountry>(ip => ipNumber >= ip.IpStart && ipNumber <= ip.IpEnd);
        }
    }
}
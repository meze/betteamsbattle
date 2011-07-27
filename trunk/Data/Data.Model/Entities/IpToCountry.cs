using BetTeamsBattle.Data.Model.Interfaces;

namespace BetTeamsBattle.Data.Model.Entities
{
    public class IpToCountry : IEntity
    {
        public virtual long Id { get; set; }
        public virtual long IpEnd { get; set; }
        public virtual long IpStart { get; set; }
        public virtual string CountryName { get; set; }
        public virtual string CountryCode { get; set; }
        public virtual string LongCountryCode { get; set; }
    }
}
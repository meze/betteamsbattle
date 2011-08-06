﻿using BetTeamsBattle.Data.Model.Entities;

namespace BetTeamsBattle.Data.Repositories.Specifications
{
    public class IpToCountrySpecifications
    {
        public static LinqSpec<IpToCountry> IpIsInRange(long ipNumber)
        {
            return LinqSpec.For<IpToCountry>(ip => ipNumber >= ip.IpStart && ipNumber <= ip.IpEnd);
        }
    }
}
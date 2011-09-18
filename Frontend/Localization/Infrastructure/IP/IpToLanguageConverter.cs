#region

using System.Linq;
using System.Collections.Generic;
using BetTeamsBattle.Data.Model.Entities;
using BetTeamsBattle.Data.Model.Enums;
using BetTeamsBattle.Data.Model.Specifications;
using BetTeamsBattle.Data.Repositories.Base;
using BetTeamsBattle.Data.Repositories.Base.Interfaces;
using BetTeamsBattle.Frontend.Localization.Infrastructure.IP.Interfaces;

#endregion

namespace BetTeamsBattle.Frontend.Localization.Infrastructure.IP
{
   public class IpToLanguageConverter : IIpToLanguageConverter
   {
      private readonly IDictionary<string, Language> _countryCodeMappings = new Dictionary<string, Language>
            {
                {"ru", Language.Russian}, //Russia
                {"by", Language.Russian}, //Belarussia
                {"ua", Language.Russian}, //Ukraine
                {"ge", Language.Russian}, //Georgia
                {"tm", Language.Russian}, //Turkmenistan
                {"uz", Language.Russian}, //Uzbekistan
                {"tj", Language.Russian}, //Tajikistan
                {"az", Language.Russian}, //Azerbaijan
                {"am", Language.Russian}, //Armenia
                {"kz", Language.Russian}, //Kazakhstan
                {"kg", Language.Russian}, //Kyrgyzstan
                {"md", Language.Russian}, //Moldova
                {"lt", Language.Russian}, //Lithuania
                {"lv", Language.Russian}, //Latvia
                {"ee", Language.Russian}  //Estonia
            };

      private readonly Language _defaultLanguage = Language.English;

      private readonly IIpToNumberConverter _ipToNumberConverter;
      private readonly IRepository<IpToCountry> _repositoryOfIpToCountry;

      public IpToLanguageConverter(IIpToNumberConverter ipToNumberConverter, IRepository<IpToCountry> repositoryOfIpToCountry)
      {
          _ipToNumberConverter = ipToNumberConverter;
          _repositoryOfIpToCountry = repositoryOfIpToCountry;
      }

       public Language GetLanguage(string ipAddress)
      {
         long ipNumber = _ipToNumberConverter.IpToNumber(ipAddress);
         string countryCode = _repositoryOfIpToCountry.Get(IpToCountrySpecifications.IpIsInRange(ipNumber)).Select(ip => ip.CountryCode).SingleOrDefault();

         if (countryCode == null)
            return _defaultLanguage;

            return _countryCodeMappings.ContainsKey(countryCode)
                       ? _countryCodeMappings[countryCode]
                       : _defaultLanguage;
      }
   }
}
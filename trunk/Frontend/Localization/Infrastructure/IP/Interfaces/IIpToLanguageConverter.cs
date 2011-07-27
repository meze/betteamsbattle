#region



#endregion

using BetTeamsBattle.Data.Model.Entities;
using BetTeamsBattle.Data.Model.Enums;

namespace BetTeamsBattle.Frontend.Localization.Infrastructure.IP.Interfaces
{
    public interface IIpToLanguageConverter
    {
        Language GetLanguage(string ipAddress);
    }
}
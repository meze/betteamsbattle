#region



#endregion

using BetTeamsBattle.Data.Model.Entities;

namespace BetTeamsBattle.Frontend.Localization.Infrastructure.IP.Interfaces
{
    public interface IIpToLanguageConverter
    {
        Language GetLanguage(string ipAddress);
    }
}
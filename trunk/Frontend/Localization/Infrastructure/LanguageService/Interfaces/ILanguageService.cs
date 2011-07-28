#region



#endregion

using System.Web;
using BetTeamsBattle.Data.Model.Entities;
using BetTeamsBattle.Data.Model.Enums;

namespace BetTeamsBattle.Frontend.Localization.Infrastructure.LanguageService.Interfaces
{
    public interface ILanguageService
    {
        Language ProccessLanguageForRequest(User user, HttpRequest request, HttpResponse response);
    }
}
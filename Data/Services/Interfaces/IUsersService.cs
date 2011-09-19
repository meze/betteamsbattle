using BetTeamsBattle.Data.Model.Enums;

namespace BetTeamsBattle.Data.Services.Interfaces
{
    public interface IUsersService
    {
        long Register(string login, string openIdUrl, Language language);
    }
}
using BetTeamsBattle.Data.Model.Enums;

namespace BetTeamsBattle.Data.Services.Interfaces
{
    public interface IUsersService
    {
        void Register(string login, string openIdUrl, Language language);
    }
}
using System.Collections.Generic;

namespace BetTeamsBattle.Data.Services.Interfaces
{
    public interface ITeamsService
    {
        long CreateProTeam(string title, string description, string site, IEnumerable<long> usersIds);
    }
}
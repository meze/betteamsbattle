using System.Collections.Generic;
using BetTeamsBattle.Frontend.Areas.NotAdmin.Models.Teams;

namespace BetTeamsBattle.Frontend.Areas.NotAdmin.ViewServices.Battles.Interfaces
{
    public interface ITeamsViewService
    {
        IEnumerable<TopTeamViewModel> TopTeams();
        IEnumerable<TopTeamViewModel> BattleTopTeams(long battleId);
    }
}
using System.Collections.Generic;
using BetTeamsBattle.Frontend.Areas.NotAdmin.Models.Battles;
using BetTeamsBattle.Frontend.Areas.NotAdmin.Models.Teams;

namespace BetTeamsBattle.Frontend.Areas.NotAdmin.ViewServices.Battles.Interfaces
{
    public interface IBattlesViewService
    {
        BattleViewModel Battle(long battleId, long? nullableUserId);
        AllBattlesViewModel AllBattles();
    }
}
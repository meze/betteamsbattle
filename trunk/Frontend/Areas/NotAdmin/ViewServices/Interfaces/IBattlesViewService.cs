using System.Collections.Generic;
using BetTeamsBattle.Frontend.Areas.NotAdmin.Models.Battle;

namespace BetTeamsBattle.Frontend.Areas.NotAdmin.ViewServices.Battles.Interfaces
{
    public interface IBattlesViewService
    {
        BattleViewModel Battle(long battleId, long? nullableUserId);
        AllBattlesViewModel AllBattles();
        IEnumerable<BattleTopUsersUserViewModel> BattleTopUsers(long battleId);
    }
}
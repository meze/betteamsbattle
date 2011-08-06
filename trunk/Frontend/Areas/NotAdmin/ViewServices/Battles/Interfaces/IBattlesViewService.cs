using System.Collections.Generic;
using BetTeamsBattle.Frontend.Areas.NotAdmin.Models.Battle;

namespace BetTeamsBattle.Frontend.Areas.NotAdmin.ViewServices.Battles.Interfaces
{
    public interface IBattlesViewService
    {
        IEnumerable<ActualBattleViewModel> ActualBattlesViewModels(long? userId);
    }
}
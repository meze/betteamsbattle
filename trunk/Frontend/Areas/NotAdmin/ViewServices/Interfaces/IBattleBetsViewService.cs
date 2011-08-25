using System.Collections.Generic;
using BetTeamsBattle.Data.Model.Entities;

namespace BetTeamsBattle.Frontend.Areas.NotAdmin.ViewServices.Battles.Interfaces
{
    public interface IBattleBetsViewService
    {
        IEnumerable<BattleBet> MyBets(long battleId, long userId);
    }
}
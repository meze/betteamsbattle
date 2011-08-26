using System.Collections.Generic;
using BetTeamsBattle.Data.Model.Entities;
using BetTeamsBattle.Frontend.Areas.NotAdmin.Models.BattleBets;

namespace BetTeamsBattle.Frontend.Areas.NotAdmin.ViewServices.Battles.Interfaces
{
    public interface IBattleBetsViewService
    {
        IEnumerable<BattleBet> MyBets(long battleId, long userId);
        MakeBetViewModel MakeBet(long battleId, long userId, MakeBetFormViewModel makeBetFormViewModel);
        MakeBetViewModel MakeBet(long battleId, long userId);
    }
}
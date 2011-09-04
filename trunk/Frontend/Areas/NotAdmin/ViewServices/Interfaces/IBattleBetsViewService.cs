using System.Collections.Generic;
using BetTeamsBattle.Data.Model.Entities;
using BetTeamsBattle.Frontend.Areas.NotAdmin.Models.BattleBets;

namespace BetTeamsBattle.Frontend.Areas.NotAdmin.ViewServices.Battles.Interfaces
{
    public interface IBattleBetsViewService
    {
        MakeBetViewModel MakeBet(long battleId, long userId, MakeBetFormViewModel makeBetFormViewModel);
        MakeBetViewModel MakeBet(long battleId, long userId);
        IEnumerable<MyBetViewModel> MyBattleBets(long battleId, long userId);
        IEnumerable<MyBetViewModel> TeamBets(long teamId);
        IEnumerable<MyBetViewModel> UserBets(long userId);
    }
}
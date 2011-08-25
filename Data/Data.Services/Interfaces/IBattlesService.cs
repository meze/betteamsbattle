using System;
using BetTeamsBattle.Data.Model.Entities;
using BetTeamsBattle.Data.Model.Enums;

namespace BetTeamsBattle.Data.Services.Interfaces
{
    public interface IBattlesService
    {
        long CreateBattle(DateTime startDate, DateTime endDate, BattleType battleType, int budget);
        long MakeBet(long battleId, long teamId, long userId, string title, double bet, double coefficient, string url, bool isPrivate);
        void BetSucceeded(long battleBetId, long userId, out long battleId);
        void BetFailed(long battleBetId, long userId, out long battleId);
    }
}
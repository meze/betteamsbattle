using System;
using BetTeamsBattle.Data.Model.Enums;

namespace BetTeamsBattle.Data.Services.Interfaces
{
    public interface IBattlesService
    {
        void CreateBattle(DateTime startDate, DateTime endDate, BattleType battleType, int budget);
        void JoinToBattle(long battleId, long userId);
        void LeaveBattle(long battleId, long userId);
        long MakeBet(long battleId, long userId, string title, double bet, double coefficient, string url, bool isPrivate);
        void BetSucceeded(long battleBetId, long userId, out long battleId);
        void BetFailed(long battleBetId, long userId, out long battleId);
        bool UserIsJoinedToBattle(long userId, long battleId);
    }
}
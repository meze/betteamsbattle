using System;
using BetTeamsBattle.Data.Model.Enums;

namespace BetTeamsBattle.Data.Services.Interfaces
{
    public interface IBattlesService
    {
        void CreateBattle(DateTime startDate, DateTime endDate, BattleType battleType, int budget);
        void JoinToBattle(long battleId, long userId);
        void LeaveBattle(long battleId, long userId);
        long OpenBattleBet(long battleId, long userId, double bet, double coefficient, string url);
        void CloseBattleBetAsSucceeded(long battleBetId, long userId, out long battleId);
        void CloseBattleBetAsFailed(long battleBetId, long userId, out long battleId);
        bool UserIsJoinedToBattle(long userId, long battleId);
    }
}
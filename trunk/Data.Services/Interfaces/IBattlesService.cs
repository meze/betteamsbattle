using System;
using BetTeamsBattle.Data.Model.Enums;

namespace BetTeamsBattle.Data.Services.Interfaces
{
    public interface IBattlesService
    {
        void Create(DateTime startDate, DateTime endDate, BattleType battleType, int budget);
    }
}
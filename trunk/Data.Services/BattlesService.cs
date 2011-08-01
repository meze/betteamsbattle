using System;
using BetTeamsBattle.Data.Model.Entities;
using BetTeamsBattle.Data.Model.Enums;
using BetTeamsBattle.Data.Repositories.Base;
using BetTeamsBattle.Data.Services.Interfaces;

namespace BetTeamsBattle.Data.Services
{
    internal class BattlesService : IBattlesService
    {
        private readonly IRepository<Battle> _repositoryOfBattles;

        public BattlesService(IRepository<Battle> repositoryOfBattles)
        {
            _repositoryOfBattles = repositoryOfBattles;
        }

        public void Create(DateTime startDate, DateTime endDate, BattleType battleType, int budget)
        {
            var battle = new Battle(startDate, endDate, battleType, budget);

            _repositoryOfBattles.Add(battle);

            _repositoryOfBattles.SaveChanges();
        }
    }
}
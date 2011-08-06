using System;
using BetTeamsBattle.Data.Model.Entities;
using BetTeamsBattle.Data.Model.Enums;
using BetTeamsBattle.Data.Repositories.Base;
using BetTeamsBattle.Data.Repositories.ContextScope;
using BetTeamsBattle.Data.Services.Interfaces;

namespace BetTeamsBattle.Data.Services
{
    internal class BattlesService : IBattlesService
    {
        private readonly IRepository<Battle> _repositoryOfBattles;
        private readonly IRepository<BattleUser> _repositoryOfBattlesUsers;

        public BattlesService(IRepository<Battle> repositoryOfBattles, IRepository<BattleUser> repositoryOfBattlesUsers)
        {
            _repositoryOfBattles = repositoryOfBattles;
            _repositoryOfBattlesUsers = repositoryOfBattlesUsers;
        }

        public void CreateBattle(DateTime startDate, DateTime endDate, BattleType battleType, int budget)
        {
            using (var contextScope = new ContextScope())
            {
                var battle = new Battle(startDate, endDate, battleType, budget);

                _repositoryOfBattles.Add(battle);

                contextScope.SaveChanges();
            }
        }

        public void JoinToBattle(long battleId, long userId)
        {
            using (var contextScope = new ContextScope())
            {
                var battleUser = new BattleUser(battleId, userId, BattleAction.Join);

                _repositoryOfBattlesUsers.Add(battleUser);

                contextScope.SaveChanges();
            }
        }

        public void LeaveBattle(long battleId, long userId)
        {
            using (var contextScope = new ContextScope())
            {
                var battleUser = new BattleUser(battleId, userId, BattleAction.Leave);

                _repositoryOfBattlesUsers.Add(battleUser);

                contextScope.SaveChanges();
            }
        }
    }
}
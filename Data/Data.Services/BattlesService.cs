using System;
using System.Transactions;
using BetTeamsBattle.Data.Model.Entities;
using BetTeamsBattle.Data.Model.Enums;
using BetTeamsBattle.Data.Repositories.Base;
using BetTeamsBattle.Data.Repositories.Base.Interfaces;
using BetTeamsBattle.Data.Repositories.UnitOfWork;
using BetTeamsBattle.Data.Services.Interfaces;

namespace BetTeamsBattle.Data.Services
{
    internal class BattlesService : IBattlesService
    {
        private readonly IRepository<Battle> _repositoryOfBattles;
        private readonly IRepository<BattleUser> _repositoryOfBattlesUsers;
        private readonly IRepository<BattleBet> _repositoryOfBattleBet;
        private readonly IRepository<QueuedBetUrl> _repositoryOfQueuedBetUrl;

        public BattlesService(IRepository<Battle> repositoryOfBattles, IRepository<BattleUser> repositoryOfBattlesUsers, IRepository<BattleBet> repositoryOfBattleBet, IRepository<QueuedBetUrl> repositoryOfQueuedBetUrl)
        {
            _repositoryOfBattles = repositoryOfBattles;
            _repositoryOfBattlesUsers = repositoryOfBattlesUsers;
            _repositoryOfBattleBet = repositoryOfBattleBet;
            _repositoryOfQueuedBetUrl = repositoryOfQueuedBetUrl;
        }

        public void CreateBattle(DateTime startDate, DateTime endDate, BattleType battleType, int budget)
        {
            using (var contextScope = new UnitOfWorkScope())
            {
                var battle = new Battle(startDate, endDate, battleType, budget);

                _repositoryOfBattles.Add(battle);

                contextScope.SaveChanges();
            }
        }

        public void JoinToBattle(long battleId, long userId)
        {
            using (var contextScope = new UnitOfWorkScope())
            {
                var battleUser = new BattleUser(battleId, userId, BattleUserAction.Join);

                _repositoryOfBattlesUsers.Add(battleUser);

                contextScope.SaveChanges();
            }
        }

        public void LeaveBattle(long battleId, long userId)
        {
            using (var contextScope = new UnitOfWorkScope())
            {
                var battleUser = new BattleUser(battleId, userId, BattleUserAction.Leave);

                _repositoryOfBattlesUsers.Add(battleUser);

                contextScope.SaveChanges();
            }
        }

        public void MakeBattleBet(long battleId, long userId, double bet, double coefficient, string url)
        {
            using (var transactionScope = new TransactionScope())
            {
                using (var contextScope = new UnitOfWorkScope())
                {
                    var battleBet = new BattleBet(battleId, userId, bet, coefficient, url);
                    _repositoryOfBattleBet.Add(battleBet);
                    contextScope.SaveChanges();

                    var queuedBetUrl = new QueuedBetUrl(battleBet.Id, url, QueuedBetUrlType.Open);
                    _repositoryOfQueuedBetUrl.Add(queuedBetUrl);
                    contextScope.SaveChanges();
                }

                transactionScope.Complete();
            }
        }
    }
}
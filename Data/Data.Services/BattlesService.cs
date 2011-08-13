using System;
using System.Linq;
using System.Transactions;
using BetTeamsBattle.Data.Model.Entities;
using BetTeamsBattle.Data.Model.Enums;
using BetTeamsBattle.Data.Repositories.Base;
using BetTeamsBattle.Data.Repositories.Base.Interfaces;
using BetTeamsBattle.Data.Repositories.EntityRepositories.Interfaces;
using BetTeamsBattle.Data.Repositories.Infrastructure.TransactionScope.Interfaces;
using BetTeamsBattle.Data.Repositories.Specifications;
using BetTeamsBattle.Data.Repositories.UnitOfWork;
using BetTeamsBattle.Data.Repositories.UnitOfWork.Interfaces;
using BetTeamsBattle.Data.Services.Interfaces;

namespace BetTeamsBattle.Data.Services
{
    internal class BattlesService : IBattlesService
    {
        private readonly ITransactionScopeFactory _transactionScopeFactory;
        private readonly IRepository<Battle> _repositoryOfBattles;
        private readonly IRepository<BattleBet> _repositoryOfBattleBet;
        private readonly IRepository<QueuedBetUrl> _repositoryOfQueuedBetUrl;
        private readonly IRepository<BattleUserStatistics> _repositoryOfBattleUserStatistics;
        private readonly IBattleUserRepository _battleUserRepository;
        private readonly IUnitOfWorkScopeFactory _unitOfWorkScopeFactory;

        public BattlesService(ITransactionScopeFactory transactionScopeFactory, IUnitOfWorkScopeFactory unitOfWorkScopeFactory, IRepository<Battle> repositoryOfBattles, IRepository<BattleBet> repositoryOfBattleBet, IRepository<QueuedBetUrl> repositoryOfQueuedBetUrl, IRepository<BattleUserStatistics> repositoryOfBattleUserStatistics, IBattleUserRepository battleUserRepository)
        {
            _transactionScopeFactory = transactionScopeFactory;
            _repositoryOfBattles = repositoryOfBattles;
            _repositoryOfBattleBet = repositoryOfBattleBet;
            _repositoryOfQueuedBetUrl = repositoryOfQueuedBetUrl;
            _repositoryOfBattleUserStatistics = repositoryOfBattleUserStatistics;
            _battleUserRepository = battleUserRepository;
            _unitOfWorkScopeFactory = unitOfWorkScopeFactory;
        }

        public void CreateBattle(DateTime startDate, DateTime endDate, BattleType battleType, int budget)
        {
            using (var unitOfWorkScope = _unitOfWorkScopeFactory.Create())
            {
                var battle = new Battle(startDate, endDate, battleType, budget);
                _repositoryOfBattles.Add(battle);
                unitOfWorkScope.SaveChanges();
            }
        }

        public void JoinToBattle(long battleId, long userId)
        {
            using (var unitOfWorkScope = _unitOfWorkScopeFactory.Create())
            {
                var battleUser = new BattleUser(battleId, userId, BattleUserAction.Join);
                _battleUserRepository.Add(battleUser);

                var battleUserStatisticsExists =
                    _repositoryOfBattleUserStatistics.Get(
                        BattleUserStatisticsSpecifications.BattleIdAndUserIdAreEqualTo(battleId, userId)).Any();
                if (!battleUserStatisticsExists)
                {
                    var battleBudget = _repositoryOfBattles.Get(EntitySpecifications.EntityIdIsEqualTo<Battle>(battleId)).Select(b => b.Budget).Single();

                    var battleUserStatistics = new BattleUserStatistics(battleId, userId, battleBudget);
                    _repositoryOfBattleUserStatistics.Add(battleUserStatistics);
                }

                unitOfWorkScope.SaveChanges();
            }
        }

        public void LeaveBattle(long battleId, long userId)
        {
            using (var unitOfWorkScope = _unitOfWorkScopeFactory.Create())
            {
                var battleUser = new BattleUser(battleId, userId, BattleUserAction.Leave);
                _battleUserRepository.Add(battleUser);
                unitOfWorkScope.SaveChanges();
            }
        }

        public bool UserIsJoinedToBattle(long userId, long battleId)
        {
            var lastBattleUserAction = _battleUserRepository.Get(BattleUserSpecifications.BattleIdAndUserIdAreEqualTo(battleId, userId)).OrderByDescending(bu => bu.Id).Select(bu => bu.Action).FirstOrDefault();
            if (lastBattleUserAction == default(sbyte) || lastBattleUserAction == (sbyte)BattleUserAction.Leave)
                return false;
            return true;
        }

        public long OpenBattleBet(long battleId, long userId, double bet, double coefficient, string url)
        {
            using (var unitOfWorkScope = _unitOfWorkScopeFactory.Create())
            {
                if (!UserIsJoinedToBattle(userId, battleId))
                    throw new ArgumentException("User is not joined to the battle");

                var battleBet = new BattleBet(battleId, userId, bet, coefficient, url);
                
                var battleUserStatistics = _repositoryOfBattleUserStatistics.Get(BattleUserStatisticsSpecifications.BattleIdAndUserIdAreEqualTo(battleId, userId)).Single();
                battleUserStatistics.Balance -= bet;
                battleUserStatistics.OpenedBetsCount++;

                var queuedBetUrl = new QueuedBetUrl(battleBet.Id, url, QueuedBetUrlType.Open);
                battleBet.QueuedBetUrls.Add(queuedBetUrl);

                _repositoryOfBattleBet.Add(battleBet);

                unitOfWorkScope.SaveChanges();

                return battleBet.Id;
            }
        }

        public void CloseBattleBetAsSucceeded(long battleBetId, long userId, out long battleId)
        {
            CloseBattleBet(battleBetId, userId, true, out battleId);
        }

        public void CloseBattleBetAsFailed(long battleBetId, long userId, out long battleId)
        {
            CloseBattleBet(battleBetId, userId, false, out battleId);
        }

        private void CloseBattleBet(long battleBetId, long userId, bool success, out long battleId)
        {
            using (var unitOfWorkScope = _unitOfWorkScopeFactory.Create())
            {
                var battleBet = _repositoryOfBattleBet.Get(EntitySpecifications.EntityIdIsEqualTo<BattleBet>(battleBetId)).Single();

                if (userId != battleBet.UserId)
                    throw new ArgumentException("You are trying to close not your bet");

                battleBet.CloseDateTime = DateTime.UtcNow;
                battleBet.Success = success;

                battleId = battleBet.BattleId;

                var battleUserStatistics = _repositoryOfBattleUserStatistics.Get(BattleUserStatisticsSpecifications.BattleIdAndUserIdAreEqualTo(battleId, userId)).Single();
                if (success)
                    battleUserStatistics.Balance += battleBet.Bet * battleBet.Coefficient;
                battleUserStatistics.OpenedBetsCount--;
                battleUserStatistics.ClosedBetsCount++;

                unitOfWorkScope.SaveChanges();
            }
        }
    }
}
using System;
using System.Linq;
using System.Transactions;
using BetTeamsBattle.Data.Model.Entities;
using BetTeamsBattle.Data.Model.Enums;
using BetTeamsBattle.Data.Repositories.Base;
using BetTeamsBattle.Data.Repositories.Base.Interfaces;
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
        private readonly IRepository<Battle> _repositoryOfBattle;
        private readonly IRepository<Team> _repositoryOfTeam;
        private readonly IRepository<Bet> _repositoryOfBet;
        private readonly IRepository<BetScreenshot> _repositoryOfBetScreenshot;
        private readonly IRepository<BattleTeamStatistics> _repositoryOfBattleTeamStatistics;
        private readonly IUnitOfWorkScopeFactory _unitOfWorkScopeFactory;

        public BattlesService(ITransactionScopeFactory transactionScopeFactory, IUnitOfWorkScopeFactory unitOfWorkScopeFactory, IRepository<Battle> repositoryOfBattle, IRepository<Team> repositoryOfTeam, IRepository<Bet> repositoryOfBet, IRepository<BetScreenshot> repositoryOfBetScreenshot, IRepository<BattleTeamStatistics> repositoryOfBattleTeamStatistics)
        {
            _transactionScopeFactory = transactionScopeFactory;
            _repositoryOfBattle = repositoryOfBattle;
            _repositoryOfTeam = repositoryOfTeam;
            _repositoryOfBet = repositoryOfBet;
            _repositoryOfBetScreenshot = repositoryOfBetScreenshot;
            _repositoryOfBattleTeamStatistics = repositoryOfBattleTeamStatistics;
            _unitOfWorkScopeFactory = unitOfWorkScopeFactory;
        }

        public long CreateBattle(DateTime startDate, DateTime endDate, BattleType battleType, int budget)
        {
            using (var unitOfWorkScope = _unitOfWorkScopeFactory.Create())
            {
                var battle = new Battle(startDate, endDate, battleType, budget);

                _repositoryOfBattle.Add(battle);

                unitOfWorkScope.SaveChanges();

                return battle.Id;
            }
        }

        public long MakeBet(long battleId, long teamId, long userId, string title, double bet, double coefficient, string url, bool isPrivate)
        {
            using (var unitOfWorkScope = _unitOfWorkScopeFactory.Create())
            {
                var battleBet = new Bet(battleId, teamId, userId, title, bet, coefficient, url, isPrivate);
                battleBet.OpenBetScreenshot = new BetScreenshot();
                _repositoryOfBet.Add(battleBet);

                var battleTeamStatistics = _repositoryOfBattleTeamStatistics.Get(BattleTeamStatisticsSpecifications.BattleIdAndTeamIdAreEqualTo(battleId, teamId)).SingleOrDefault();
                if (battleTeamStatistics == null)
                {
                    var battleBudget = _repositoryOfBattle.Get(EntitySpecifications.IdIsEqualTo<Battle>(battleId)).Select(b => b.Budget).Single();
                    battleTeamStatistics = new BattleTeamStatistics(battleId, teamId, battleBudget);
                    _repositoryOfBattleTeamStatistics.Add(battleTeamStatistics);
                }

                var team = _repositoryOfTeam.Get(EntitySpecifications.IdIsEqualTo<Team>(teamId)).Single();

                team.Rating -= bet;
                battleTeamStatistics.Balance -= bet;

                battleTeamStatistics.OpenedBetsCount++;

                unitOfWorkScope.SaveChanges();

                return battleBet.Id;
            }
        }

        public void CloseBet(long battleBetId, long userId, BattleBetStatus status, out long battleId)
        {
            using (var unitOfWorkScope = _unitOfWorkScopeFactory.Create())
            {
                var bet = _repositoryOfBet.Get(EntitySpecifications.IdIsEqualTo<Bet>(battleBetId)).Single();
                battleId = bet.BattleId;

                if (userId != bet.UserId)
                    throw new ArgumentException("You are trying to close not your bet");
                if (bet.IsClosed)
                    throw new ArgumentException("This bet is already closed");

                bet.CloseDateTime = DateTime.UtcNow;
                bet.CloseBetScreenshot = new BetScreenshot();
                bet.StatusEnum = status;

                var battleTeamStatistics = _repositoryOfBattleTeamStatistics.Get(BattleTeamStatisticsSpecifications.BattleIdAndTeamIdAreEqualTo(bet.BattleId, bet.TeamId)).Single();
                var team = _repositoryOfTeam.Get(EntitySpecifications.IdIsEqualTo<Team>(bet.TeamId)).Single();

                double balanceChange;
                if (status == BattleBetStatus.Succeeded)
                    balanceChange = bet.Amount * bet.Coefficient;
                else if (status == BattleBetStatus.Failed)
                    balanceChange = 0;
                else if (status == BattleBetStatus.CanceledByBookmaker)
                    balanceChange = bet.Amount;
                else
                    throw new ArgumentOutOfRangeException("status");

                bet.Result = balanceChange - bet.Amount;
                battleTeamStatistics.Balance += balanceChange;
                team.Rating += balanceChange;

                battleTeamStatistics.OpenedBetsCount--;
                battleTeamStatistics.ClosedBetsCount++;

                unitOfWorkScope.SaveChanges();
            }
        }

        public void BetSucceeded(long battleBetId, long userId, out long battleId)
        {
            CloseBet(battleBetId, userId, BattleBetStatus.Succeeded, out battleId);
        }

        public void BetFailed(long battleBetId, long userId, out long battleId)
        {
            CloseBet(battleBetId, userId, BattleBetStatus.Failed, out battleId);
        }

        public void BetCanceledByBookmaker(long battleBetId, long userId, out long battleId)
        {
            CloseBet(battleBetId, userId, BattleBetStatus.CanceledByBookmaker, out battleId);
        }
    }
}
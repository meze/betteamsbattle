﻿using System;
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
        private readonly IRepository<BattleBet> _repositoryOfBattleBet;
        private readonly IRepository<BetScreenshot> _repositoryOfBetScreenshot;
        private readonly IRepository<BattleTeamStatistics> _repositoryOfBattleTeamStatistics;
        private readonly IUnitOfWorkScopeFactory _unitOfWorkScopeFactory;

        public BattlesService(ITransactionScopeFactory transactionScopeFactory, IUnitOfWorkScopeFactory unitOfWorkScopeFactory, IRepository<Battle> repositoryOfBattle, IRepository<Team> repositoryOfTeam, IRepository<BattleBet> repositoryOfBattleBet, IRepository<BetScreenshot> repositoryOfBetScreenshot, IRepository<BattleTeamStatistics> repositoryOfBattleTeamStatistics)
        {
            _transactionScopeFactory = transactionScopeFactory;
            _repositoryOfBattle = repositoryOfBattle;
            _repositoryOfTeam = repositoryOfTeam;
            _repositoryOfBattleBet = repositoryOfBattleBet;
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
                var battleBet = new BattleBet(battleId, teamId, userId, title, bet, coefficient, url, isPrivate);
                _repositoryOfBattleBet.Add(battleBet);

                var battleTeamStatistics = _repositoryOfBattleTeamStatistics.Get(BattleTeamStatisticsSpecifications.BattleIdAndTeamIdAreEqualTo(battleId, teamId)).SingleOrDefault();
                if (battleTeamStatistics == null)
                {
                    var battleBudget = _repositoryOfBattle.Get(EntitySpecifications.IdIsEqualTo<Battle>(battleId)).Select(b => b.Budget).Single();
                    battleTeamStatistics = new BattleTeamStatistics(battleId, teamId, battleBudget);
                    _repositoryOfBattleTeamStatistics.Add(battleTeamStatistics);
                }
                battleTeamStatistics.Balance -= bet;
                battleTeamStatistics.OpenedBetsCount++;

                battleBet.OpenBetScreenshot = new BetScreenshot() { CreationDateTime = DateTime.UtcNow };

                unitOfWorkScope.SaveChanges();

                return battleBet.Id;
            }
        }

        public void BetSucceeded(long battleBetId, long userId, out long battleId)
        {
            CloseBattleBet(battleBetId, userId, true, out battleId);
        }

        public void BetFailed(long battleBetId, long userId, out long battleId)
        {
            CloseBattleBet(battleBetId, userId, false, out battleId);
        }

        private void CloseBattleBet(long battleBetId, long userId, bool success, out long battleId)
        {
            using (var unitOfWorkScope = _unitOfWorkScopeFactory.Create())
            {
                var battleBet = _repositoryOfBattleBet.Get(EntitySpecifications.IdIsEqualTo<BattleBet>(battleBetId)).Single();
                battleId = battleBet.BattleId;

                if (userId != battleBet.UserId)
                    throw new ArgumentException("You are trying to close not your bet");

                battleBet.CloseDateTime = DateTime.UtcNow;
                battleBet.Success = success;

                var battleTeamStatistics = _repositoryOfBattleTeamStatistics.Get(BattleTeamStatisticsSpecifications.BattleIdAndTeamIdAreEqualTo(battleBet.BattleId, battleBet.TeamId)).Single();
                var team = _repositoryOfTeam.Get(EntitySpecifications.IdIsEqualTo<Team>(battleBet.TeamId)).Single();
                if (success)
                {
                    var betWin = battleBet.Bet * battleBet.Coefficient;

                    battleTeamStatistics.Balance += betWin;
                    team.Rating += betWin;
                }
                else
                    team.Rating -= battleBet.Bet;

                battleTeamStatistics.OpenedBetsCount--;
                battleTeamStatistics.ClosedBetsCount++;

                unitOfWorkScope.SaveChanges();
            }
        }
    }
}
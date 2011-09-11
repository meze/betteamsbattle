using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using BetTeamsBattle.Data.Model.Entities;
using BetTeamsBattle.Data.Model.Enums;
using BetTeamsBattle.Data.Model.Helpers;
using BetTeamsBattle.Data.Repositories.Base;
using BetTeamsBattle.Data.Repositories.Base.Interfaces;
using BetTeamsBattle.Data.Repositories.Specifications;
using BetTeamsBattle.Data.Services.Interfaces;
using BetTeamsBattle.Frontend.Areas.NotAdmin.Localization.Localizers.InDays.Interfaces;
using BetTeamsBattle.Frontend.Areas.NotAdmin.Models.Battles;
using BetTeamsBattle.Frontend.Areas.NotAdmin.Models.Teams;
using BetTeamsBattle.Frontend.Areas.NotAdmin.ViewServices.Battles.Interfaces;
using BetTeamsBattle.Frontend.Authentication;
using BetTeamsBattle.Frontend.Services.Interfaces;

namespace BetTeamsBattle.Frontend.Areas.NotAdmin.ViewServices.Battles
{
    internal class BattlesViewService : IBattlesViewService
    {
        private readonly IRepository<Battle> _repositoryOfBattle;
        private readonly IInDaysLocalizer _inDaysLocalizer;
        private readonly IRepository<TeamBattleStatistics> _repositoryOfBattleTeamStatistics;
        private readonly IBattlesService _battlesService;

        public BattlesViewService(IRepository<Battle> repositoryOfBattle, IInDaysLocalizer inDaysLocalizer, IRepository<TeamBattleStatistics> repositoryOfBattleTeamStatistics)
        {
            _repositoryOfBattle = repositoryOfBattle;
            _inDaysLocalizer = inDaysLocalizer;
            _repositoryOfBattleTeamStatistics = repositoryOfBattleTeamStatistics;
        }

        public BattleViewModel Battle(long battleId, long? nullableUserId)
        {
            var battle = _repositoryOfBattle.Get(EntitySpecifications.IdIsEqualTo<Battle>(battleId)).Single();
            var battleIsActive = new List<Battle> { battle }.AsQueryable().Where(BattleSpecifications.Current()).Any();

            var battleViewModel = new BattleViewModel(battle.Id, battle.StartDate.ToShortDateString(), battle.EndDate.ToShortDateString(), battle.Budget, battle.BetLimit, battleIsActive);

            if (nullableUserId.HasValue)
            {
                var battleTeamStatistics = _repositoryOfBattleTeamStatistics.Get(TeamBattleStatisticsSpecifications.BattleIdAndTeamIdAreEqualTo(battleId, nullableUserId.Value)).SingleOrDefault();

                var battleStatisticsViewModel = new BattleStatisticsViewModel();

                if (battleTeamStatistics != null)
                {
                    var gain = battleTeamStatistics.Gain;
                    var gainInPercents = gain / battle.Budget * 100;
                    battleStatisticsViewModel.Balance = battle.Budget + gain;
                    battleStatisticsViewModel.Gain = gain;
                    battleStatisticsViewModel.GainInPercents = gainInPercents;

                    battleStatisticsViewModel.TotalBetsCount = battleTeamStatistics.OpenedBetsCount + battleTeamStatistics.ClosedBetsCount;
                    battleStatisticsViewModel.OpenBetsCount = battleTeamStatistics.OpenedBetsCount;
                }
                else
                {
                    battleStatisticsViewModel.Balance = battle.Budget;
                    battleStatisticsViewModel.Gain = 0;
                    battleStatisticsViewModel.GainInPercents = 0;

                    battleStatisticsViewModel.TotalBetsCount = 0;
                    battleStatisticsViewModel.OpenBetsCount = 0;
                }

                battleViewModel.BattleStatistics = battleStatisticsViewModel;
            }

            return battleViewModel;
        }

        public AllBattlesViewModel AllBattles()
        {
            var allBattlesViewModel = new AllBattlesViewModel();

            allBattlesViewModel.CurrentBattlesViewModels = _repositoryOfBattle.Get(BattleSpecifications.Current()).
                OrderBy(b => b.StartDate).
                AsEnumerable().
                Select(b => new CurrentBattleViewModel() { BattleId = b.Id, Budget = b.Budget, BetLimit = b.BetLimit, StartDate = b.StartDate.ToShortDateString(), EndDate = b.EndDate.ToShortDateString() }).
                ToList();

            allBattlesViewModel.NotStartedBattlesViewModels = _repositoryOfBattle.Get(BattleSpecifications.NotStarted()).
                OrderBy(b => b.StartDate).
                AsEnumerable().
                Select(b => new NotStartedBattleViewModel() { BattleId = b.Id, Budget = b.Budget, BetLimit = b.BetLimit, StartDate = b.StartDate.ToShortDateString(), EndDate = b.EndDate.ToShortDateString() }).
                ToList();

            allBattlesViewModel.FinishedBattlesViewModels = _repositoryOfBattle.Get(BattleSpecifications.Finished()).
                OrderBy(b => b.StartDate).
                AsEnumerable().
                Select(b => new FinishedBattleViewModel() { BattleId = b.Id, Budget = b.Budget, BetLimit = b.BetLimit, StartDate = b.StartDate.ToShortDateString(), EndDate = b.EndDate.ToShortDateString() }).
                ToList();

            return allBattlesViewModel;
        }
    }
}
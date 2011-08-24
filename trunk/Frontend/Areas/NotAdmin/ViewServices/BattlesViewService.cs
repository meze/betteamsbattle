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
        private IRepository<BattleTeamStatistics> _repositoryOfBattleTeamStatistics;

        public BattlesViewService(IRepository<Battle> repositoryOfBattle, IInDaysLocalizer inDaysLocalizer, IRepository<BattleTeamStatistics> repositoryOfBattleTeamStatistics)
        {
            _repositoryOfBattle = repositoryOfBattle;
            _inDaysLocalizer = inDaysLocalizer;
            _repositoryOfBattleTeamStatistics = repositoryOfBattleTeamStatistics;
        }

        public BattleViewModel Battle(long battleId, long? nullableUserId)
        {
            var battle = _repositoryOfBattle.Get(EntitySpecifications.EntityIdIsEqualTo<Battle>(battleId)).Single();
            var battleIsActive = new List<Battle> { battle }.AsQueryable().Where(BattleSpecifications.Current()).Any();

            var battleViewModel = new BattleViewModel(battle.Id, battle.StartDate.ToShortDateString(), battle.EndDate.ToShortDateString(), battle.Budget, battle.BetLimit, battleIsActive);

            //if (battleViewModel.UserIsJoinedToBattle)
            //{
            //var battleUserStatistics = _repositoryOfBattleUserStatistics.Get(BattleUserStatisticsSpecifications.BattleIdAndUserIdAreEqualTo(battleId, nullableUserId.Value)).Single();

            //var earned = battleUserStatistics.Balance - battle.Budget;
            //var earnedPercents = earned/battle.Budget;
            //battleViewModel.Earned = earned;
            //battleViewModel.EarnedPercents = earnedPercents;

            //battleViewModel.TotalBetsCount = battleUserStatistics.OpenedBetsCount + battleUserStatistics.ClosedBetsCount;
            //battleViewModel.OpenBetsCount = battleUserStatistics.OpenedBetsCount;
            //}

            return battleViewModel;
        }

        public IEnumerable<BattleTopTeamsTeamViewModel> BattleTopTeams(long battleId)
        {
            return
                _repositoryOfBattleTeamStatistics.Get(BattleTeamStatisticsSpecifications.BattleIdIsEqualTo(battleId)).
                    OrderByDescending(bts => bts.Balance).
                    Skip(0).Take(10).
                    Select(
                        bus =>
                        new BattleTopTeamsTeamViewModel() { TeamId = bus.TeamId, Login = bus.Team.Title, Balance = bus.Balance }).
                    ToList();
        }

        public AllBattlesViewModel AllBattles()
        {
            var allBattlesViewModel = new AllBattlesViewModel();

            var currentBattles = _repositoryOfBattle.Get(BattleSpecifications.Current()).OrderBy(b => b.StartDate).ToList();
            var currentBattlesViewModels = new List<CurrentBattleViewModel>();
            foreach (var currentBattle in currentBattles)
            {
                var currentBattleViewModel = new CurrentBattleViewModel(currentBattle.Id, currentBattle.Budget, currentBattle.BetLimit, currentBattle.StartDate.ToShortDateString(), currentBattle.EndDate.ToShortDateString());
                currentBattlesViewModels.Add(currentBattleViewModel);
            }
            allBattlesViewModel.CurrentBattlesViewModels = currentBattlesViewModels;

            var notStartedBattles = _repositoryOfBattle.Get(BattleSpecifications.NotStarted()).OrderBy(b => b.StartDate).ToList();
            var notStartedBattlesViewModels = new List<NotStartedBattleViewModel>();
            foreach (var notStartedBattle in notStartedBattles)
            {
                var inDays = (notStartedBattle.StartDate - DateTime.UtcNow).Days;
                var inDaysString = _inDaysLocalizer.Localize(inDays);

                var futureBattleViewModel = new NotStartedBattleViewModel(notStartedBattle.Id, notStartedBattle.Budget, notStartedBattle.BetLimit, notStartedBattle.StartDate.ToShortDateString(), notStartedBattle.EndDate.ToShortDateString(), inDaysString);
                notStartedBattlesViewModels.Add(futureBattleViewModel);
            }
            allBattlesViewModel.NotStartedBattlesViewModels = notStartedBattlesViewModels;

            var finishedBattles = _repositoryOfBattle.Get(BattleSpecifications.Finished()).OrderBy(b => b.StartDate).ToList();
            var finishedBattlesViewModels = new List<FinishedBattleViewModel>();
            foreach (var finishedBattle in finishedBattles)
            {
                var finishedBattleViewModel = new FinishedBattleViewModel(finishedBattle.Id, finishedBattle.Budget, finishedBattle.BetLimit, finishedBattle.StartDate.ToShortDateString(), finishedBattle.EndDate.ToShortDateString());
                finishedBattlesViewModels.Add(finishedBattleViewModel);
            }
            allBattlesViewModel.FinishedBattlesViewModels = finishedBattlesViewModels;

            return allBattlesViewModel;
        }
    }
}
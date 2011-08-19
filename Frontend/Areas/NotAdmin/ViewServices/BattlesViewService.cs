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
using BetTeamsBattle.Data.Repositories.EntityRepositories.Interfaces;
using BetTeamsBattle.Data.Repositories.Specifications;
using BetTeamsBattle.Frontend.Areas.NotAdmin.Localization.Localizers.InDays.Interfaces;
using BetTeamsBattle.Frontend.Areas.NotAdmin.Models.Battle;
using BetTeamsBattle.Frontend.Areas.NotAdmin.ViewServices.Battles.Interfaces;
using BetTeamsBattle.Frontend.Authentication;
using BetTeamsBattle.Frontend.Services.Interfaces;

namespace BetTeamsBattle.Frontend.Areas.NotAdmin.ViewServices.Battles
{
    internal class BattlesViewService : IBattlesViewService
    {
        private readonly IRepository<Battle> _repositoryOfBattle;
        private readonly IBattleUserRepository _battleUserRepository;
        private readonly IRepository<BattleUserStatistics> _repositoryOfBattleUserStatistics;
        private readonly IInDaysLocalizer _inDaysLocalizer;

        public BattlesViewService(IRepository<Battle> repositoryOfBattle, IBattleUserRepository battleUserRepository, IRepository<BattleUserStatistics> repositoryOfBattleUserStatistics, IInDaysLocalizer inDaysLocalizer)
        {
            _repositoryOfBattle = repositoryOfBattle;
            _battleUserRepository = battleUserRepository;
            _repositoryOfBattleUserStatistics = repositoryOfBattleUserStatistics;
            _inDaysLocalizer = inDaysLocalizer;
        }

        public BattleViewModel Battle(long battleId, long? nullableUserId)
        {
            var battle = _repositoryOfBattle.Get(EntitySpecifications.EntityIdIsEqualTo<Battle>(battleId)).Single();
            
            var battleViewModel = new BattleViewModel(battle.Id, battle.StartDate.ToShortDateString(), battle.EndDate.ToShortDateString(), battle.Budget, battle.BetLimit);

            BattleUser battleUser = null;
            if (nullableUserId.HasValue)
                battleUser = _battleUserRepository.GetLastBattleUser(nullableUserId.Value, battleId).SingleOrDefault();
            if (battleUser == null || battleUser.ActionEnum == BattleUserAction.Leave)
            {
                battleViewModel.IsJoined = false;
                battleViewModel.JoinOrLeaveActionResult = MVC.NotAdmin.Battles.JoinBattle(battleId);
                battleViewModel.JoinOrLeaveTitle = Localization.Resources.Views.Battles.Battles.Join;
            }
            else
            {
                battleViewModel.IsJoined = true;
                battleViewModel.JoinOrLeaveActionResult = MVC.NotAdmin.Battles.LeaveBattle(battleId);
                battleViewModel.JoinOrLeaveTitle = Localization.Resources.Views.Battles.Battles.Leave;
            }

            if (nullableUserId.HasValue)
            {
                var battleUserStatistics = _repositoryOfBattleUserStatistics.Get(BattleUserStatisticsSpecifications.UserIdIsEqualTo(nullableUserId.Value)).Single();

                var earned = battleUserStatistics.Balance - battle.Budget;
                var earnedPercents = earned/battle.Budget;
                battleViewModel.Earned = earned;
                battleViewModel.EarnedPercents = earnedPercents;

                battleViewModel.TotalBetsCount = battleUserStatistics.OpenedBetsCount + battleUserStatistics.ClosedBetsCount;
                battleViewModel.OpenBetsCount = battleUserStatistics.OpenedBetsCount;
            }

            return battleViewModel;
        }

        public AllBattlesViewModel AllBattles()
        {
            var allBattlesViewModel = new AllBattlesViewModel();

            var currentBattles = _repositoryOfBattle.Get(BattleSpecifications.Current()).OrderBy(b => b.StartDate).ToList();
            var currentBattlesViewModels = new List<CurrentBattleViewModel>();
            foreach (var currentBattle in currentBattles)
            {
                var currentBattleViewModel = new CurrentBattleViewModel(currentBattle.Budget, currentBattle.BetLimit, currentBattle.StartDate.ToShortDateString(), currentBattle.EndDate.ToShortDateString());
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
                var finishedBattleViewModel = new FinishedBattleViewModel(finishedBattle.Budget, finishedBattle.BetLimit, finishedBattle.StartDate.ToShortDateString(), finishedBattle.EndDate.ToShortDateString());
                finishedBattlesViewModels.Add(finishedBattleViewModel);
            }
            allBattlesViewModel.FinishedBattlesViewModels = finishedBattlesViewModels;

            return allBattlesViewModel;
        }
    }
}
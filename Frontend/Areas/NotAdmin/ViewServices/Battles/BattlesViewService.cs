using System;
using System.Collections.Generic;
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
using BetTeamsBattle.Frontend.Services.Interfaces;

namespace BetTeamsBattle.Frontend.Areas.NotAdmin.ViewServices.Battles
{
    internal class BattlesViewService : IBattlesViewService
    {
        private readonly IRepository<Battle> _repositoryOfBattle;
        private readonly IBattleUserRepository _battleUserRepository;
        private IInDaysLocalizer _inDaysLocalizer;

        public BattlesViewService(IRepository<Battle> repositoryOfBattle, IBattleUserRepository battleUserRepository, IInDaysLocalizer inDaysLocalizer)
        {
            _repositoryOfBattle = repositoryOfBattle;
            _battleUserRepository = battleUserRepository;
            _inDaysLocalizer = inDaysLocalizer;
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

                var futureBattleViewModel = new NotStartedBattleViewModel(notStartedBattle.Budget, notStartedBattle.BetLimit, notStartedBattle.StartDate.ToShortDateString(), notStartedBattle.EndDate.ToShortDateString(), inDaysString);
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

        public IEnumerable<ActualBattleViewModel> ActualBattlesViewModels(long? userId)
        {
            var actualBattles = _repositoryOfBattle.Get(BattleSpecifications.Current()).
                OrderBy(b => b.StartDate).
                ToList();
            var actualBattlesIds = EntityHelper.GetIds(actualBattles);

            IEnumerable<BattleUser> actualBattlesUsers = new List<BattleUser>();
            if (userId.HasValue)
                actualBattlesUsers = _battleUserRepository.GetLastBattleUsers(userId.Value, actualBattlesIds).ToList();

            var actualBattleViewModels = new List<ActualBattleViewModel>();
            foreach (var battle in actualBattles)
            {
                var actualBattleViewModel = new ActualBattleViewModel()
                                                {
                                                    Id = battle.Id,
                                                    StartDate = battle.StartDate,
                                                    EndDate = battle.EndDate,
                                                    Budget = battle.Budget,
                                                    Earned = 0,
                                                };

                var battleUser = actualBattlesUsers.Where(bu => bu.BattleId == battle.Id).SingleOrDefault();

                if (battleUser == null || battleUser.ActionEnum == BattleUserAction.Leave)
                {
                    actualBattleViewModel.IsJoined = false;
                    actualBattleViewModel.JoinOrLeaveActionResult = userId.HasValue
                                                                        ? MVC.NotAdmin.Battles.JoinBattle(battle.Id)
                                                                        : MVC.NotAdmin.Accounts.SignIn();
                    actualBattleViewModel.JoinOrLeaveTitle = Resources.Battles.Join;
                }
                else
                {
                    actualBattleViewModel.IsJoined = true;
                    actualBattleViewModel.JoinOrLeaveActionResult = MVC.NotAdmin.Battles.LeaveBattle(battle.Id);
                    actualBattleViewModel.JoinOrLeaveTitle = Resources.Battles.Leave;
                }

                actualBattleViewModels.Add(actualBattleViewModel);
            }

            return actualBattleViewModels;
        }
    }
}
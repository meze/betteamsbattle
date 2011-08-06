using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BetTeamsBattle.Data.Model.Entities;
using BetTeamsBattle.Data.Model.Enums;
using BetTeamsBattle.Data.Model.Helpers;
using BetTeamsBattle.Data.Repositories.Base;
using BetTeamsBattle.Data.Repositories.Specific.Interfaces;
using BetTeamsBattle.Data.Repositories.Specifications;
using BetTeamsBattle.Frontend.Areas.NotAdmin.Models.Battle;
using BetTeamsBattle.Frontend.Areas.NotAdmin.ViewServices.Battles.Interfaces;

namespace BetTeamsBattle.Frontend.Areas.NotAdmin.ViewServices.Battles
{
    internal class BattlesViewService : IBattlesViewService
    {
        private readonly IRepository<Battle> _repositoryOfBattle;
        private readonly IBattleUserRepository _battleUserRepository;

        public BattlesViewService(IRepository<Battle> repositoryOfBattle, IBattleUserRepository battleUserRepository)
        {
            _repositoryOfBattle = repositoryOfBattle;
            _battleUserRepository = battleUserRepository;
        }

        public IEnumerable<ActualBattleViewModel> ActualBattlesViewModels(long userId)
        {
            var actualBattles = _repositoryOfBattle.Get(BattleSpecifications.NotFinishedOrNotStarted()).
                OrderBy(b => b.StartDate).
                ToList();
            var actualBattlesIds = EntityHelper.GetIds(actualBattles);

            var actualBattlesUsers = _battleUserRepository.GetLastBattleUsers(userId, actualBattlesIds).ToList();

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
                if (battleUser == null || battleUser.ActionEnum == BattleAction.Leave)
                {
                    actualBattleViewModel.IsJoined = false;
                    actualBattleViewModel.JoinOrLeaveActionResult = MVC.NotAdmin.Battles.JoinBattle(battle.Id);
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
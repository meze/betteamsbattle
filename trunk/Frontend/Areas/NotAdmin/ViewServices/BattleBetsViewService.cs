using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using BetTeamsBattle.BettScreenshotsManager.BetScreenshotProcessor.Interfaces;
using BetTeamsBattle.Data.Model.Entities;
using BetTeamsBattle.Data.Repositories.Base.Interfaces;
using BetTeamsBattle.Data.Repositories.Specifications;
using BetTeamsBattle.Frontend.Areas.NotAdmin.Models.BattleBets;
using BetTeamsBattle.Frontend.Areas.NotAdmin.ViewServices.Battles.Interfaces;
using Resources;

namespace BetTeamsBattle.Frontend.Areas.NotAdmin.ViewServices
{
    internal class BattleBetsViewService : IBattleBetsViewService
    {
        private readonly IRepository<BattleBet> _repositoryOfBattleBet;
        private IRepository<Team> _repositoryOfTeam;
        private readonly IRepository<Battle> _repositoryOfBattle;
        private IBetScreenshotPathService _betScreenshotPathService;

        public BattleBetsViewService(IRepository<BattleBet> repositoryOfBattleBet, IRepository<Team> repositoryOfTeam, IRepository<Battle> repositoryOfBattle, IBetScreenshotPathService betScreenshotPathService)
        {
            _repositoryOfBattleBet = repositoryOfBattleBet;
            _repositoryOfTeam = repositoryOfTeam;
            _repositoryOfBattle = repositoryOfBattle;
            _betScreenshotPathService = betScreenshotPathService;
        }

        public MakeBetViewModel MakeBet(long battleId, long userId, MakeBetFormViewModel makeBetFormViewModel)
        {
            var battle = _repositoryOfBattle.Get(EntitySpecifications.IdIsEqualTo<Battle>(battleId)).Single();

            var teams = _repositoryOfTeam.Get(TeamSpecifications.UserIsMember(userId)).ToList();
            var personalTeam = teams.Where(t => t.IsPersonal).Single();
            teams.Remove(personalTeam);
            teams.Insert(0, personalTeam);
            var teamsSelectListItems = teams.Select(
                t =>
                    {
                        string title;
                        if (t.IsPersonal)
                            title = BattleBets.MePersonally;
                        else
                            title = String.Format("{0} {1}", BattleBets.OfTeam, t.Title);
                        var isSelected = t.Id == makeBetFormViewModel.TeamId;
                        var selectListItem = new SelectListItem() {Value = t.Id.ToString(), Text = title, Selected = isSelected};
                        return selectListItem;
                    }).
                ToList();

            var makeBetViewModel = new MakeBetViewModel(battleId, battle.StartDate.ToShortDateString(), battle.EndDate.ToShortDateString(), battle.Budget, battle.BetLimit, teamsSelectListItems, makeBetFormViewModel);

            return makeBetViewModel;
        }

        public MakeBetViewModel MakeBet(long battleId, long userId)
        {
            return MakeBet(battleId, userId, new MakeBetFormViewModel());
        }

        public IEnumerable<MyBetViewModel> MyBets(long battleId, long userId)
        {
            var myBets = _repositoryOfBattleBet.
                   Get(BattleBetSpecifications.BattleIdAndUserIdAreEqualTo(battleId, userId)).
                   Include(bb => bb.OpenBetScreenshot).
                   Include(bb => bb.CloseBetScreenshot).
                   OrderByDescending(b => b.OpenDateTime).
                   ToList();

            var myBetsViewModels = new List<MyBetViewModel>();
            foreach (var myBet in myBets)
            {
                var myBetViewModel = new MyBetViewModel()
                                         {
                                             BattleBetId = myBet.Id,
                                             BattleId = myBet.BattleId,
                                             Title = myBet.Title,
                                             Url = myBet.Url,
                                             Bet = myBet.Bet,
                                             Coefficient = myBet.Coefficient,
                                             IsPrivate = myBet.IsPrivate,
                                         };

                myBetViewModel.OpenDateTime = myBet.OpenDateTime.ToShortDateString();
                myBetViewModel.OpenScreenshotStatus = BetScreenshotStatusToString(myBet.OpenBetScreenshot.StatusEnum);
                if (myBet.OpenBetScreenshot.StatusEnum == BetScreenshotStatus.Processed)
                    myBetViewModel.OpenScreenshotUrl = _betScreenshotPathService.GetUrl(myBet.OpenBetScreenshotId);

                if (myBet.CloseBetScreenshotId.HasValue)
                {
                    myBetViewModel.CloseDateTime = myBet.CloseDateTime.Value.ToShortDateString();
                    myBetViewModel.CloseScreenshotStatus = BetScreenshotStatusToString(myBet.OpenBetScreenshot.StatusEnum);
                    if (myBet.CloseBetScreenshot.StatusEnum == BetScreenshotStatus.Processed)
                        myBetViewModel.CloseScreenshotUrl = _betScreenshotPathService.GetUrl(myBet.CloseBetScreenshotId.Value);
                }
                else
                    myBetViewModel.CloseDateTime = "?";

                myBetsViewModels.Add(myBetViewModel);
            }

            return myBetsViewModels;
        }

        private string BetScreenshotStatusToString(BetScreenshotStatus betScreenshotStatus)
        {
            if (betScreenshotStatus == BetScreenshotStatus.Failed)
                return "Failed";
            else if (betScreenshotStatus == BetScreenshotStatus.NotProcessed)
                return "Retrieving...";
            else if (betScreenshotStatus == BetScreenshotStatus.Processed)
                return "Screenshot";
            else
                throw new ArgumentOutOfRangeException("betScreenshotStatus");
        }
    }
}
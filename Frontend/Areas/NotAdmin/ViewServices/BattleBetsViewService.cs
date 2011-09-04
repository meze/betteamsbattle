using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using BetTeamsBattle.Data.Model.Entities;
using BetTeamsBattle.Data.Model.Enums;
using BetTeamsBattle.Data.Repositories.Base.Interfaces;
using BetTeamsBattle.Data.Repositories.Specifications;
using BetTeamsBattle.Frontend.Areas.NotAdmin.Models.BattleBets;
using BetTeamsBattle.Frontend.Areas.NotAdmin.ViewServices.Battles.Interfaces;
using BetTeamsBattle.Frontend.Extensions;
using BetTeamsBattle.Screenshots.AmazonS3.Interfaces;
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
                    var selectListItem = new SelectListItem() { Value = t.Id.ToString(), Text = title, Selected = isSelected };
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

        public IEnumerable<MyBetViewModel> MyBattleBets(long battleId, long userId)
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
                                             IsClosed = myBet.IsClosed,
                                             Status = myBet.StatusEnum,
                                         };

                myBetViewModel.OpenDateAndScreenshot = GetDateAndScreenshot(myBet.OpenDateTime, myBet.OpenBetScreenshot.StatusEnum, myBet.OpenBetScreenshot.FileName);

                if (myBet.IsClosed)
                {
                    myBetViewModel.CloseDateAndScreenshot = GetDateAndScreenshot(myBet.CloseDateTime.Value, myBet.CloseBetScreenshot.StatusEnum, myBet.CloseBetScreenshot.FileName);
                    myBetViewModel.StatusActionImage = GetBattleBetStatusImages(myBet.Id)[myBet.StatusEnum];
                    myBetViewModel.Result = myBet.Result.Value;
                }
                else
                    myBetViewModel.StatusActionImages = GetBattleBetStatusImages(myBet.Id);

                myBetsViewModels.Add(myBetViewModel);
            }

            return myBetsViewModels;
        }

        public IEnumerable<MyBetViewModel> TeamBets(long teamId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MyBetViewModel> UserBets(long userId)
        {
            throw new NotImplementedException();
        }

        public IDictionary<BattleBetStatus, StatusActionImageViewModel> GetBattleBetStatusImages(long battleBetId)
        {
            return new Dictionary<BattleBetStatus, StatusActionImageViewModel>()
                { 
                    { BattleBetStatus.Succeeded, new StatusActionImageViewModel(MVC.NotAdmin.BattleBets.BetSucceeded(battleBetId), "scr_2", BattleBets.Succeeded) },
                    { BattleBetStatus.Failed, new StatusActionImageViewModel(MVC.NotAdmin.BattleBets.BetFailed(battleBetId), "scr_1", BattleBets.Failed) },
                    { BattleBetStatus.CanceledByBookmaker, new StatusActionImageViewModel(MVC.NotAdmin.BattleBets.BetCanceledByBookmaker(battleBetId), "scr_3", BattleBets.CanceledByBookmaker ) }
                };
        }

        private DateAndScreenshotViewModel GetDateAndScreenshot(DateTime dateTime, BetScreenshotStatus betScreenshotStatus, string betScreenshotFileName)
        {
            var dateAndScreenshot = new DateAndScreenshotViewModel
                                        {
                                            DateTime = dateTime.ToShortDateLongTimeString(),
                                            ScreenshotStatusClass = GetBetScreenshotStatusClass(betScreenshotStatus),
                                            ScreenshotStatusString = GetBetScreenshotStatusString(betScreenshotStatus)
                                        };
            if (betScreenshotStatus == BetScreenshotStatus.Succeeded)
                dateAndScreenshot.ScreenshotUrl = _betScreenshotPathService.GetUrl(betScreenshotFileName);
            return dateAndScreenshot;
        }

        private string GetBetScreenshotStatusClass(BetScreenshotStatus betScreenshotStatus)
        {
            if (betScreenshotStatus == BetScreenshotStatus.NotProcessed)
                return "sc_inprogress";
            else if (betScreenshotStatus == BetScreenshotStatus.Failed)
                return "sc_failed";
            else if (betScreenshotStatus == BetScreenshotStatus.Succeeded)
                return "sc_succeeded";
            else
                throw new ArgumentOutOfRangeException("betScreenshotStatus");
        }

        private string GetBetScreenshotStatusString(BetScreenshotStatus betScreenshotStatus)
        {
            if (betScreenshotStatus == BetScreenshotStatus.Succeeded)
                return BattleBets.Screenshot;
            else if (betScreenshotStatus == BetScreenshotStatus.Failed)
                return BattleBets.FailedToMakeScreenshot;
            else if (betScreenshotStatus == BetScreenshotStatus.NotProcessed)
                return BattleBets.MakingScreenshot;
            else
                throw new ArgumentOutOfRangeException("betScreenshotStatus");
        }
    }
}
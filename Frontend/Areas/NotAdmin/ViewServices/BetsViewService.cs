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
    internal class BetsViewService : IBetsViewService
    {
        private readonly IRepository<Bet> _repositoryOfBet;
        private IRepository<Team> _repositoryOfTeam;
        private readonly IRepository<Battle> _repositoryOfBattle;
        private IBetScreenshotPathService _betScreenshotPathService;

        public BetsViewService(IRepository<Bet> repositoryOfBet, IRepository<Team> repositoryOfTeam, IRepository<Battle> repositoryOfBattle, IBetScreenshotPathService betScreenshotPathService)
        {
            _repositoryOfBet = repositoryOfBet;
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
                        title = Bets.MePersonally;
                    else
                        title = String.Format("{0} {1}", Bets.OfTeam, t.Title);
                    var selectListItem = new SelectListItem() { Value = t.Id.ToString(), Text = title };
                    return selectListItem;
                }).
                ToList();
            if (makeBetFormViewModel.TeamId == 0 && teamsSelectListItems.Count == 2)
                makeBetFormViewModel.TeamId = teams[1].Id; //select non-personal team by default

            var makeBetViewModel = new MakeBetViewModel(battleId, battle.StartDate.ToShortDateString(), battle.EndDate.ToShortDateString(), battle.Budget, battle.BetLimit, teamsSelectListItems, makeBetFormViewModel);

            return makeBetViewModel;
        }

        public MakeBetViewModel MakeBet(long battleId, long userId)
        {
            return MakeBet(battleId, userId, new MakeBetFormViewModel());
        }

        public BetsViewModel GetMyBattleBets(long battleId, long? userId)
        {
            var betsViewModel = new BetsViewModel();
            betsViewModel.Title = Bets.MyBets;
            betsViewModel.NoBetsMessage = Bets.YouHaveMadeNoBetsInThisBattle;
            if (!userId.HasValue)
                return betsViewModel;

            var myBattleBets = GetBetsQuery(BetSpecifications.BattleIdAndUserIdAreEqualTo(battleId, userId.Value)).ToList();
            betsViewModel.Bets = GetBetViewModels(myBattleBets, true, false, false);

            return betsViewModel;
        }

        public BetsViewModel GetUserBets(long userId, long? currentUserId)
        {
            var betsViewModel = new BetsViewModel();

            if (userId == currentUserId)
            {
                betsViewModel.Title = Bets.MyBets;
                betsViewModel.NoBetsMessage = Bets.YouHaveMadeNoBets;

                var myBets = GetBetsQuery(BetSpecifications.UserIdIsEqualTo(userId)).Include(b => b.Battle).ToList();
                betsViewModel.Bets = GetBetViewModels(myBets, true, true, false);
            }
            else
            {
                betsViewModel.Title = Bets.UserBets;
                betsViewModel.NoBetsMessage = Bets.UserHasMadeNoBets;

                var userBets = GetBetsQuery(BetSpecifications.UserIdIsEqualTo(userId)).Include(b => b.Battle).ToList();
                betsViewModel.Bets = GetBetViewModels(userBets, false, true, false);
            }

            return betsViewModel;
        }

        public BetsViewModel GetTeamBets(long teamId)
        {
            var betsViewModel = new BetsViewModel();
            betsViewModel.Title = Bets.TeamBets;
            betsViewModel.NoBetsMessage = Bets.TeamHasMadeNoBets;

            var teamBets = GetBetsQuery(BetSpecifications.TeamIdIsEqualTo(teamId)).Include(b => b.User).ToList();
            betsViewModel.Bets = GetBetViewModels(teamBets, false, false, true);

            return betsViewModel;
        }

        private IQueryable<Bet> GetBetsQuery(LinqSpec<Bet> specification)
        {
            return _repositoryOfBet.
                Get(specification).
                Include(bb => bb.OpenBetScreenshot).
                Include(bb => bb.CloseBetScreenshot).
                OrderByDescending(b => b.OpenDateTime);
        }

        private IEnumerable<BetViewModel> GetBetViewModels(IEnumerable<Bet> bets, bool isEditable, bool displayBattle, bool displayUser)
        {
            var betViewModels = new List<BetViewModel>();
            foreach (var bet in bets)
            {
                var betViewModel = new BetViewModel() { BetId = bet.Id };
                betViewModel.IsEditable = isEditable;
                betViewModel.IsClosed = bet.IsClosed;

                betViewModel.IsVisible = true;
                if (!isEditable)
                {
                    if (bet.IsPrivate)
                    {
                        betViewModel.IsVisible = false;
                        betViewModel.InvisibleIconClass = "eye_red";
                        betViewModel.InvisibleIconTitle = Bets.BetIsPrivate;
                    }
                    else if (!bet.IsClosed)
                    {
                        betViewModel.IsVisible = false;
                        betViewModel.InvisibleIconClass = "eye_blek";
                        betViewModel.InvisibleIconTitle = Bets.BetIsNotClosed;
                    }
                }

                if (betViewModel.IsVisible)
                {
                    betViewModel.Title = bet.Title;
                    betViewModel.Url = bet.Url;
                    betViewModel.Amount = bet.Amount;
                    betViewModel.Coefficient = bet.Coefficient;
                }

                betViewModel.OpenDateAndScreenshot = GetDateAndScreenshot(bet.OpenBetScreenshot, betViewModel.IsVisible);

                if (bet.IsClosed)
                {
                    betViewModel.CloseDateAndScreenshot = GetDateAndScreenshot(bet.CloseBetScreenshot, betViewModel.IsVisible);
                    betViewModel.StatusActionImage = GeBetStatusImage(bet.Id, bet.StatusEnum);
                    betViewModel.Result = bet.Result.Value;
                }
                else if (isEditable)
                    betViewModel.StatusActionImages = GetBetStatusImages(bet.Id);

                if (displayBattle)
                {
                    
                }

                if (displayUser)
                {
                    
                }

                betViewModels.Add(betViewModel);
            }

            return betViewModels;
        }

        private IEnumerable<StatusActionImageViewModel> GetBetStatusImages(long battleBetId)
        {
            return GetBetStatusImageMappings(battleBetId).Values;
        }

        private StatusActionImageViewModel GeBetStatusImage(long battleBetId, BetStatus betStatus)
        {
            return GetBetStatusImageMappings(battleBetId)[betStatus];
        }

        private IDictionary<BetStatus, StatusActionImageViewModel> GetBetStatusImageMappings(long battleBetId)
        {
            return new Dictionary<BetStatus, StatusActionImageViewModel>()
                { 
                    { BetStatus.Succeeded, new StatusActionImageViewModel(MVC.NotAdmin.Bets.BetSucceeded(battleBetId), "scr_2", Bets.Succeeded) },
                    { BetStatus.Failed, new StatusActionImageViewModel(MVC.NotAdmin.Bets.BetFailed(battleBetId), "scr_1", Bets.Failed) },
                    { BetStatus.CanceledByBookmaker, new StatusActionImageViewModel(MVC.NotAdmin.Bets.BetCanceledByBookmaker(battleBetId), "scr_3", Bets.CanceledByBookmaker) }
                };
        }

        private DateAndScreenshotViewModel GetDateAndScreenshot(BetScreenshot betScreenshot, bool screenshotIsVisible)
        {
            var dateAndScreenshot = new DateAndScreenshotViewModel
                                        {
                                            DateTime = betScreenshot.CreationDateTime.ToShortDateLongTimeString(),
                                            ScreenshotIsVisible = screenshotIsVisible
                                        };
            if (screenshotIsVisible)
            {
                dateAndScreenshot.ScreenshotStatusClass = GetBetScreenshotStatusClass(betScreenshot.StatusEnum);
                dateAndScreenshot.ScreenshotStatusString = GetBetScreenshotStatusString(betScreenshot.StatusEnum);
                if (betScreenshot.StatusEnum == BetScreenshotStatus.Succeeded)
                    dateAndScreenshot.ScreenshotUrl = _betScreenshotPathService.GetUrl(betScreenshot.FileName);
            }
            
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
                return Bets.Screenshot;
            else if (betScreenshotStatus == BetScreenshotStatus.Failed)
                return Bets.FailedToMakeScreenshot;
            else if (betScreenshotStatus == BetScreenshotStatus.NotProcessed)
                return Bets.MakingScreenshot;
            else
                throw new ArgumentOutOfRangeException("betScreenshotStatus");
        }
    }
}
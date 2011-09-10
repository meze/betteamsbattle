using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Transactions;
using System.Web.Mvc;
using BetTeamsBattle.Data.Model.Entities;
using BetTeamsBattle.Data.Model.Enums;
using BetTeamsBattle.Data.Repositories.Base.Interfaces;
using BetTeamsBattle.Data.Services.Interfaces;
using BetTeamsBattle.Data.Services.Tests.Helpers;
using BetTeamsBattle.Frontend.Areas.NotAdmin.Models.BattleBets;
using BetTeamsBattle.Frontend.Areas.NotAdmin.ViewServices.Battles.Interfaces;
using BetTeamsBattle.Frontend.Extensions;
using BetTeamsBattle.Frontend.Tests.DI;
using NUnit.Framework;
using Ninject;
using Resources;

namespace BetTeamsBattle.Frontend.Tests.Areas.NotAdmin.ViewServices
{
    [TestFixture]
    internal class BetViewServiceTests
    {
        private TransactionScope _transactionScope;
        private Creator _creator;

        private IBetsViewService _betsViewService;

        private IRepository<Bet> _repositoryOfBet; 

        private long _battleId;
        private long _teamId;
        private long _userId;
        private long _notMeUserId;

        private Bet _myOpenedPrivateBet;
        private Bet _myClosedPrivateBet;
        private Bet _myOpenedPublicBet;
        private Bet _myClosedPublicBet;
        private Bet _myOtherBattleOpenedPrivateBet;
        private Bet _notMyOpenedPrivateBet;
        
        [SetUp]
        public void Setup()
        {
            _transactionScope = new TransactionScope();

            var kernel = TestNinjectKernel.Kernel;
            _creator = kernel.Get<Creator>();

            var repositoryOfTeamUser = kernel.Get<IRepository<TeamUser>>();
            _repositoryOfBet = kernel.Get<IRepository<Bet>>();

            var usersService = kernel.Get<IUsersService>();
            var battlesService = kernel.Get<IBattlesService>();
            
            _betsViewService = kernel.Get<IBetsViewService>();

            _userId = usersService.Register("login", "openIdUrl", Language.English);
            _notMeUserId = usersService.Register("login1", "openIdUrl2", Language.Russian);
            _teamId = repositoryOfTeamUser.All().Where(tu => tu.UserId == _userId).Select(tu => tu.TeamId).SingleOrDefault();

            _battleId = battlesService.CreateBattle(DateTime.UtcNow, DateTime.UtcNow, BattleType.FixedBudget, 10000);
            var otherBattleId = battlesService.CreateBattle(DateTime.UtcNow, DateTime.UtcNow, BattleType.FixedBudget, 10000);

            var myOpenedPublicBetId = battlesService.MakeBet(_battleId, _teamId, _userId, "title1", 100, 1.5, "http://url2", false);

            var myClosedPublicBetId = battlesService.MakeBet(_battleId, _teamId, _userId, "title2", 10, 10, "http://url1", false);
            long closedBattleId;
            battlesService.BetSucceeded(myClosedPublicBetId, _userId, out closedBattleId);

            var myOpenedPrivateBetId = battlesService.MakeBet(_battleId, _teamId, _userId, "title3", 100, 10, "http://url3", true);

            var myClosedPrivateBetId = battlesService.MakeBet(_battleId, _teamId, _userId, "title4", 100, 10, "http://url4", true);
            battlesService.BetSucceeded(myClosedPrivateBetId, _userId, out closedBattleId);

            var notMyOpenedPrivateBetId = battlesService.MakeBet(_battleId, _teamId, _notMeUserId, "title5", 100, 2, "http://url3", true);
            var myOtherBattleOpenedPrivateBetId = battlesService.MakeBet(otherBattleId, _teamId, _userId, "title6", 100, 2, "http://url4", true);

            _myOpenedPrivateBet = GetBet(myOpenedPrivateBetId);
            _myClosedPrivateBet = GetBet(myClosedPrivateBetId);
            _myOpenedPublicBet = GetBet(myOpenedPublicBetId);
            _myClosedPublicBet = GetBet(myClosedPublicBetId);
            _myOtherBattleOpenedPrivateBet = GetBet(myOtherBattleOpenedPrivateBetId);
            _notMyOpenedPrivateBet = GetBet(notMyOpenedPrivateBetId);
        }

        [TearDown]
        public void TearDown()
        {
            _transactionScope .Dispose();
        }

        [Test]
        public void GetMyBattleBets_UserIdIsNull_NoBets()
        {
            var betsViewModel = _betsViewService.GetMyBattleBets(_battleId, null);

            Assert.AreEqual(BattleBets.MyBets, betsViewModel.Title);
            Assert.AreEqual(BattleBets.YouHaveMadeNoBetsInThisBattle, betsViewModel.NoBetsMessage);

            Assert.AreEqual(0, betsViewModel.Bets.Count());
        }

        [Test]
        public void GetMyBattleBets_UserIdIsNotNull()
        {
            var betsViewModel = _betsViewService.GetMyBattleBets(_battleId, _userId);

            Assert.AreEqual(BattleBets.MyBets, betsViewModel.Title);
            Assert.AreEqual(BattleBets.YouHaveMadeNoBetsInThisBattle, betsViewModel.NoBetsMessage);

            var bets = betsViewModel.Bets;
            Assert.AreEqual(4, bets.Count());
            AssertOpenedMyBattleBet(_myOpenedPrivateBet, bets.Where(bb => bb.BetId == _myOpenedPrivateBet.Id).Single());
            AssertClosedMyBattleBet(_myClosedPrivateBet, bets.Where(bb => bb.BetId == _myClosedPrivateBet.Id).Single());
            AssertOpenedMyBattleBet(_myOpenedPublicBet, bets.Where(bb => bb.BetId == _myOpenedPublicBet.Id).Single());
            AssertClosedMyBattleBet(_myClosedPublicBet, bets.Where(bb => bb.BetId == _myClosedPublicBet.Id).Single());
        }

        [Test]
        public void GetUserBets_Me()
        {
            var betsViewModel = _betsViewService.GetUserBets(_userId, _userId);

            Assert.AreEqual(BattleBets.MyBets, betsViewModel.Title);
            Assert.AreEqual(BattleBets.YouHaveMadeNoBets, betsViewModel.NoBetsMessage);

            var bets = betsViewModel.Bets;
            Assert.AreEqual(5, bets.Count());
            AssertClosedMyBet(_myClosedPrivateBet, bets.Where(bb => bb.BetId == _myClosedPrivateBet.Id).Single());
            AssertOpenedMyBet(_myOpenedPrivateBet, bets.Where(bb => bb.BetId == _myOpenedPrivateBet.Id).Single());
            AssertClosedMyBet(_myClosedPublicBet, bets.Where(bb => bb.BetId == _myClosedPublicBet.Id).Single());
            AssertOpenedMyBet(_myOpenedPublicBet, bets.Where(bb => bb.BetId == _myOpenedPublicBet.Id).Single());
            AssertOpenedMyBet(_myOtherBattleOpenedPrivateBet, bets.Where(bb => bb.BetId == _myOtherBattleOpenedPrivateBet.Id).Single());
        }

        [Test]
        public void GetUserBets_NotMe()
        {
            var betsViewModel = _betsViewService.GetUserBets(_userId, _notMeUserId);

            Assert.AreEqual(BattleBets.UserBets, betsViewModel.Title);
            Assert.AreEqual(BattleBets.UserHasMadeNoBets, betsViewModel.NoBetsMessage);

            var bets = betsViewModel.Bets;

            Assert.AreEqual(5, bets.Count());
            AssertOpenedPrivateUserBet(_myOpenedPrivateBet, bets.Where(bb => bb.BetId == _myOpenedPrivateBet.Id).Single());
            AssertClosedPrivateUserBet(_myClosedPrivateBet, bets.Where(bb => bb.BetId == _myClosedPrivateBet.Id).Single());
            AssertOpenedPublicUserBet(_myOpenedPublicBet, bets.Where(bb => bb.BetId == _myOpenedPublicBet.Id).Single());
            AssertClosedPublicUserBet(_myClosedPublicBet, bets.Where(bb => bb.BetId == _myClosedPublicBet.Id).Single());
            AssertOpenedPrivateUserBet(_myOtherBattleOpenedPrivateBet, bets.Where(bb => bb.BetId == _myOtherBattleOpenedPrivateBet.Id).Single());
        }

        [Test]
        public void GetTeamBets()
        {
            var betsViewModel = _betsViewService.GetTeamBets(_teamId);

            Assert.AreEqual(BattleBets.TeamBets, betsViewModel.Title);
            Assert.AreEqual(BattleBets.TeamHasMadeNoBets, betsViewModel.NoBetsMessage);

            var bets = betsViewModel.Bets;
            Assert.AreEqual(6, bets.Count());
            AssertOpenedPrivateTeamBet(_myOpenedPrivateBet, bets.Where(bb => bb.BetId == _myOpenedPrivateBet.Id).Single());
            AssertClosedPrivateTeamBet(_myClosedPrivateBet, bets.Where(bb => bb.BetId == _myClosedPrivateBet.Id).Single());
            AssertOpenedPublicTeamBet(_myOpenedPublicBet, bets.Where(bb => bb.BetId == _myOpenedPublicBet.Id).Single());
            AssertClosedPublicTeamBet(_myClosedPublicBet, bets.Where(bb => bb.BetId == _myClosedPublicBet.Id).Single());
            AssertOpenedPrivateTeamBet(_notMyOpenedPrivateBet, bets.Where(bb => bb.BetId == _notMyOpenedPrivateBet.Id).Single());
            AssertOpenedPrivateTeamBet(_myOtherBattleOpenedPrivateBet, bets.Where(bb => bb.BetId == _myOtherBattleOpenedPrivateBet.Id).Single());
        }

        private Bet GetBet(long betId)
        {
            return _repositoryOfBet.All().Where(bb => bb.Id == betId).Include(bb => bb.OpenBetScreenshot).Include(bb => bb.CloseBetScreenshot).Include(bb => bb.Battle).Include(bb => bb.User).Single(); ;
        }

        private void AssertOpenedMyBattleBet(Bet bet, BetViewModel betViewModel)
        {
            AssertVisibleBetCommon(bet, false, true, betViewModel);

            AssertNotClosedEditableBetCommon(bet, betViewModel);
        }

        private void AssertClosedMyBattleBet(Bet bet, BetViewModel betViewModel)
        {
            AssertVisibleBetCommon(bet, true, true, betViewModel);

            AssertClosedBetCommon(bet, true, betViewModel);
        }

        private void AssertOpenedMyBet(Bet bet, BetViewModel betViewModel)
        {
            AssertOpenedMyBattleBet(bet, betViewModel);
            //ToDo: Add battle-related properties validation
        }

        private void AssertClosedMyBet(Bet bet, BetViewModel betViewModel)
        {
            AssertClosedMyBattleBet(bet, betViewModel);
            //ToDo: Add battle-related properties validation
        }

        private void AssertOpenedPrivateUserBet(Bet bet, BetViewModel betViewModel)
        {
            AssertNotVisibleBetCommon(bet, false, false, "eye_red", BattleBets.BetIsPrivate, betViewModel);

            AssertNotClosedNotEditableBetCommon(bet, betViewModel);
        }

        private void AssertOpenedPublicUserBet(Bet bet, BetViewModel betViewModel)
        {
            AssertNotVisibleBetCommon(bet, false, false, "eye_blek", BattleBets.BetIsNotClosed, betViewModel);

            AssertNotClosedNotEditableBetCommon(bet, betViewModel);
        }

        private void AssertClosedPrivateUserBet(Bet bet, BetViewModel betViewModel)
        {
            AssertNotVisibleBetCommon(bet, true, false, "eye_red", BattleBets.BetIsPrivate, betViewModel);

            AssertClosedBetCommon(bet, false, betViewModel);
        }

        private void AssertClosedPublicUserBet(Bet bet, BetViewModel betViewModel)
        {
            AssertVisibleBetCommon(bet, true, false, betViewModel);

            AssertClosedBetCommon(bet, true, betViewModel);
        }

        private void AssertOpenedPrivateTeamBet(Bet beet, BetViewModel betViewModel)
        {
            AssertOpenedPrivateUserBet(beet, betViewModel);
            //ToDo: Add Battle and User validation
        }

        private void AssertOpenedPublicTeamBet(Bet bet, BetViewModel betViewModel)
        {
            AssertOpenedPublicUserBet(bet, betViewModel);
            //ToDo: Add user and battle validation
        }

        private void AssertClosedPrivateTeamBet(Bet bet, BetViewModel betViewModel)
        {
            AssertClosedPrivateUserBet(bet, betViewModel);
            //ToDo: Add user and battle validation
        }

        private void AssertClosedPublicTeamBet(Bet bet, BetViewModel betViewModel)
        {
            AssertClosedPublicUserBet(bet, betViewModel);
            //ToDo: Add user and battle validation
        }

        private void AssertVisibleBetCommon(Bet bet, bool isClosed, bool isEditable, BetViewModel betViewModel)
        {
            Assert.AreEqual(bet.Id, betViewModel.BetId);
            Assert.AreEqual(bet.Title, betViewModel.Title);
            Assert.AreEqual(bet.Url, betViewModel.Url);
            Assert.AreEqual(bet.Amount, betViewModel.Amount);
            Assert.AreEqual(bet.Coefficient, betViewModel.Coefficient);

            Assert.AreEqual(isClosed, betViewModel.IsClosed);
            Assert.AreEqual(isEditable, betViewModel.IsEditable);

            Assert.IsTrue(betViewModel.IsVisible);
            Assert.IsNull(betViewModel.InvisibleIconClass);

            AssertVisibleDateAndScreenshot(bet.OpenBetScreenshot.CreationDateTime, BattleBets.MakingScreenshot, "sc_inprogress", null, betViewModel.OpenDateAndScreenshot);
        }

        private void AssertNotVisibleBetCommon(Bet bet, bool isClosed, bool isEditable, string invisibleIconClass, string invisibleIconTitle, BetViewModel betViewModel)
        {
            Assert.AreEqual(bet.Id, betViewModel.BetId);
            Assert.IsNull(betViewModel.Title);
            Assert.IsNull(betViewModel.Url);
            Assert.AreEqual(0, betViewModel.Amount);
            Assert.AreEqual(0, betViewModel.Coefficient);

            Assert.AreEqual(isClosed, betViewModel.IsClosed);
            Assert.AreEqual(isEditable, betViewModel.IsEditable);

            Assert.IsFalse(betViewModel.IsVisible);
            Assert.AreEqual(invisibleIconClass, betViewModel.InvisibleIconClass);
            Assert.AreEqual(invisibleIconTitle, betViewModel.InvisibleIconTitle);

            AssertNotVisibleDateAndScreenshot(bet.OpenBetScreenshot.CreationDateTime, betViewModel.OpenDateAndScreenshot);
        }

        private void AssertVisibleDateAndScreenshot(DateTime dateTime, string screenshotStatusString, string screenshotStatusClass, string screenshotUrl, DateAndScreenshotViewModel dateAndScreenshotViewModel)
        {
            Assert.AreEqual(dateTime.ToShortDateLongTimeString(), dateAndScreenshotViewModel.DateTime);
            Assert.IsTrue(dateAndScreenshotViewModel.ScreenshotIsVisible);
            Assert.AreEqual(screenshotStatusString, dateAndScreenshotViewModel.ScreenshotStatusString);
            Assert.AreEqual(screenshotStatusClass, dateAndScreenshotViewModel.ScreenshotStatusClass);
            Assert.AreEqual(screenshotUrl, dateAndScreenshotViewModel.ScreenshotUrl);
        }

        private void AssertNotVisibleDateAndScreenshot(DateTime dateTime, DateAndScreenshotViewModel dateAndScreenshotViewModel)
        {
            Assert.AreEqual(dateTime.ToShortDateLongTimeString(), dateAndScreenshotViewModel.DateTime);
            Assert.IsFalse(dateAndScreenshotViewModel.ScreenshotIsVisible);
            Assert.IsNull(dateAndScreenshotViewModel.ScreenshotStatusString);
            Assert.IsNull(dateAndScreenshotViewModel.ScreenshotStatusClass);
            Assert.IsNull(dateAndScreenshotViewModel.ScreenshotUrl);
        }

        private void AssertClosedBetCommon(Bet bet, bool visible, BetViewModel betViewModel)
        {
            if (visible)
                AssertVisibleDateAndScreenshot(bet.CloseBetScreenshot.CreationDateTime, BattleBets.MakingScreenshot, "sc_inprogress", null, betViewModel.CloseDateAndScreenshot);
            else
                AssertNotVisibleDateAndScreenshot(bet.CloseBetScreenshot.CreationDateTime, betViewModel.CloseDateAndScreenshot);
            AssertSucceedStatusActionImage(bet.Id, betViewModel.StatusActionImage);
            Assert.AreEqual(bet.Result, betViewModel.Result);

            Assert.IsNull(betViewModel.StatusActionImages);
        }

        private void AssertNotClosedEditableBetCommon(Bet bet, BetViewModel betViewModel)
        {
            Assert.IsNull(betViewModel.CloseDateAndScreenshot);
            Assert.IsNull(betViewModel.StatusActionImage);
            Assert.AreEqual(0, betViewModel.Result);

            AssertStatusActionImages(bet.Id, betViewModel.StatusActionImages);
        }

        private void AssertNotClosedNotEditableBetCommon(Bet bet, BetViewModel betViewModel)
        {
            Assert.IsNull(betViewModel.CloseDateAndScreenshot);
            Assert.IsNull(betViewModel.StatusActionImage);
            Assert.AreEqual(0, betViewModel.Result);

            Assert.IsNull(betViewModel.StatusActionImages);
        }

        private void AssertStatusActionImages(long battleBetId, IEnumerable<StatusActionImageViewModel> statusActionImages)
        {
            Assert.AreEqual(3, statusActionImages.Count());
            
            AssertSucceedStatusActionImage(battleBetId, statusActionImages.ElementAt(0));
            AssertFailedStatusActionImage(battleBetId, statusActionImages.ElementAt(1));
            AssertCanceledByBookmakerActionImage(battleBetId, statusActionImages.ElementAt(2));
        }

        private void AssertSucceedStatusActionImage(long battleBetId, StatusActionImageViewModel statusActionImage)
        {
            AssertStatusActionImage(BattleBets.Succeeded, "scr_2", MVC.NotAdmin.BattleBets.BetSucceeded(battleBetId), statusActionImage);
        }

        private void AssertFailedStatusActionImage(long battleBetId, StatusActionImageViewModel statusActionImage)
        {
            AssertStatusActionImage(BattleBets.Failed, "scr_1", MVC.NotAdmin.BattleBets.BetFailed(battleBetId), statusActionImage);
        }

        private void AssertCanceledByBookmakerActionImage(long battleBetId, StatusActionImageViewModel statusActionImage)
        {
            AssertStatusActionImage(BattleBets.CanceledByBookmaker, "scr_3", MVC.NotAdmin.BattleBets.BetCanceledByBookmaker(battleBetId), statusActionImage);
        }

        private void AssertStatusActionImage(string title, string imageClass, ActionResult actionResult, StatusActionImageViewModel statusActionImage)
        {
            Assert.AreEqual(title, statusActionImage.Title);
            Assert.AreEqual(imageClass, statusActionImage.ImageClass);
            Assert.AreEqual(actionResult, statusActionImage.ActionResult);
        }
    }
}
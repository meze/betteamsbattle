using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
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

        public BattleBetsViewService(IRepository<BattleBet> repositoryOfBattleBet, IRepository<Team> repositoryOfTeam)
        {
            _repositoryOfBattleBet = repositoryOfBattleBet;
            _repositoryOfTeam = repositoryOfTeam;
        }

        public MakeBetViewModel MakeBet(long battleId, long userId, MakeBetFormViewModel makeBetFormViewModel)
        {
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
                        var selectListItem = new SelectListItem() {Value = t.Id.ToString(), Text = t.Title, Selected = isSelected};
                        return selectListItem;
                    }).
                ToList();
            var makeBetViewModel = new MakeBetViewModel(battleId, teamsSelectListItems, makeBetFormViewModel);

            return makeBetViewModel;
        }

        public MakeBetViewModel MakeBet(long battleId, long userId)
        {
            return MakeBet(battleId, userId, new MakeBetFormViewModel());
        }

        public IEnumerable<BattleBet> MyBets(long battleId, long userId)
        {
            var myBets = _repositoryOfBattleBet.
                   Get(BattleBetSpecifications.BattleIdAndUserIdAreEqualTo(battleId, userId)).
                   Include(bb => bb.OpenBetScreenshot).
                   Include(bb => bb.CloseBetScreenshot).
                   OrderByDescending(b => b.OpenDateTime).
                   ToList();

            foreach (var myBet in myBets)
            {
                //var myBetViewModel = new MyBetViewModel();
                //myBet.BattleId
                //myBet.Title
                //myBet.Url
                //myBet.Bet
                //myBet.OpenDateTime
                //myBet.OpenBetScreenshot.Status
                //OpenScreenshotUrl
                //myBet.CloseDateTime
                //myBet.CloseBetScreenshot.Status
                //myBet.IsPrivate
                //CloseScreenshotUrl  
            }

            return myBets;
        }
    }
}
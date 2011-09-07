using System;
using System.Linq;
using BetTeamsBattle.Data.Model.Entities;
using BetTeamsBattle.Data.Model.Enums;
using BetTeamsBattle.Data.Repositories.Base.Interfaces;

namespace BetTeamsBattle.Data.Services.Tests.Helpers
{
    public class EntityAssert
    {
        private readonly IRepository<Battle> _repositoryOfBattle;
        private readonly IRepository<Bet> _repositoryOfBet;
        private readonly IRepository<BattleTeamStatistics> _repositoryOfBattleTeamStatistics;
        private readonly IRepository<Team> _repositoryOfTeam;
        private IRepository<User> _repositoryOfUser;

        public EntityAssert(IRepository<Battle> repositoryOfBattle, IRepository<Bet> repositoryOfBet, IRepository<BattleTeamStatistics> repositoryOfBattleTeamStatistics, IRepository<Team> repositoryOfTeam, IRepository<User> repositoryOfUser)
        {
            _repositoryOfBattle = repositoryOfBattle;
            _repositoryOfBet = repositoryOfBet;
            _repositoryOfBattleTeamStatistics = repositoryOfBattleTeamStatistics;
            _repositoryOfTeam = repositoryOfTeam;
            _repositoryOfUser = repositoryOfUser;
        }

        public void OpenedBattleBet(long battleBetId, long battleId, long teamId, long userId, string _betTitle, double amount, double coefficient, string url, bool isPrivate, double teamRating, double battleTeamBalance, int openedBetsCount, int closedBetsCount)
        {
            _repositoryOfBet.All().Where(bb => bb.Id == battleBetId && bb.BattleId == battleId && bb.TeamId == teamId && bb.UserId == userId && bb.Title == _betTitle && bb.Url == url && bb.Amount == amount && bb.Coefficient == coefficient && bb.Result == null && bb.IsPrivate == isPrivate && bb.OpenBetScreenshot.Status == (sbyte)BetScreenshotStatus.NotProcessed).Single();
            Team(teamId, teamRating);
            BattleTeamStatistics(battleId, teamId, battleTeamBalance, openedBetsCount, closedBetsCount);
        }

        public void ClosedBattleBet(long battleBetId, BattleBetStatus status, double result, double teamRating, double battleTeamBalance, int openedBetsCount, int closedBetsCount)
        {
            var battleBet = _repositoryOfBet.All().Where(bb => bb.Id == battleBetId && bb.CloseDateTime != null && bb.CloseBetScreenshotId != null && bb.Status == (sbyte)status && bb.Result == result).Single();
            Team(battleBet.TeamId, teamRating);
            BattleTeamStatistics(battleBet.BattleId, battleBet.TeamId, battleTeamBalance, openedBetsCount, closedBetsCount);
        }

        public void BattleTeamStatistics(long battleId, long teamId, double balance, int openedBetsCount, int closedBetsCount)
        {
            _repositoryOfBattleTeamStatistics.All().Where(bts => bts.BattleId == battleId && bts.TeamId == teamId && bts.Balance == balance && bts.OpenedBetsCount == openedBetsCount && bts.ClosedBetsCount == closedBetsCount).Single();
        }

        public void Battle(DateTime startDate, DateTime endDate, BattleType battleType, double budget)
        {
            _repositoryOfBattle.All().Where(b => b.StartDate == startDate && b.EndDate == endDate && b.BattleType == (sbyte)battleType && b.Budget == budget).Single();
        }

        public void Team(long teamId, double rating)
        {
            _repositoryOfTeam.All().Where(t => t.Id == teamId && t.Rating == rating).Single();
        }

        public void ProTeam(string title, string description, string site)
        {
            _repositoryOfTeam.All().Where(t => !t.IsPersonal && t.IsPro && t.Rating == 0 && t.Title == title && t.Description == description && t.Site == site).Single();
        }

        public void UserAndPersonalTeam(string login, string openIdUrl, Language language)
        {
            var user = _repositoryOfUser.All().Where(u => u.Login == login && u.OpenIdUrl == openIdUrl && u.UserProfile.Language == (sbyte)language).Single();
            _repositoryOfTeam.All().Where(t => t.Title == login && t.IsPersonal && t.Rating == 0 && t.TeamUsers.Count() == 1 && t.TeamUsers.Any(tu => tu.UserId == user.Id)).Single();
        }
    }
}
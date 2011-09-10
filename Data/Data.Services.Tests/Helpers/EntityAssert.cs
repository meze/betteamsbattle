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
        private readonly IRepository<TeamStatistics> _repositoryOfTeamStatistics; 
        private readonly IRepository<TeamBattleStatistics> _repositoryOfBattleTeamStatistics;
        private readonly IRepository<Team> _repositoryOfTeam;
        private readonly IRepository<TeamUser> _repositoryOfTeamUser; 
        private readonly IRepository<User> _repositoryOfUser;

        public EntityAssert(IRepository<Battle> repositoryOfBattle, IRepository<Bet> repositoryOfBet, IRepository<TeamStatistics> repositoryOfTeamStatistics, IRepository<TeamBattleStatistics> repositoryOfTeamBattleStatistics, IRepository<Team> repositoryOfTeam, IRepository<User> repositoryOfUser, IRepository<TeamUser> repositoryOfTeamUser)
        {
            _repositoryOfBattle = repositoryOfBattle;
            _repositoryOfBet = repositoryOfBet;
            _repositoryOfTeamStatistics = repositoryOfTeamStatistics;
            _repositoryOfBattleTeamStatistics = repositoryOfTeamBattleStatistics;
            _repositoryOfTeam = repositoryOfTeam;
            _repositoryOfUser = repositoryOfUser;
            _repositoryOfTeamUser = repositoryOfTeamUser;
        }

        public void OpenedBattleBet(long battleBetId, long battleId, long teamId, long userId, string betTitle, double amount, double coefficient, string url, bool isPrivate, double teamRating, double teamBattleGain, int openedBetsCount, int closedBetsCount)
        {
            _repositoryOfBet.All().Where(bb => bb.Id == battleBetId && bb.BattleId == battleId && bb.TeamId == teamId && bb.UserId == userId && bb.Title == betTitle && bb.Url == url && bb.Amount == amount && bb.Coefficient == coefficient && bb.Result == null && bb.IsPrivate == isPrivate && bb.OpenBetScreenshot.Status == (sbyte)BetScreenshotStatus.NotProcessed).Single();
            Team(teamId);
            TeamStatistics(teamId, teamRating, openedBetsCount, closedBetsCount);
            TeamBattleStatistics(battleId, teamId, teamBattleGain, openedBetsCount, closedBetsCount);
        }

        public void ClosedBattleBet(long battleBetId, BattleBetStatus status, double result, double teamRating, double teamBattleGain, int openedBetsCount, int closedBetsCount)
        {
            var bet = _repositoryOfBet.All().Where(bb => bb.Id == battleBetId && bb.CloseDateTime != null && bb.CloseBetScreenshotId != null && bb.Status == (sbyte)status && bb.Result == result).Single();
            Team(bet.TeamId);
            TeamStatistics(bet.TeamId, teamRating, openedBetsCount, closedBetsCount);
            TeamBattleStatistics(bet.BattleId, bet.TeamId, teamBattleGain, openedBetsCount, closedBetsCount);
        }

        public void TeamStatistics(long teamId, double rating, int openedBetsCount, int closedBetsCount)
        {
            _repositoryOfTeamStatistics.All().Where(bts => bts.Id == teamId && bts.Rating == rating && bts.OpenedBetsCount == openedBetsCount && bts.ClosedBetsCount == closedBetsCount).Single();
        }

        public void TeamBattleStatistics(long battleId, long teamId, double gain, int openedBetsCount, int closedBetsCount)
        {
            _repositoryOfBattleTeamStatistics.All().Where(bts => bts.BattleId == battleId && bts.TeamId == teamId && bts.Gain == gain && bts.OpenedBetsCount == openedBetsCount && bts.ClosedBetsCount == closedBetsCount).Single();
        }

        public void Battle(DateTime startDate, DateTime endDate, BattleType battleType, double budget)
        {
            _repositoryOfBattle.All().Where(b => b.StartDate == startDate && b.EndDate == endDate && b.BattleType == (sbyte)battleType && b.Budget == budget).Single();
        }

        public void Team(long teamId)
        {
            _repositoryOfTeam.All().Where(t => t.Id == teamId).Single();
        }

        public void ProTeam(string title, string description, string site)
        {
            var team = _repositoryOfTeam.All().Where(t => !t.IsPersonal && t.IsPro && t.Title == title && t.Description == description && t.Site == site).Single();
            TeamStatistics(team.Id, 0, 0, 0);
        }

        public Team PersonalTeam(string title)
        {
            return _repositoryOfTeam.All().Where(t => t.Title == title && t.IsPersonal).Single();
        }

        public void JoinedTeamUser(long teamId, long userId)
        {
            _repositoryOfTeamUser.All().Where(ts => ts.TeamId == teamId && ts.UserId == ts.UserId && ts.Action == (sbyte)TeamUserAction.Join).Single();
        }

        public void User(string login, string openIdUrl, Language language)
        {
            var user = _repositoryOfUser.All().Where(u => u.Login == login && u.OpenIdUrl == openIdUrl && u.UserProfile.Language == (sbyte)language).Single();
            var team = PersonalTeam(login);
            TeamStatistics(team.Id, 0, 0, 0);
            JoinedTeamUser(team.Id, user.Id);
        }
    }
}
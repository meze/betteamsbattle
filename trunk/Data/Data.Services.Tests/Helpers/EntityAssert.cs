using System;
using System.Linq;
using BetTeamsBattle.Data.Model.Entities;
using BetTeamsBattle.Data.Model.Enums;
using BetTeamsBattle.Data.Repositories.Base.Interfaces;

namespace BetTeamsBattle.Data.Services.Tests.Helpers
{
    internal class EntityAssert
    {
        private readonly IRepository<Battle> _repositoryOfBattle;
        private readonly IRepository<BattleBet> _repositoryOfBattleBet;
        private readonly IRepository<BattleTeamStatistics> _repositoryOfBattleTeamStatistics;
        private readonly IRepository<Team> _repositoryOfTeam;
        private IRepository<User> _repositoryOfUser;

        public EntityAssert(IRepository<Battle> repositoryOfBattle, IRepository<BattleBet> repositoryOfBattleBet, IRepository<BattleTeamStatistics> repositoryOfBattleTeamStatistics, IRepository<Team> repositoryOfTeam, IRepository<User> repositoryOfUser)
        {
            _repositoryOfBattle = repositoryOfBattle;
            _repositoryOfBattleBet = repositoryOfBattleBet;
            _repositoryOfBattleTeamStatistics = repositoryOfBattleTeamStatistics;
            _repositoryOfTeam = repositoryOfTeam;
            _repositoryOfUser = repositoryOfUser;
        }

        public void OpenedBattleBet(long battleBetId, long battleId, long teamId, long userId, string _betTitle, double bet, double coefficient, string url, bool isPrivate)
        {
            _repositoryOfBattleBet.All().Where(bb => bb.Id == battleBetId && bb.BattleId == battleId && bb.TeamId == teamId && bb.UserId == userId && bb.Title == _betTitle && bb.Bet == bet && bb.Coefficient == coefficient && bb.Url == url && bb.IsPrivate == isPrivate && bb.OpenBetScreenshot.Status == (sbyte)BetScreenshotStatus.NotProcessed).Single();
        }

        public void ClosedBattleBet(long battleBetId, BattleBetStatus status)
        {
            _repositoryOfBattleBet.All().Where(bb => bb.Id == battleBetId && bb.CloseDateTime != null && bb.CloseBetScreenshotId != null && bb.Status == (sbyte)status).Single();
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
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BetTeamsBattle.Data.Model.Entities;
using BetTeamsBattle.Data.Repositories.Base.Interfaces;
using BetTeamsBattle.Data.Repositories.Specifications;
using BetTeamsBattle.Frontend.Areas.NotAdmin.Models.Teams;
using BetTeamsBattle.Frontend.Areas.NotAdmin.Models.Users;
using BetTeamsBattle.Frontend.Areas.NotAdmin.ViewServices.Battles.Interfaces;
using Resources;

namespace BetTeamsBattle.Frontend.Areas.NotAdmin.ViewServices.Battles
{
    internal class TeamsViewService : ITeamsViewService
    {
        private readonly IRepository<Team> _repositoryOfTeam;
        private readonly IRepository<TeamUser> _repositoryOfTeamUser;
        private readonly IRepository<TeamBattleStatistics> _repositoryOfBattleTeamStatistics;
        private readonly IRepository<TeamStatistics> _repositoryOfTeamStatistics;
        private readonly IRepository<User> _repositoryOfUser;

        public TeamsViewService(IRepository<Team> repositoryOfTeam, IRepository<TeamBattleStatistics> repositoryOfBattleTeamStatistics, IRepository<TeamUser> repositoryOfTeamUser, IRepository<TeamStatistics> repositoryOfTeamStatistics, IRepository<User> repositoryOfUser)
        {
            _repositoryOfTeam = repositoryOfTeam;
            _repositoryOfBattleTeamStatistics = repositoryOfBattleTeamStatistics;
            _repositoryOfTeamUser = repositoryOfTeamUser;
            _repositoryOfTeamStatistics = repositoryOfTeamStatistics;
            _repositoryOfUser = repositoryOfUser;
        }

        public TeamViewModel GetTeam(long teamId)
        {
            var team = _repositoryOfTeam.Get(EntitySpecifications.IdIsEqualTo<Team>(teamId)).Include(t => t.TeamStatistics).Single();
            
            var teamViewModel = new TeamViewModel()
                                    {
                                        TeamId = team.Id,
                                        Title = team.Title,
                                    };

            teamViewModel.Statistics = GetTeamStatisticsViewModel(team.TeamStatistics, Teams.TeamStatistics);

            return teamViewModel;
        }

        public PersonalTeamViewModel GetPersonalTeam(long userId)
        {
            var user = _repositoryOfUser.Get(EntitySpecifications.IdIsEqualTo<User>(userId)).Single();

            var personalTeamViewModel = new PersonalTeamViewModel() { UserId = user.Id, Login = user.Login };

            var personalTeamStatistics = _repositoryOfTeamStatistics.All().Where(ts => ts.Team.IsPersonal && ts.Team.TeamUsers.Any(tu => tu.UserId == userId)).Single();

            personalTeamViewModel.Statistics = GetTeamStatisticsViewModel(personalTeamStatistics, Teams.UserStatistics);

            return personalTeamViewModel;
        }

        private TeamStatisticsViewModel GetTeamStatisticsViewModel(TeamStatistics teamStatistics, string title)
        {
            return new TeamStatisticsViewModel(title, teamStatistics.Rating, teamStatistics.OpenedBetsCount,
                                               teamStatistics.ClosedBetsCount);
        }

        public IEnumerable<TopTeamViewModel> TopTeams()
        {
            var topTeams = _repositoryOfTeam.All().OrderByDescending(t => t.TeamStatistics.Rating).
                Skip(0).Take(10).
                Select(t => new TopTeamViewModel() { TeamId = t.Id, Title = t.Title, Rating = t.TeamStatistics.Rating, IsPro = t.IsPro, IsPersonal = t.IsPersonal }).
                ToList();
            return ProcessTopTeams(topTeams);
        }

        public IEnumerable<TopTeamViewModel> BattleTopTeams(long battleId)
        {
            //ToDo: rework when http://bugs.mysql.com/bug.php?id=62349&thanks=5&notify=3 is fixed
            var battleTopTeams = _repositoryOfBattleTeamStatistics.Get(TeamBattleStatisticsSpecifications.BattleIdIsEqualTo(battleId)).
                    OrderByDescending(bts => bts.Gain).
                    Skip(0).Take(10).
                    Select(bts => new TopTeamViewModel() { TeamId = bts.TeamId, Title = bts.Team.Title, Rating = bts.Gain, IsPro = bts.Team.IsPro, IsPersonal = bts.Team.IsPersonal }).
                    ToList();
            return ProcessTopTeams(battleTopTeams);
        }

        private IEnumerable<TopTeamViewModel> ProcessTopTeams(IEnumerable<TopTeamViewModel> topTeams)
        {
            SetUserIdForPersonalTeams(topTeams);
            SetTeamOrUserActionResult(topTeams);
            return topTeams;
        }

        private void SetUserIdForPersonalTeams(IEnumerable<TopTeamViewModel> topTeams)
        {
            var personalTeams = topTeams.Where(tt => tt.IsPersonal).Select(tt => tt.TeamId).ToList();
            var personalTeamsUsersMapping = _repositoryOfTeamUser.All().Where(tu => personalTeams.Contains(tu.TeamId)).ToDictionary(tu => tu.TeamId, tu => tu.UserId);

            foreach (var topTeam in topTeams)
                if (topTeam.IsPersonal)
                    topTeam.UserId = personalTeamsUsersMapping[topTeam.TeamId];
        }

        private void SetTeamOrUserActionResult(IEnumerable<TopTeamViewModel> topTeams)
        {
            foreach (var topTeam in topTeams)
                topTeam.TeamOrUserActionResult = topTeam.IsPersonal
                                                     ? MVC.NotAdmin.Teams.GetPersonalTeam(topTeam.UserId)
                                                     : MVC.NotAdmin.Teams.GetTeam(topTeam.TeamId);
        }
    }
}
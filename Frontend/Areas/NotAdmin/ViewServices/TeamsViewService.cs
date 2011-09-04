using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BetTeamsBattle.Data.Model.Entities;
using BetTeamsBattle.Data.Repositories.Base.Interfaces;
using BetTeamsBattle.Data.Repositories.Specifications;
using BetTeamsBattle.Frontend.Areas.NotAdmin.Models.Teams;
using BetTeamsBattle.Frontend.Areas.NotAdmin.ViewServices.Battles.Interfaces;

namespace BetTeamsBattle.Frontend.Areas.NotAdmin.ViewServices.Battles
{
    internal class TeamsViewService : ITeamsViewService
    {
        private readonly IRepository<Team> _repositoryOfTeam;
        private readonly IRepository<BattleTeamStatistics> _repositoryOfBattleTeamStatistics;

        public TeamsViewService(IRepository<Team> repositoryOfTeam, IRepository<BattleTeamStatistics> repositoryOfBattleTeamStatistics)
        {
            _repositoryOfTeam = repositoryOfTeam;
            _repositoryOfBattleTeamStatistics = repositoryOfBattleTeamStatistics;
        }

        public IEnumerable<TopTeamViewModel> TopTeams()
        {
            var topTeams = _repositoryOfTeam.All().OrderByDescending(t => t.Rating).
                Skip(0).Take(10).
                Select(t => new TopTeamViewModel() { TeamId =  t.Id, Title = t.Title, Rating = t.Rating, IsPro = t.IsPro }).
                ToList();
            return topTeams;
        }

        public IEnumerable<TopTeamViewModel> BattleTopTeams(long battleId)
        {
            var battleTopTeams = _repositoryOfBattleTeamStatistics.Get(BattleTeamStatisticsSpecifications.BattleIdIsEqualTo(battleId)).
                    OrderByDescending(bts => bts.Balance).
                    Skip(0).Take(10).
                    Select(
                        bus =>
                        new TopTeamViewModel() { TeamId = bus.TeamId, Title = bus.Team.Title, Rating = bus.Balance, IsPro = bus.Team.IsPro }).
                    ToList();
            return battleTopTeams;
        }
    }
}
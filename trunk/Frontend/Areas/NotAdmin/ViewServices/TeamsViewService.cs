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

        public TeamsViewService(IRepository<Team> repositoryOfTeam)
        {
            _repositoryOfTeam = repositoryOfTeam;
        }

        public IEnumerable<TeamViewModel> TopTeams()
        {
            return _repositoryOfTeam.All().OrderByDescending(t => t.Rating).
                Skip(0).Take(10).
                Select(t => new TeamViewModel() { TeamId =  t.Id, Title = t.Title, Rating = t.Rating, IsPro = t.IsPro }).
                ToList();
        }
    }
}
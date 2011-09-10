using BetTeamsBattle.Data.Model.Entities;
using BetTeamsBattle.Data.Repositories.Base.Interfaces;
using BetTeamsBattle.Data.Repositories.UnitOfWork.Interfaces;
using BetTeamsBattle.Data.Services.Interfaces;

namespace BetTeamsBattle.Data.Services
{
    internal class TeamsService : ITeamsService
    {
        private readonly IUnitOfWorkScopeFactory _unitOfWorkScopeFactory;
        private IRepository<Team> _repositoryOfTeam;

        public TeamsService(IUnitOfWorkScopeFactory unitOfWorkScopeFactory, IRepository<Team> repositoryOfTeam)
        {
            _unitOfWorkScopeFactory = unitOfWorkScopeFactory;
            _repositoryOfTeam = repositoryOfTeam;
        }

        public long CreateProTeam(string title, string description, string site)
        {
            using (var unitOfWorkScope = _unitOfWorkScopeFactory.Create())
            {
                var proTeam = Team.CreateProTeam(title, description, site);
                _repositoryOfTeam.Add(proTeam);

                unitOfWorkScope.SaveChanges();

                return proTeam.Id;
            }
        }
    }
}
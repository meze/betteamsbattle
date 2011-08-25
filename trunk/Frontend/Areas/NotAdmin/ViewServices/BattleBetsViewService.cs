using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BetTeamsBattle.Data.Model.Entities;
using BetTeamsBattle.Data.Repositories.Base.Interfaces;
using BetTeamsBattle.Data.Repositories.Specifications;
using BetTeamsBattle.Frontend.Areas.NotAdmin.ViewServices.Battles.Interfaces;

namespace BetTeamsBattle.Frontend.Areas.NotAdmin.ViewServices
{
    internal class BattleBetsViewService : IBattleBetsViewService
    {
        private readonly IRepository<BattleBet> _repositoryOfBattleBet;

        public BattleBetsViewService(IRepository<BattleBet> repositoryOfBattleBet)
        {
            _repositoryOfBattleBet = repositoryOfBattleBet;
        }

        public IEnumerable<BattleBet> MyBets(long battleId, long userId)
        {
            var myBets = _repositoryOfBattleBet.
                   Get(BattleBetSpecifications.BattleIdAndUserIdAreEqualTo(battleId, userId)).
                   Include(bb => bb.OpenBetScreenshot).
                   Include(bb => bb.CloseBetScreenshot).
                   OrderByDescending(b => b.OpenDateTime).
                   ToList();

            return myBets;
        }
    }
}
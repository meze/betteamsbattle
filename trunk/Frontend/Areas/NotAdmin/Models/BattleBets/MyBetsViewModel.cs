using System.Collections.Generic;
using BetTeamsBattle.Data.Model.Entities;

namespace BetTeamsBattle.Frontend.Areas.NotAdmin.Models.BattleBets
{
    public class MyBetsViewModel
    {
        public long BattleId { get; set; }
        public IEnumerable<BattleBet> MyBets { get; set; }

        public MyBetsViewModel()
        {
        }

        public MyBetsViewModel(long battleId, IEnumerable<BattleBet> myBets)
        {
            BattleId = battleId;
            MyBets = myBets;
        }
    }
}
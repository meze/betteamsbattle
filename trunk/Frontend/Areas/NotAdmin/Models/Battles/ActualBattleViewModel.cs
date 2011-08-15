using System;
using System.Web.Mvc;

namespace BetTeamsBattle.Frontend.Areas.NotAdmin.Models.Battle
{
    public class ActualBattleViewModel
    {
        public long Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Budget { get; set; }
        public double Earned { get; set; }
        public double EarnedPercents { get; set; }
        public int TotalBetsCount { get; set; }
        public int OpenBetsCount { get; set; }

        public bool IsJoined { get; set; }
        public ActionResult JoinOrLeaveActionResult { get; set; }
        public string JoinOrLeaveTitle { get; set; }
    }
}
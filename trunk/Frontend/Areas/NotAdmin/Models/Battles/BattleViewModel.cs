using System;
using System.Web.Mvc;

namespace BetTeamsBattle.Frontend.Areas.NotAdmin.Models.Battle
{
    public class BattleViewModel
    {
        public long Id { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int Budget { get; set; }
        public int BetLimit { get; set; }

        public bool IsActive { get; set; }

        public bool UserIsJoined { get; set; }
        public string JoinOrLeaveTitle { get; set; }
        public ActionResult JoinOrLeaveActionResult { get; set; }

        public double Earned { get; set; }
        public double EarnedPercents { get; set; }
        public int TotalBetsCount { get; set; }
        public int OpenBetsCount { get; set; }

        public BattleViewModel(long id, string startDate, string endDate, int budget, int betLimit, bool isActive)
        {
            Id = id;
            StartDate = startDate;
            EndDate = endDate;
            Budget = budget;
            BetLimit = betLimit;
            IsActive = isActive;
        }
    }
}
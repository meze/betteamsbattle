using System;
using System.Collections.Generic;
using BetTeamsBattle.Data.Model.Enums;

namespace BetTeamsBattle.Frontend.Areas.NotAdmin.Models.BattleBets
{
    public class MyBetViewModel
    {
        public long BattleBetId { get; set; }
        public long BattleId { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public double Bet { get; set; }
        public double Coefficient { get; set; }
        public DateAndScreenshotViewModel OpenDateAndScreenshot { get; set; }
        public DateAndScreenshotViewModel CloseDateAndScreenshot { get; set; }
        public bool IsPrivate { get; set; }
        public bool IsClosed { get; set; }
        public BattleBetStatus Status { get; set; }
        public StatusActionImageViewModel StatusActionImage { get; set; }
        public IDictionary<BattleBetStatus, StatusActionImageViewModel> StatusActionImages { get; set; }
    }
}
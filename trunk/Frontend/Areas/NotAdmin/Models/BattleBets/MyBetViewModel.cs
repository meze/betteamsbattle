using System;

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
        public string OpenDateTime { get; set; }
        public string OpenScreenshotStatus { get; set; }
        public string OpenScreenshotUrl { get; set; }
        public string CloseDateTime { get; set; }
        public string CloseScreenshotStatus { get; set; }
        public string CloseScreenshotUrl { get; set; }
        public bool IsPrivate { get; set; }
    }
}
using System;
using System.Collections.Generic;
using BetTeamsBattle.Data.Model.Enums;

namespace BetTeamsBattle.Frontend.Areas.NotAdmin.Models.BattleBets
{
    public class BetViewModel
    {
        public long BetId { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public double Amount { get; set; }
        public double Coefficient { get; set; }
        public DateAndScreenshotViewModel OpenDateAndScreenshot { get; set; }
        public DateAndScreenshotViewModel CloseDateAndScreenshot { get; set; }
        public StatusActionImageViewModel StatusActionImage { get; set; }
        public IEnumerable<StatusActionImageViewModel> StatusActionImages { get; set; }
        public double Result { get; set; }
        public bool IsClosed { get; set; }
        public bool IsVisible { get; set; }
        public bool IsEditable { get; set; }
        public string InvisibleIconClass { get; set; }
    }
}
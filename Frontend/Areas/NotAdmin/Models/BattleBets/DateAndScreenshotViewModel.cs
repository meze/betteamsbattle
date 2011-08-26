namespace BetTeamsBattle.Frontend.Areas.NotAdmin.Models.BattleBets
{
    public class DateAndScreenshotViewModel
    {
        public string DateTime { get; set; }
        public string ScreenshotStatus { get; set; }
        public string ScreenshotUrl { get; set; }

        public DateAndScreenshotViewModel(string dateTime, string screenshotStatus, string screenshotUrl)
        {
            DateTime = dateTime;
            ScreenshotStatus = screenshotStatus;
            ScreenshotUrl = screenshotUrl;
        }
    }
}
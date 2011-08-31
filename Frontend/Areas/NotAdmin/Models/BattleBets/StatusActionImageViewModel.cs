using System.Web.Mvc;

namespace BetTeamsBattle.Frontend.Areas.NotAdmin.Models.BattleBets
{
    public class StatusActionImageViewModel
    {
        public ActionResult ActionResult { get; set; }
        public string ImageClass { get; set; }
        public string Title { get; set; }

        public StatusActionImageViewModel(ActionResult actionResult, string imageClass, string title)
        {
            ActionResult = actionResult;
            ImageClass = imageClass;
            Title = title;
        }
    }
}
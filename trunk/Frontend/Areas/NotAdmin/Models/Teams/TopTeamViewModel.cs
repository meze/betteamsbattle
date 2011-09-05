using System.Web.Mvc;
using BetTeamsBattle.Data.Model.Entities;

namespace BetTeamsBattle.Frontend.Areas.NotAdmin.Models.Teams
{
    public class TopTeamViewModel
    {
        public long TeamId { get; set; }
        public string Title { get; set; }
        public double Rating { get; set; }
        public bool IsPro { get; set; }
        public bool IsPersonal { get; set; }
        public long UserId { get; set; }
        public ActionResult TeamOrUserActionResult { get; set; }
    }
}
using System.Collections.Generic;

namespace BetTeamsBattle.Frontend.Areas.Admin.Models.Teams
{
    public class CreateProTeamViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public IList<long?> UsersIds { get; set; }

        public CreateProTeamViewModel()
        {
            UsersIds = new List<long?>() { null, null, null, null, null };
        }
    }
}
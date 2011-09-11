using BetTeamsBattle.Data.Model.Entities;
using BetTeamsBattle.Frontend.Areas.NotAdmin.Models.Teams;

namespace BetTeamsBattle.Frontend.Areas.NotAdmin.Models.Users
{
    public class PersonalTeamViewModel
    {
        public long UserId { get; set; }
        public string Login { get; set; }

        public TeamStatisticsViewModel Statistics { get; set; }
    }
}
using System.Collections.Generic;

namespace BetTeamsBattle.Frontend.Areas.NotAdmin.Models.BattleBets
{
    public class BetsViewModel
    {
        public IEnumerable<BetViewModel> Bets { get; set; }
        public bool AllowEditing { get; set; }
        public bool DisplayUser { get; set; }
        public bool DisplayBattle { get; set; }
    }
}
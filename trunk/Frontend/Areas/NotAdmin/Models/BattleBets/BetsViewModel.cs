using System.Collections.Generic;

namespace BetTeamsBattle.Frontend.Areas.NotAdmin.Models.BattleBets
{
    public class BetsViewModel
    {
        public string Title { get; set; }
        public string NoBetsMessage { get; set; }
        public IEnumerable<BetViewModel> Bets { get; set; }

        public BetsViewModel()
        {
            Bets = new List<BetViewModel>();
        }
    }
}
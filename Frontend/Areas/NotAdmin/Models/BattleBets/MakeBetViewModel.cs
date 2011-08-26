using System.Collections.Generic;
using System.Web.Mvc;
using BetTeamsBattle.Data.Model.Entities;

namespace BetTeamsBattle.Frontend.Areas.NotAdmin.Models.BattleBets
{
    public class MakeBetViewModel : BattleBaseViewModel
    {
        public IEnumerable<SelectListItem> Teams { get; set; } 
        public MakeBetFormViewModel MakeBetForm { get; set; }

        public MakeBetViewModel(long battleId, string startDate, string endDate, int budget, int betLimit, IEnumerable<SelectListItem> teams)
            : this(battleId, startDate, endDate, budget, betLimit, teams, new MakeBetFormViewModel())
        {
        }

        public MakeBetViewModel(long battleId, string startDate, string endDate, int budget, int betLimit, IEnumerable<SelectListItem> teams, MakeBetFormViewModel makeBetViewModel) : base(battleId, startDate, endDate, budget, betLimit)
        {
            BattleId = battleId;
            Teams = teams;
            MakeBetForm = makeBetViewModel;
        }
    }

    public class MakeBetFormViewModel
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public double Coefficient { get; set; }
        public double Bet { get; set; }
        public long TeamId { get; set; }
        public bool IsPrivate { get; set; }
    }
}
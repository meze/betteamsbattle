using System.Collections.Generic;
using System.Web.Mvc;
using BetTeamsBattle.Data.Model.Entities;

namespace BetTeamsBattle.Frontend.Areas.NotAdmin.Models.BattleBets
{
    public class MakeBetViewModel
    {
        public long BattleId { get; set; }
        public IEnumerable<SelectListItem> Teams { get; set; } 
        public MakeBetFormViewModel MakeBetForm { get; set; }

        public MakeBetViewModel(long battleId, IEnumerable<SelectListItem> teams)
            : this(battleId, teams, new MakeBetFormViewModel())
        {
        }

        public MakeBetViewModel(long battleId, IEnumerable<SelectListItem> teams, MakeBetFormViewModel makeBetViewModel)
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
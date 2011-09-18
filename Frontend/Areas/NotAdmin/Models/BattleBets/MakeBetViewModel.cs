using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        public MakeBetViewModel(long battleId, string startDate, string endDate, int budget, int betLimit, IEnumerable<SelectListItem> teams, MakeBetFormViewModel makeBetViewModel)
            : base(battleId, startDate, endDate, budget, betLimit)
        {
            BattleId = battleId;
            Teams = teams;
            MakeBetForm = makeBetViewModel;
        }
    }

    public class MakeBetFormViewModel
    {
        public long TeamId { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        //ToDo: This Amount+AmountString like approach is applied for all numeric types. It solved two problems:
        //1. Allows to override error message if incorrect numeric value is inputed (http://stackoverflow.com/questions/7412101/override-default-asp-net-mvc-message-in-fluentvalidation)
        //2. Allows to format numeric values for strongly-typed helpers
        internal double? Amount
        {
            get
            {
                double amount;
                if (double.TryParse(AmountString, out amount))
                    return amount;
                return null;
            }
        }
        public string AmountString { get; set; }
        internal double? Coefficient
        {
            get
            {
                double coefficient;
                if (double.TryParse(CoefficientString, out coefficient))
                    return coefficient;
                return null;
            }
        }
        public string CoefficientString { get; set; }
        public bool IsPrivate { get; set; }
        public double BetLimit { get; set; }

        public MakeBetFormViewModel()
        {
            AmountString = 0.ToString("N2");
            CoefficientString = 0.ToString("N2");
        }
    }
}
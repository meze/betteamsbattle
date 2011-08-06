using System;
using System.Collections.Generic;
using System.Web.Mvc;
using BetTeamsBattle.Data.Model.Enums;

namespace BetTeamsBattle.Frontend.Areas.Admin.Models
{
    public class CreateBattleViewModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public BattleType BattleType { get; set; }
        public IEnumerable<SelectListItem> BattleTypes
        {
            get
            {
                var fixedBudgetBattleType = BattleType.FixedBudget;
                return new List<SelectListItem>()
                           {
                               new SelectListItem() 
                                   {
                                       Text = fixedBudgetBattleType.ToString(),
                                       Value = ((int) fixedBudgetBattleType).ToString()
                                   }
                           };
            }
        }
        public int Budget { get; set; }

        public CreateBattleViewModel()
        {
            var beginOfToday = DateTime.UtcNow;
            beginOfToday = beginOfToday.Add(-beginOfToday.TimeOfDay);

            StartDate = beginOfToday;
            EndDate = beginOfToday;
        }
    }
}
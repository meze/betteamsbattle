using System;
using System.Collections.Generic;
using System.Web.Mvc;
using BetTeamsBattle.Data.Model.Enums;

namespace BetTeamsBattle.Frontend.Areas.Admin.Models
{
    public class BattleViewModel
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
    }
}
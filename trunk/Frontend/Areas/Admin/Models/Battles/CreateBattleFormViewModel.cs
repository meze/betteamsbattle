using System;
using System.Collections.Generic;
using System.Web.Mvc;
using BetTeamsBattle.Data.Model.Enums;

namespace BetTeamsBattle.Frontend.Areas.Admin.Models.Battles
{
    public class CreateBattleFormViewModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public BattleType BattleType { get; set; }
        
        public int Budget { get; set; }

        public CreateBattleFormViewModel()
        {
            var beginOfToday = DateTime.UtcNow;
            beginOfToday = beginOfToday.Add(-beginOfToday.TimeOfDay);

            StartDate = beginOfToday;
            EndDate = beginOfToday;
        }
    }
}
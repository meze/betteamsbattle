using System;
using System.Collections.Generic;
using System.Web.Mvc;
using BetTeamsBattle.Data.Model.Enums;

namespace BetTeamsBattle.Frontend.Areas.Admin.Models.Battles
{
    public class CreateBattleViewModel
    {
        public IEnumerable<SelectListItem> BattleTypes { get; set; }
        public CreateBattleFormViewModel CreateBattleForm { get; set; }
        
        public CreateBattleViewModel() : this(new CreateBattleFormViewModel())
        {
        }

        public CreateBattleViewModel(CreateBattleFormViewModel createBattleFormViewModel)
        {
            const BattleType fixedBudgetBattleType = BattleType.FixedBudget;
            BattleTypes = new List<SelectListItem>()
                              {
                                  new SelectListItem()
                                      {
                                          Text = fixedBudgetBattleType.ToString(),
                                          Value = ((int) fixedBudgetBattleType).ToString()
                                      }
                              };

            CreateBattleForm = createBattleFormViewModel;
        }
    }
}
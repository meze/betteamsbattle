using System;
using System.Collections.Generic;

namespace BetTeamsBattle.Frontend.Areas.NotAdmin.Models.Battle
{
    public class AllBattlesViewModel
    {
        public IEnumerable<CurrentBattleViewModel> CurrentBattlesViewModels { get; set; }
        public IEnumerable<NotStartedBattleViewModel> NotStartedBattlesViewModels { get; set; }
        public IEnumerable<FinishedBattleViewModel> FinishedBattlesViewModels { get; set; }

        public AllBattlesViewModel()
        {
            CurrentBattlesViewModels = new List<CurrentBattleViewModel>();
            NotStartedBattlesViewModels = new List<NotStartedBattleViewModel>();
            FinishedBattlesViewModels = new List<FinishedBattleViewModel>();
        }
    }

    public class CurrentBattleViewModel
    {
        public int Budget { get; set; }
        public int BetLimit { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }

        public CurrentBattleViewModel(int budget, int betLimit, string startDate, string endDate)
        {
            Budget = budget;
            BetLimit = betLimit;
            StartDate = startDate;
            EndDate = endDate;
        }
    }

    public class NotStartedBattleViewModel
    {
        public int Budget { get; set; }
        public int BetLimit { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string InDaysString { get; set; }

        public NotStartedBattleViewModel(int budget, int betLimit, string startDate, string endDate, string inDaysString)
        {
            Budget = budget;
            BetLimit = betLimit;
            StartDate = startDate;
            EndDate = endDate;
            InDaysString = inDaysString;
        }
    }

    public class FinishedBattleViewModel
    {
        public int Budget { get; set; }
        public int BetLimit { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }

        public FinishedBattleViewModel(int budget, int betLimit, string startDate, string endDate)
        {
            Budget = budget;
            BetLimit = betLimit;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
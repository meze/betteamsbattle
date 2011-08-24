using System.Collections.Generic;

namespace BetTeamsBattle.Frontend.Areas.NotAdmin.Models.Battles
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
        public long BattleId { get; set; }
        public int Budget { get; set; }
        public int BetLimit { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }

    public class NotStartedBattleViewModel
    {
        public long BattleId { get; set; }
        public int Budget { get; set; }
        public int BetLimit { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string InDaysString { get; set; }
    }

    public class FinishedBattleViewModel
    {
        public long BattleId { get; set; }
        public int Budget { get; set; }
        public int BetLimit { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}
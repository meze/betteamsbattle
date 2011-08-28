using System;

namespace BetTeamsBattle.Frontend.Models.Shared
{
    public class ColumnBlockViewModel
    {
        public string Title { get; set; }
        public Func<dynamic, object> Content { get; set; }

        public ColumnBlockViewModel(string title, Func<dynamic, object> content)
        {
            Title = title;
            Content = content;
        }
    }
}
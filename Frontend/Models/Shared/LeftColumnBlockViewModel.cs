using System;

namespace BetTeamsBattle.Frontend.Models.Shared
{
    public class LeftColumnBlockViewModel
    {
        public string Title { get; set; }
        public Func<dynamic, object> Content { get; set; }

        public LeftColumnBlockViewModel(string title, Func<dynamic, object> content)
        {
            Title = title;
            Content = content;
        }
    }
}
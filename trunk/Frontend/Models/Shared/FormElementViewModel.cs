using System.Web.Mvc;

namespace BetTeamsBattle.Frontend.Models
{
    public class FormElementViewModel
    {
        public string Title { get; set; }
        public MvcHtmlString Element { get; set; }
        public MvcHtmlString ValidationMessage { get; set; }

        public FormElementViewModel(string title, MvcHtmlString element, MvcHtmlString validationMessage)
        {
            Title = title;
            Element = element;
            ValidationMessage = validationMessage;
        }
    }
}
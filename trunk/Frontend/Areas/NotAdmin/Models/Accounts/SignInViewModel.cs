namespace BetTeamsBattle.Frontend.Areas.NotAdmin.Models.Accounts
{
    public class SignInViewModel
    {
        public string Message { get; set; }

        public SignInViewModel(string message)
        {
            Message = message;
        }
    }
}
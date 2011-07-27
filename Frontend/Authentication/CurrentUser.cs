#region

using System.Web;
using BetTeamsBattle.Data.Model.Entities;

#endregion

namespace BetTeamsBattle.Frontend.Authentication
{
    public class CurrentUser
    {
        public static User User
        {
            get
            {
                return (User)HttpContext.Current.Items[FrontendConstants.UserKey];
            }
        }

        public static long UserId
        {
            get
            {
                return User.Id;
            }
        }

        public static long? NullableUserId
        {
            get
            {
                var user = User;
                return user == null ? (long?)null : user.Id;
            }
        }
    }
}
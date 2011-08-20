using System.Collections.Generic;
using BetTeamsBattle.Frontend.Areas.NotAdmin.Models.Users;

namespace BetTeamsBattle.Frontend.Areas.NotAdmin.ViewServices.Battles.Interfaces
{
    public interface IUsersViewService
    {
        IEnumerable<TopUsersUserViewModel> TopUsers();
    }
}
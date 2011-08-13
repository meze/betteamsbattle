namespace BetTeamsBattle.Data.Services.Interfaces
{
    public interface IBattleUsersService
    {
        bool UserIsJoinedToBattle(long userId, long battleId);
    }
}
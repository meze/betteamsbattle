using System.Data.Objects;
using BetTeamsBattle.Data.Model.Entities;

namespace BetTeamsBattle.Data.Model
{
    public class ModelContext : ObjectContext
    {
        public ModelContext() : base("name=ModelContainer")
        {
        }

        private ObjectSet<User> _users; 
        public ObjectSet<User> Users
        {
            get
            {
                if (_users == null)
                    _users = CreateObjectSet<User>();
                return _users;
            }
        }

        private ObjectSet<Battle> _battles;
        public ObjectSet<Battle> Battles
        {
            get
            {
                if (_battles == null)
                    _battles = CreateObjectSet<Battle>();
                return _battles;
            }
        }

        private ObjectSet<BattleUser> _battlesUsers;
        public ObjectSet<BattleUser> BattlesUsers
        {
            get
            {
                if (_battlesUsers == null)
                    _battlesUsers = CreateObjectSet<BattleUser>();
                return _battlesUsers;
            }
        }
    }
}
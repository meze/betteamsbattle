using System;
using System.Collections.Generic;

namespace BetTeamsBattle.Data.Model.Entities
{
    public class User
    {
        public virtual long Id { get; set; }
        public virtual string Login { get; set; }
        public virtual DateTime RegistrationDate { get; set; }
        public virtual DateTime LastSeen { get; set; }

        public virtual ICollection<Battle> Battles { get; set; }
    }
}
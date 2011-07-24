using System;
using System.Collections.Generic;

namespace BetTeamsBattle.Data.Model.Entities
{
    public class Battle
    {
        public virtual long Id { get; set; }
        public virtual DateTime StartDate { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
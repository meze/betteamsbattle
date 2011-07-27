using System;
using System.Collections.Generic;
using BetTeamsBattle.Data.Model.Interfaces;

namespace BetTeamsBattle.Data.Model.Entities
{
    public class Battle : IEntity
    {
        public virtual long Id { get; set; }
        public virtual DateTime StartDate { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
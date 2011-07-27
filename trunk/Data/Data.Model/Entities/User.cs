﻿using System;
using System.Collections.Generic;
using BetTeamsBattle.Data.Model.Interfaces;

namespace BetTeamsBattle.Data.Model.Entities
{
    public class User : IEntity
    {
        public virtual long Id { get; set; }
        public virtual string Login { get; set; }
        public virtual DateTime RegistrationDate { get; set; }
        public virtual DateTime LastSeen { get; set; }

        public virtual UserProfile Profile { get; set; } 
        public virtual ICollection<Battle> Battles { get; set; }
    }
}
using System;
using System.Collections.Generic;
using BetTeamsBattle.Data.Model.Enums;
using BetTeamsBattle.Data.Model.Interfaces;

namespace BetTeamsBattle.Data.Model.Entities
{
    public class Team : IEntity
    {
        public virtual long Id { get; set; }
        public virtual string Title { get; set; }
        public virtual string Site { get; set; }
        public virtual string Description { get; set; }
        public virtual bool IsPersonal { get; set; }
        public virtual bool IsPro { get; set; }

        private ICollection<TeamUser> _teamUsers;
        public virtual ICollection<TeamUser> TeamUsers
        {
            get
            {
                if (_teamUsers == null)
                    _teamUsers = new List<TeamUser>();
                return _teamUsers;
            }
            set { _teamUsers = value; }
            }
        public virtual ICollection<Bet> Bets { get; set; }
        public virtual TeamStatistics TeamStatistics { get; set; } 
        public virtual ICollection<TeamBattleStatistics> TeamBattlesStatistics { get; set; }

        internal Team()
        {
        }

        public static Team CreatePersonalTeam(string title, User user)
        {
            var personalTeam = new Team() { Title = title, IsPersonal = true, IsPro = false };

            personalTeam.TeamStatistics = new TeamStatistics();

            var teamUser = new TeamUser(user, TeamUserAction.Join);
            personalTeam.TeamUsers.Add(teamUser);

            return personalTeam;
        }

        public static Team CreateProTeam(string title, string description, string site, IEnumerable<long> usersIds)
        {
            var proTeam = new Team() { Title = title, Description = description, Site = site, IsPersonal = false, IsPro = true };
            proTeam.TeamStatistics = new TeamStatistics();

            foreach (var userId in usersIds)
                proTeam.TeamUsers.Add(new TeamUser(userId, TeamUserAction.Join));

            return proTeam;
        }
    }
}
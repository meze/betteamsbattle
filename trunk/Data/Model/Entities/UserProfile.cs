using BetTeamsBattle.Data.Model.Enums;

namespace BetTeamsBattle.Data.Model.Entities
{
    public class UserProfile
    {
        public virtual long Id { get; set; }
        public virtual Language? LanguageEnum
        {
            get { return (Language?)Language; }
            set { Language = (sbyte?)value; }
        }
        public virtual sbyte? Language { get; set; }

        public virtual User User { get; set; }
    }
}
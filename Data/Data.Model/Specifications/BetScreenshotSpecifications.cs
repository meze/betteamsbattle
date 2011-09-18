using BetTeamsBattle.Data.Model.Entities;
using BetTeamsBattle.Data.Model.Enums;

namespace BetTeamsBattle.Data.Model.Specifications
{
    public class BetScreenshotSpecifications
    {
        public static LinqSpec<BetScreenshot> NotProcessed()
        {
            return LinqSpec.For<BetScreenshot>(bs => bs.Status == (sbyte)BetScreenshotStatus.NotProcessed);
        }
    }
}
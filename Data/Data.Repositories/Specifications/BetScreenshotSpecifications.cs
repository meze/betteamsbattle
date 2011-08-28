using System.Collections.Generic;
using System.Linq;
using BetTeamsBattle.Data.Model.Entities;
using BetTeamsBattle.Data.Model.Enums;

namespace BetTeamsBattle.Data.Repositories.Specifications
{
    public class BetScreenshotSpecifications
    {
        public static LinqSpec<BetScreenshot> NotProcessed()
        {
            return LinqSpec.For<BetScreenshot>(bs => bs.Status == (sbyte)BetScreenshotStatus.NotProcessed);
        }
    }
}
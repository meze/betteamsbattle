using System.Collections.Generic;
using System.Linq;
using BetTeamsBattle.Data.Model.Entities;

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
using System.Linq;
using System.Collections.Generic;
using BetTeamsBattle.Data.Model.Entities;

namespace BetTeamsBattle.Data.Repositories.Specifications
{
    public class QueuedBetUrlSpecifications
    {
        public static LinqSpec<QueuedBetUrl> NotProcessed()
        {
            return LinqSpec.For<QueuedBetUrl>(qbu => qbu.FinishDateTime == null);
        }

        public static LinqSpec<QueuedBetUrl> IdIsNotContainedIn(IEnumerable<long> queuedBetUrlsIds)
        {
            return LinqSpec.For<QueuedBetUrl>(qbu => !queuedBetUrlsIds.Contains(qbu.Id));
        }
    }
}
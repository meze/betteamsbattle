using System.Linq;
using System.Collections.Generic;

namespace BetTeamsBattle.Data.Repositories.Specific.QueuedBetUrl
{
    public class QueuedBetUrlSpecifications
    {
        public static LinqSpec<Model.Entities.QueuedBetUrl> NotProcessed()
        {
            return LinqSpec.For<Model.Entities.QueuedBetUrl>(qbu => qbu.FinishDateTime == null);
        }

        public static LinqSpec<Model.Entities.QueuedBetUrl> IdIsNotContainedIn(IEnumerable<long> queuedBetUrlsIds)
        {
            return LinqSpec.For<Model.Entities.QueuedBetUrl>(qbu => !queuedBetUrlsIds.Contains(qbu.Id));
        }
    }
}
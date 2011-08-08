using BetTeamsBattle.Data.Model;
using BetTeamsBattle.Data.Repositories.UnitOfWork;
using Ninject;

namespace BetTeamsBattle.Data.Repositories.Base
{
    internal class ContextManagerBase
    {
        private ModelContext _context;
        [Inject]
        public ModelContext Context
        {
            get
            {
                var currentContext = UnitOfWorkScope.CurrentContext;
                if (currentContext != null)
                    return currentContext;
                return _context;
            }
            set { _context = value; }
        }
    }
}
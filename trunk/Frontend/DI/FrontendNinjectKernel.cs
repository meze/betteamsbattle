using BetTeamsBattle.Data.Repositories.DI;
using BetTeamsBattle.Data.Repositories.Infrastructure.DI;
using BetTeamsBattle.Data.Services.DI;
using Ninject;

namespace BetTeamsBattle.Frontend.DI
{
    internal class FrontendNinjectKernel
    {
        public static IKernel CreateKernel()
        {
            return new StandardKernel(new DataRepositoriesNinjectModule(),
                                      new DataServicesNinjectModule(),
                                      new DataRepositoriesInfrastructureNinjectModule(),
                                      new FrontendNinjectModule());
        }
    }
}
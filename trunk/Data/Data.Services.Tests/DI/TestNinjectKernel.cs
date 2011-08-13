using BetTeamsBattle.Data.Model;
using BetTeamsBattle.Data.Repositories.DI;
using BetTeamsBattle.Data.Repositories.Infrastructure.DI;
using BetTeamsBattle.Data.Services.DI;
using Ninject;

namespace BetTeamsBattle.Data.Services.Tests.DI
{
    internal class TestNinjectKernel
    {
        public static IKernel Kernel
        {
            get
            {
                var kernel = new StandardKernel(new DataRepositoriesNinjectModule(), new DataServicesNinjectModule(), new DataRepositoriesInfrastructureNinjectModule());
                kernel.Bind<ModelContext>().To<ModelContext>().InSingletonScope();
                return kernel;
            }
        }
    }
}
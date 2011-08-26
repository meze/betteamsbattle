using BetTeamsBattle.Data.Repositories.DI;
using BetTeamsBattle.Data.Repositories.Infrastructure.DI;
using Ninject;

namespace BetTeamsBattle.ScreenShotsMaker.DI
{
    internal class ScreenshotsMakerNinjectKernel
    {
        public static IKernel CreateKernel()
        {
            return new StandardKernel(
                new DataRepositoriesInfrastructureNinjectModule(),
                new DataRepositoriesNinjectModule(), 
                new ScreenshotsMakerNinjectModule());
        }
    }
}
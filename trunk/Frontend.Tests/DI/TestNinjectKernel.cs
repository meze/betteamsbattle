using BetTeamsBattle.Data.Model;
using BetTeamsBattle.Data.Repositories.DI;
using BetTeamsBattle.Data.Services.DI;
using BetTeamsBattle.Frontend.DI;
using BetTeamsBattle.Screenshots.AmazonS3.DI;
using Ninject;

namespace BetTeamsBattle.Frontend.Tests.DI
{
    internal class TestNinjectKernel
    {
        public static IKernel Kernel
        {
            get
            {
                var kernel = new StandardKernel(new DataRepositoriesNinjectModule(), new DataServicesNinjectModule(), new FrontendNinjectModule(), new ScreenshotsAmazonS3NinjectModule());
                kernel.Bind<ModelContext>().To<ModelContext>().InSingletonScope();
                return kernel;
            }
        }
    }
}
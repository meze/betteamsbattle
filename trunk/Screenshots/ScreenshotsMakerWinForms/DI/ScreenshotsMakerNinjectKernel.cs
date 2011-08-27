using BetTeamsBattle.AwesomiumScreenshotMaker.DI;
using BetTeamsBattle.BettScreenshotsManager.DI;
using BetTeamsBattle.Data.Repositories.DI;
using BetTeamsBattle.Data.Repositories.Infrastructure.DI;
using Ninject;

namespace BetTeamsBattle.ScreenshotsMakerWinForms.DI
{
    internal class ScreenshotsMakerNinjectKernel
    {
        public static IKernel CreateKernel()
        {
            return new StandardKernel(
                new DataRepositoriesInfrastructureNinjectModule(),
                new DataRepositoriesNinjectModule(), 
                new AwesomiumScreenshotMakerNinjectModule(),
                new AmazonS3NinjectModule(),
                new ScreenshotsMakerNinjectModule());
        }
    }
}
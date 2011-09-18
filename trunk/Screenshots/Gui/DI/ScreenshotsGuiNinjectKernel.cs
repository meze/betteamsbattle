using BetTeamsBattle.Data.Repositories.DI;
using BetTeamsBattle.Screenshots.AmazonS3.DI;
using BetTeamsBattle.Screenshots.AwesomiumScreenshotMaker.DI;
using BetTeamsBattle.Screenshots.BettScreenshotsManager.DI;
using BetTeamsBattle.Screenshots.Common.DI;
using Ninject;

namespace BetTeamsBattle.Screenshots.Gui.DI
{
    public class ScreenshotsGuiNinjectKernel
    {
        public static IKernel CreateKernel()
        {
            return new StandardKernel(
                new DataRepositoriesNinjectModule(), 
                new ScreenshotsCommonNinjectModule(),
                new ScreenshotsAwesomiumScreenshotMakerNinjectModule(),
                new ScreenshotsAmazonS3NinjectModule(),
                new ScreenshotsBetScreenshotsManagerNinjectModule());
        }
    }
}
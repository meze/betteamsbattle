﻿using BetTeamsBattle.Data.Repositories.DI;
using BetTeamsBattle.Data.Services.DI;
using BetTeamsBattle.Screenshots.AmazonS3.DI;
using Ninject;

namespace BetTeamsBattle.Frontend.DI
{
    internal class FrontendNinjectKernel
    {
        public static IKernel CreateKernel()
        {
            return new StandardKernel(new DataRepositoriesNinjectModule(),
                                      new DataServicesNinjectModule(),
                                      new ScreenshotsAmazonS3NinjectModule(),
                                      new FrontendNinjectModule());
        }
    }
}
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using BetTeamsBattle.Data.Model.DI;
using BetTeamsBattle.Data.Model.Entities;
using BetTeamsBattle.Data.Repositories.Base;
using BetTeamsBattle.Data.Repositories.DI;
using BetTeamsBattle.Data.Repositories.Specifications;
using BetTeamsBattle.Frontend.AspNetMvc;
using BetTeamsBattle.Frontend.AspNetMvc.ActionFilters;
using BetTeamsBattle.Frontend.AspNetMvc.Routes;
using BetTeamsBattle.Frontend.DI;
using BetTeamsBattle.Frontend.Helpers;
using BetTeamsBattle.Frontend.Localization.Infrastructure.LanguageService.Interfaces;
using Ninject;
using Ninject.Web.Mvc;

namespace BetTeamsBattle.Frontend
{
    public class MvcApplication : NinjectHttpApplication
    {
        public void RegisterRoutes(RouteCollection routes)
        {
            var languageConstraint = new RegexRouteConstraint("^(en|ru)$");
            var startsNotFromLanguageConstraint = new RegexRouteConstraint("^(?!((en|ru)($|/)))");

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("",
                            "{language}/{controller}/{action}",
                            (object)null,
                            new { language = languageConstraint });

            #region Language-inspecific queries
            //These routes are catched by LanguageGlobalActionFilter and redirected to page with specific language
            //Really the second route should work for empty string (when user comes to root - thingface.com) but it doesn't - 
            //I have created a special route for this case
            //None of these routes will really come to Home.Index action
            routes.MapRoute("Root",
                            "",
                            MVC.Home.Index());

            routes.MapRoute("",
                            "{*something}",
                            MVC.Home.Index(),
                            null,
                            new { something = startsNotFromLanguageConstraint }); //doesn't start from country code

            #endregion Language-inspecific queries
        }

        protected override void OnApplicationStarted()
        {
            base.OnApplicationStarted();

            AreaRegistration.RegisterAllAreas();

            RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_BeginRequest()
        {
            DomainHelper.RedirectToNonWww();
        }

        protected void Application_AuthenticateRequest()
        {
            if (User != null && User.Identity != null)
            {
                var login = User.Identity.Name;
                var repositoryOfUser = Kernel.Get<IRepository<User>>();
                var user = repositoryOfUser.Filter(UserSpecifications.LoginIsEqual(login)).Include(u => u.Profile).Single();
                Context.Items[FrontendConstants.UserKey] = user;
            }
        }

        protected override IKernel CreateKernel()
        {
            return new StandardKernel(new DataModelNinjectModule(), new DataRepositoriesNinjectModule(), new FrontendNinjectModule());
        }
    }
}
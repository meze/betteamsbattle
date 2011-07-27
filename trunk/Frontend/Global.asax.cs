using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
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
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : NinjectHttpApplication
    {
        public void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new AdminRightsGlobalFilter());
            filters.Add(new LanguageGlobalActionFilter(Kernel.Get<ILanguageService>()));
            filters.Add(new HandleErrorAttribute());
        }

        public void RegisterRoutes(RouteCollection routes)
        {
            var languageConstraint = new RegexRouteConstraint("^(en|ru)$");
            var startsNotFromLanguageConstraint = new RegexRouteConstraint("^(?!((en|ru)($|/)))");

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}" );

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

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_BeginRequest()
        {
            DomainHelper.RedirectToNonWww();
        }

        protected void Application_AuthenticateRequest()
        {
            string login = null;
            if (User != null && User.Identity != null)
                login = User.Identity.Name;
            User user;
            if (login != null)
            {
                var repositoryOfUser = Kernel.Get<IRepository<User>>();
                user = repositoryOfUser.FindAll(UserSpecifications.LoginIsEqual(login)).Include(u => u.Profile).Single();
                Context.Items[FrontendConstants.UserKey] = user;
            }
        }

        protected override IKernel CreateKernel()
        {
            return new StandardKernel(new DataRepositoriesNinjectModule(), new FrontendNinjectModule());
        }
    }
}
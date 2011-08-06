using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using BetTeamsBattle.Data.Model;
using BetTeamsBattle.Data.Model.DI;
using BetTeamsBattle.Data.Model.Entities;
using BetTeamsBattle.Data.Repositories.Base;
using BetTeamsBattle.Data.Repositories.DI;
using BetTeamsBattle.Data.Repositories.Specifications;
using BetTeamsBattle.Data.Services.DI;
using BetTeamsBattle.Frontend.AspNetMvc;
using BetTeamsBattle.Frontend.AspNetMvc.ActionFilters;
using BetTeamsBattle.Frontend.AspNetMvc.Routes;
using BetTeamsBattle.Frontend.Authentication;
using BetTeamsBattle.Frontend.DI;
using BetTeamsBattle.Frontend.Helpers;
using BetTeamsBattle.Frontend.Localization.Infrastructure.LanguageService.Interfaces;
using FluentValidation.Mvc;
using Ninject;
using Ninject.Web.Mvc;
using Ninject.Web.Mvc.FluentValidation;

namespace BetTeamsBattle.Frontend
{
    public class MvcApplication : NinjectHttpApplication
    {
        public void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("Content/{*pathInfo}");
            routes.IgnoreRoute("Scripts/{*pathInfo}");

            #region Language-inspecific queries
            //This route is catched by LanguageActionFilter and redirected to page with specific language
            //None of these routes will really come to Home.Index action

            routes.MapRoute("{*something}", MVC.NotAdmin.Home.Index(), new { something = RegexRouteConstraints.StartsNotFromLanguageConstraint }); //doesn't start from language

            #endregion Language-inspecific queries
        }

        protected override void OnApplicationStarted()
        {
            base.OnApplicationStarted();

            AreaRegistration.RegisterAllAreas();
            RegisterRoutes(RouteTable.Routes);

            ViewEngines.Engines.RemoveAt(0); //Remove WebForms ViewEngine

            var validatorFactory = new NinjectValidatorFactory(Kernel);
            FluentValidationModelValidatorProvider.Configure(x =>
                {
                    x.ValidatorFactory = validatorFactory;
                    x.AddImplicitRequiredValidator = false;
                });
        }

        protected void Application_BeginRequest()
        {
            DomainHelper.RedirectToNonWww();
        }

        protected void Application_EndRequest()
        {
            var modelContext = Kernel.Get<ModelContext>();
            modelContext.SaveChanges();
        }

        protected void Application_AuthenticateRequest()
        {
            if (User != null && User.Identity != null)
            {
                var login = User.Identity.Name;
                var repositoryOfUser = Kernel.Get<IRepository<User>>();
                var user = repositoryOfUser.Get(UserSpecifications.LoginIsEqual(login)).Include(u => u.UserProfile).Single();
                Context.Items[FrontendConstants.UserKey] = user;
            }

            var languageService = Kernel.Get<ILanguageService>(); 
            languageService.ProccessLanguageForRequest(CurrentUser.User, Request, Response);
        }

        protected override IKernel CreateKernel()
        {
            return new StandardKernel(new DataModelNinjectModule(), new DataRepositoriesNinjectModule(),
                                      new DataServicesNinjectModule(), new FrontendNinjectModule());
        }
    }
}
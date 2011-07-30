#region

using System;
using System.Globalization;
using System.Threading;
using System.Web;
using BetTeamsBattle.Data.Model.Entities;
using BetTeamsBattle.Data.Model.Enums;
using BetTeamsBattle.Data.Repositories.Base;
using BetTeamsBattle.Frontend.Localization.Infrastructure.Cookies.Interfaces;
using BetTeamsBattle.Frontend.Localization.Infrastructure.IP.Interfaces;
using BetTeamsBattle.Frontend.Localization.Infrastructure.LanguageMapping;
using BetTeamsBattle.Frontend.Localization.Infrastructure.LanguageService.Interfaces;
using BetTeamsBattle.Frontend.Localization.Infrastructure.Url;

#endregion

namespace BetTeamsBattle.Frontend.Localization.Infrastructure.LanguageService
{
    public class LanguageService : ILanguageService
    {
        private readonly IIpToLanguageConverter _ipToLanguageConverter;
        private readonly IRepository<UserProfile> _repositoryOfUserProfile;
        private readonly ICookiesService _cookiesService;

        public LanguageService(IIpToLanguageConverter ipToLanguageConverter,
            IRepository<UserProfile> repositoryOfUserProfile,
            ICookiesService cookiesService)
        {
            _ipToLanguageConverter = ipToLanguageConverter;
            _repositoryOfUserProfile = repositoryOfUserProfile;
            _cookiesService = cookiesService;
        }

        public Language ProccessLanguageForRequest(User user, HttpRequest request, HttpResponse response)
        {
            Language language;

            string pathCountryCode;
            UrlCountryCodeHelper.GetPathCountryCodeParts(request.Url.GetComponents(UriComponents.Path, UriFormat.Unescaped), out pathCountryCode);
            
            if (LanguageMappingHelper.CountryCodeIsSupported(pathCountryCode))
            {
                language = LanguageMappingHelper.GetLanguageByCountryCode(pathCountryCode);
                var userLanguage = GetLanguage(user, request);
                if (!userLanguage.HasValue || userLanguage.Value != language)
                    ChangeLanguage(user, language, response);
            }
            else
            {
                if (user != null)
                {
                    var userLanguage = GetAuthenticatedUserLanguage(user);
                    if (userLanguage.HasValue)
                        language = userLanguage.Value;
                    else
                    {
                        language = _ipToLanguageConverter.GetLanguage(request.UserHostAddress);

                        ChangeAuthenticatedUserLanguage(user, language);
                    }
                }
                else
                {
                    var cookieLanguage = GetNonAuthenticatedUserLanguage(request);
                    if (cookieLanguage.HasValue)
                        language = cookieLanguage.Value;
                    else
                    {
                        language = _ipToLanguageConverter.GetLanguage(request.UserHostAddress);

                        ChangeNonAuthenticatedUserLanguage(language, response);
                    }
                }
            }

            var correctPathCountryCode = LanguageMappingHelper.GetCountryCodeByLanguage(language);
            if (pathCountryCode != correctPathCountryCode)
            {
                var newUrl = UrlCountryCodeHelper.ChangeUrlCountryCodePart(request.Url, language);
                response.Redirect(newUrl);
            }

            return language;
        }

        private Language? GetLanguage(User user, HttpRequest request)
        {
            if (user != null)
                return GetAuthenticatedUserLanguage(user);
            else
                return GetNonAuthenticatedUserLanguage(request);
        }

        private Language? GetAuthenticatedUserLanguage(User user)
        {
            return user.Profile.LanguageEnum;
        }

        private Language? GetNonAuthenticatedUserLanguage(HttpRequest request)
        {
            var languageCookie = request.Cookies.Get(FrontendConstants.LanguageCookieName);
            if (languageCookie == null)
                return null;
            try
            {
                return (Language)Convert.ToInt64(languageCookie.Value);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private void ChangeLanguage(User user, Language newLanguage, HttpResponse response)
        {
            if (user != null)
                ChangeAuthenticatedUserLanguage(user, newLanguage);
            else
                ChangeNonAuthenticatedUserLanguage(newLanguage, response);
        }

        private void ChangeAuthenticatedUserLanguage(User user, Language newLanguage)
        {
            user.Profile.LanguageEnum = newLanguage;
        }

        private void ChangeNonAuthenticatedUserLanguage(Language newLanguage, HttpResponse response)
        {
            var languageCookie = _cookiesService.CreateCrossSiteCookie(
                    FrontendConstants.LanguageCookieName,
                    ((long)newLanguage).ToString(),
                    DateTime.Now.AddYears(1));
            response.Cookies.Add(languageCookie);
        }
    }
}
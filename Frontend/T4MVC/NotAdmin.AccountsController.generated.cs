// <auto-generated />
// This file was generated by a T4 template.
// Don't change it directly as your change would get overwritten.  Instead, make changes
// to the .tt file (i.e. the T4 template) and save it to regenerate this file.

// Make sure the compiler doesn't complain about missing Xml comments
#pragma warning disable 1591
#region T4MVC

using System;
using System.Diagnostics;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using System.Web.Routing;
using T4MVC;
namespace BetTeamsBattle.Frontend.Areas.NotAdmin.Controllers {
    public partial class AccountsController {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected AccountsController(Dummy d) { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(ActionResult result) {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoute(callInfo.RouteValueDictionary);
        }

        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult SignIn() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.SignIn);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult Authenticate() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.Authenticate);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult SignUp() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.SignUp);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public AccountsController Actions { get { return MVC.NotAdmin.Accounts; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "notadmin";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "accounts";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass {
            public readonly string SignIn = ("SignIn").ToLowerInvariant();
            public readonly string Authenticate = ("Authenticate").ToLowerInvariant();
            public readonly string SignOut = ("SignOut").ToLowerInvariant();
            public readonly string SignUp = ("SignUp").ToLowerInvariant();
        }


        static readonly ViewNames s_views = new ViewNames();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ViewNames Views { get { return s_views; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ViewNames {
            public readonly string AuthenticationLink = "~/Areas/NotAdmin/Views/Accounts/AuthenticationLink.cshtml";
            public readonly string SignIn = "~/Areas/NotAdmin/Views/Accounts/SignIn.cshtml";
            public readonly string SignUp = "~/Areas/NotAdmin/Views/Accounts/SignUp.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public class T4MVC_AccountsController: BetTeamsBattle.Frontend.Areas.NotAdmin.Controllers.AccountsController {
        public T4MVC_AccountsController() : base(Dummy.Instance) { }

        public override System.Web.Mvc.ActionResult SignIn(string message) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.SignIn);
            callInfo.RouteValueDictionary.Add("message", message);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult Authenticate(string openIdIdentifier) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.Authenticate);
            callInfo.RouteValueDictionary.Add("openIdIdentifier", openIdIdentifier);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult SignOut() {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.SignOut);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult SignUp(string openIdUrl) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.SignUp);
            callInfo.RouteValueDictionary.Add("openIdUrl", openIdUrl);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult SignUp(BetTeamsBattle.Frontend.Areas.NotAdmin.Models.Accounts.SignUpViewModel userViewModel) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.SignUp);
            callInfo.RouteValueDictionary.Add("userViewModel", userViewModel);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591

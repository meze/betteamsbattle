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
    public partial class BattleBetsController {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected BattleBetsController(Dummy d) { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(ActionResult result) {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoute(callInfo.RouteValueDictionary);
        }

        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult GetMyBattleBets() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.GetMyBattleBets);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult GetUserBets() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.GetUserBets);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult GetTeamBets() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.GetTeamBets);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult MakeBet() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.MakeBet);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult BetSucceeded() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.BetSucceeded);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult BetFailed() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.BetFailed);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult BetCanceledByBookmaker() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.BetCanceledByBookmaker);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public BattleBetsController Actions { get { return MVC.NotAdmin.BattleBets; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "notadmin";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "battlebets";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass {
            public readonly string GetMyBattleBets = ("GetMyBattleBets").ToLowerInvariant();
            public readonly string GetUserBets = ("GetUserBets").ToLowerInvariant();
            public readonly string GetTeamBets = ("GetTeamBets").ToLowerInvariant();
            public readonly string MakeBet = ("MakeBet").ToLowerInvariant();
            public readonly string BetSucceeded = ("BetSucceeded").ToLowerInvariant();
            public readonly string BetFailed = ("BetFailed").ToLowerInvariant();
            public readonly string BetCanceledByBookmaker = ("BetCanceledByBookmaker").ToLowerInvariant();
        }


        static readonly ViewNames s_views = new ViewNames();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ViewNames Views { get { return s_views; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ViewNames {
            public readonly string _DateAndScreenshot = "~/Areas/NotAdmin/Views/BattleBets/_DateAndScreenshot.cshtml";
            public readonly string _Outcome = "~/Areas/NotAdmin/Views/BattleBets/_Outcome.cshtml";
            public readonly string _OutcomeActionImage = "~/Areas/NotAdmin/Views/BattleBets/_OutcomeActionImage.cshtml";
            public readonly string Bets = "~/Areas/NotAdmin/Views/BattleBets/Bets.cshtml";
            public readonly string MakeBet = "~/Areas/NotAdmin/Views/BattleBets/MakeBet.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public class T4MVC_BattleBetsController: BetTeamsBattle.Frontend.Areas.NotAdmin.Controllers.BattleBetsController {
        public T4MVC_BattleBetsController() : base(Dummy.Instance) { }

        public override System.Web.Mvc.ActionResult GetMyBattleBets(long battleId) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.GetMyBattleBets);
            callInfo.RouteValueDictionary.Add("battleId", battleId);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult GetUserBets(long userId) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.GetUserBets);
            callInfo.RouteValueDictionary.Add("userId", userId);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult GetTeamBets(long teamId) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.GetTeamBets);
            callInfo.RouteValueDictionary.Add("teamId", teamId);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult MakeBet(long battleId) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.MakeBet);
            callInfo.RouteValueDictionary.Add("battleId", battleId);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult MakeBet(long battleId, BetTeamsBattle.Frontend.Areas.NotAdmin.Models.BattleBets.MakeBetFormViewModel makeBetForm) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.MakeBet);
            callInfo.RouteValueDictionary.Add("battleId", battleId);
            callInfo.RouteValueDictionary.Add("makeBetForm", makeBetForm);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult BetSucceeded(long battleBetId) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.BetSucceeded);
            callInfo.RouteValueDictionary.Add("battleBetId", battleBetId);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult BetFailed(long battleBetId) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.BetFailed);
            callInfo.RouteValueDictionary.Add("battleBetId", battleBetId);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult BetCanceledByBookmaker(long battleBetId) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.BetCanceledByBookmaker);
            callInfo.RouteValueDictionary.Add("battleBetId", battleBetId);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591

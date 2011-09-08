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
    public partial class BattlesController {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected BattlesController(Dummy d) { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(ActionResult result) {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoute(callInfo.RouteValueDictionary);
        }

        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult GetBattle() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.GetBattle);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public BattlesController Actions { get { return MVC.NotAdmin.Battles; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "notadmin";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "battles";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass {
            public readonly string AllBattles = ("AllBattles").ToLowerInvariant();
            public readonly string GetBattle = ("GetBattle").ToLowerInvariant();
        }


        static readonly ViewNames s_views = new ViewNames();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ViewNames Views { get { return s_views; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ViewNames {
            public readonly string _MyBattleStatistics = "~/Areas/NotAdmin/Views/Battles/_MyBattleStatistics.cshtml";
            public readonly string AllBattles = "~/Areas/NotAdmin/Views/Battles/AllBattles.cshtml";
            public readonly string GetBattle = "~/Areas/NotAdmin/Views/Battles/GetBattle.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public class T4MVC_BattlesController: BetTeamsBattle.Frontend.Areas.NotAdmin.Controllers.BattlesController {
        public T4MVC_BattlesController() : base(Dummy.Instance) { }

        public override System.Web.Mvc.PartialViewResult AllBattles() {
            var callInfo = new T4MVC_PartialViewResult(Area, Name, ActionNames.AllBattles);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult GetBattle(long battleId) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.GetBattle);
            callInfo.RouteValueDictionary.Add("battleId", battleId);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591

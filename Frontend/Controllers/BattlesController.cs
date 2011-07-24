using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BetTeamsBattle.Data.Repositories.Battles.Interfaces;

namespace BetTeamsBattle.Frontend.Controllers
{
    public partial class BattlesController : Controller
    {
        private readonly IBattlesRepository _battlesRepository;

        public BattlesController(IBattlesRepository battlesRepository)
        {
            _battlesRepository = battlesRepository;
        }
    }
}

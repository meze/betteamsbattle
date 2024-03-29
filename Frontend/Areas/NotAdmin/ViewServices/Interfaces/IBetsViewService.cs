﻿using System.Collections.Generic;
using BetTeamsBattle.Data.Model.Entities;
using BetTeamsBattle.Frontend.Areas.NotAdmin.Models.BattleBets;

namespace BetTeamsBattle.Frontend.Areas.NotAdmin.ViewServices.Battles.Interfaces
{
    public interface IBetsViewService
    {
        MakeBetViewModel MakeBet(long battleId, long userId, MakeBetFormViewModel makeBetFormViewModel);
        MakeBetViewModel MakeBet(long battleId, long userId);
        BetsViewModel GetMyBattleBets(long battleId, long? userId);
        BetsViewModel GetUserBets(long userId, long? currentUserId);
        BetsViewModel GetTeamBets(long teamId);
    }
}
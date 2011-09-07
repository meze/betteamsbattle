using System;

namespace BetTeamsBattle.Frontend.Extensions
{
    public static class DateTimeExtensions
    {
         public static string ToShortDateLongTimeString(this DateTime dateTime)
         {
             return dateTime.ToShortDateString() + " " + dateTime.ToLongTimeString();
         }
    }
}
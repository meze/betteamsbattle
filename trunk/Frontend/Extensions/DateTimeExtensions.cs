using System;

namespace BetTeamsBattle.Frontend.Extensions
{
    internal static class DateTimeExtensions
    {
         public static string ToShortDateLongTimeString(this DateTime dateTime)
         {
             return dateTime.ToShortDateString() + " " + dateTime.ToLongTimeString();
         }
    }
}
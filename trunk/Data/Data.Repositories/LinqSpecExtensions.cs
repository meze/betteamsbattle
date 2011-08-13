using System.Linq.Expressions;
using BetTeamsBattle.Data.Model.Interfaces;

namespace BetTeamsBattle.Data.Repositories
{
    internal static class LinqSpecExtensions
    {
        /// <summary>
        /// Removes K cast
        /// </summary>
        public static LinqSpec<T> Flatten<T, K>(this LinqSpec<T> linqSpec)
        {
            return ExpressionVisitor<UnaryExpression>.Visit(linqSpec.Expression, ue => ue.Type == typeof (K) ? ue.Operand : ue);
        }
    }
}
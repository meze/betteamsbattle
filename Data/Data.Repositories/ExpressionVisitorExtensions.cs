#region

using System;
using System.Linq.Expressions;

#endregion

namespace BetTeamsBattle.Data.Repositories
{
    public class ExpressionVisitor<T> : ExpressionVisitor where T : Expression
    {
        readonly Func<T, Expression> _visitor;

        public ExpressionVisitor(Func<T, Expression> visitor)
        {
            this._visitor = visitor;
        }

        public static Expression Visit(Expression exp, Func<T, Expression> visitor)
        {
            return new ExpressionVisitor<T>(visitor).Visit(exp);
        }

        public static Expression<TDelegate> Visit<TDelegate>(Expression<TDelegate> exp, Func<T, Expression> visitor)
        {
            return (Expression<TDelegate>)new ExpressionVisitor<T>(visitor).Visit(exp);
        }

        public override Expression Visit(Expression exp)
        {
            if (exp is T && _visitor != null)
                exp = _visitor((T)exp);

            return base.Visit(exp);
        }
    }
}
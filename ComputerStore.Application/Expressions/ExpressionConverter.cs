using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStore.Application.Expressions
{
    public static class ExpressionConverter
    {
        public static Expression<Func<TDestination, bool>> Convert<TSource, TDestination>(Expression<Func<TSource, bool>> source)
        {
            var parameter = Expression.Parameter(typeof(TDestination), source.Parameters[0].Name);

            var visitor = new ReplaceVisitor<TSource, TDestination>(parameter);

            var body = visitor.Visit(source.Body);

            return Expression.Lambda<Func<TDestination, bool>>(body, parameter);
        }
        private class ReplaceVisitor<TSource, TDestination> : ExpressionVisitor
        {
            private readonly ParameterExpression _parameter;

            public ReplaceVisitor(ParameterExpression parameter)
            {
                _parameter = parameter;
            }

            protected override Expression VisitParameter(ParameterExpression node)
            {
                // Заменяем параметр исходного типа на параметр целевого типа
                return _parameter;
            }

            protected override Expression VisitMember(MemberExpression node)
            {
                if (node.Member.DeclaringType == typeof(TSource))
                {
                    // Находим член целевого типа с таким же именем
                    var member = typeof(TDestination).GetMember(node.Member.Name)[0];
                    return Expression.MakeMemberAccess(Visit(node.Expression), member);
                }

                return base.VisitMember(node);
            }
        }
    }
}

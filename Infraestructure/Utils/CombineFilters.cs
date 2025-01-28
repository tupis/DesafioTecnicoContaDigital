using System.Linq.Expressions;

namespace Infraestructure.Utils
{
    public static class CombineFilters<TEntity>
    {
        /// <summary>
        /// Combina um filtro padrão com um filtro personalizado.
        /// </summary>
        /// <param name="defaultFilter">Filtro padrão (não nulo).</param>
        /// <param name="customFilter">Filtro personalizado (opcional).</param>
        /// <returns>Uma expressão combinada que aplica ambos os filtros.</returns>
        public static Expression<Func<TEntity, bool>> Handle(
            Expression<Func<TEntity, bool>> defaultFilter,
            Expression<Func<TEntity, bool>>? customFilter = null)
        {
            if (customFilter == null)
            {
                return defaultFilter;
            }

            var parameter = Expression.Parameter(typeof(TEntity), "x");

            var defaultFilterBody = new ReplaceParameterVisitor(defaultFilter.Parameters[0], parameter)
                .Visit(defaultFilter.Body);

            var customFilterBody = new ReplaceParameterVisitor(customFilter.Parameters[0], parameter)
                .Visit(customFilter.Body);

            var combinedBody = Expression.AndAlso(defaultFilterBody, customFilterBody);

            return Expression.Lambda<Func<TEntity, bool>>(combinedBody, parameter);
        }

        private class ReplaceParameterVisitor(ParameterExpression oldParameter, ParameterExpression newParameter) : ExpressionVisitor
        {
            private readonly ParameterExpression _oldParameter = oldParameter;
            private readonly ParameterExpression _newParameter = newParameter;

            protected override Expression VisitParameter(ParameterExpression node)
            {
                return node == _oldParameter ? _newParameter : base.VisitParameter(node);
            }
        }
    }
}

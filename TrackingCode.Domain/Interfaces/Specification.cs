using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace TrackingCode.Domain.Interfaces
{
    public abstract class Specification<T>
    {
        public abstract Expression<Func<T, bool>> Criteria { get; }
        public virtual List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>> OrderBy { get; private set; }
        public Expression<Func<T, object>> OrderByDesc { get; private set; }
        public int Take { get; private set; }
        public int Skip { get; private set; }
        public bool IsPagingEnabled { get; private set; }

        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }

        protected void ApplyPaging(int page, int pageSize)
        {
            IsPagingEnabled = true;
            Skip = (page - 1) * pageSize;
            Take = pageSize;
        }

        protected void ApplyOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }

        protected void ApplyOrderByDesc(Expression<Func<T, object>> orderByExpression)
        {
            OrderByDesc = orderByExpression;
        }

        public Specification<T> And(Specification<T> specification)
        {
            return new AndSpecification<T>(this, specification);
        }

        protected static Expression<Func<T, bool>> CombineSpecifications(Specification<T> left, Specification<T> right, Func<Expression, Expression, BinaryExpression> combiner)
        {
            var expr1 = left.Criteria;
            var expr2 = right.Criteria;
            var arg = Expression.Parameter(typeof(T));
            var combined = combiner.Invoke(
                new ReplaceParameterVisitor { { expr1.Parameters.Single(), arg } }.Visit(expr1.Body),
                new ReplaceParameterVisitor { { expr2.Parameters.Single(), arg } }.Visit(expr2.Body));
            return Expression.Lambda<Func<T, bool>>(combined, arg);
        }
    }

    internal class ReplaceParameterVisitor : ExpressionVisitor, IEnumerable<KeyValuePair<ParameterExpression, ParameterExpression>>
    {

        private readonly Dictionary<ParameterExpression, ParameterExpression> _parameterMappings = new Dictionary<ParameterExpression, ParameterExpression>();

        protected override Expression VisitParameter(ParameterExpression node)
        {
            return _parameterMappings.TryGetValue(node, out var newValue) ? newValue : node;
        }

        public void Add(ParameterExpression parameterToReplace, ParameterExpression replaceWith) => _parameterMappings.Add(parameterToReplace, replaceWith);

        public IEnumerator<KeyValuePair<ParameterExpression, ParameterExpression>> GetEnumerator() => _parameterMappings.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
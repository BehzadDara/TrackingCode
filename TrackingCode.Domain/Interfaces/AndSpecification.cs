using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace TrackingCode.Domain.Interfaces
{
    public class AndSpecification<T> : Specification<T>
    {
        private readonly Specification<T> _left;
        private readonly Specification<T> _right;

        public AndSpecification(Specification<T> left, Specification<T> right)
        {
            _left = left;
            _right = right;
        }

        public override Expression<Func<T, bool>> Criteria =>
            CombineSpecifications(_left, _right, Expression.AndAlso);

        public override List<Expression<Func<T, object>>> Includes =>
            _left.Includes.Union(_right.Includes).ToList();
    }
}
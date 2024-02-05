using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using IQueryableAllFieldsSearcher.Infrastructure;
using System.Reflection;

namespace IQueryableAllFieldsSearcher.Providers
{
	public abstract class ComparableSearchExpressionProvider : DefaultSearchExpressionProvider
	{

		public override IEnumerable<SearchType> GetOperators()
		{
			return base.GetOperators()
					   .Concat(
						new[]
						{
							SearchType.GreaterThan,
							SearchType.GreaterThanEqualTo,
							SearchType.LessThan,
							SearchType.LessThanEqualTo
						});
		}

		public override Expression GetComparison(PropertyInfo property, ParameterExpression obj, object value, MemberExpression left, SearchType searchType, Expression right)
		{
			switch (searchType)
			{
				case SearchType.GreaterThan:
					return Expression.AndAlso(Expression.NotEqual(left, Expression.Constant(null, property.PropertyType)), Expression.GreaterThan(left, right));
				case SearchType.GreaterThanEqualTo:
					return Expression.AndAlso(Expression.NotEqual(left, Expression.Constant(null, property.PropertyType)), Expression.GreaterThanOrEqual(left, right));
				case SearchType.LessThan:
					return Expression.AndAlso(Expression.NotEqual(left, Expression.Constant(null, property.PropertyType)), Expression.LessThan(left, right));
				case SearchType.LessThanEqualTo:
					return Expression.AndAlso(Expression.NotEqual(left, Expression.Constant(null, property.PropertyType)), Expression.LessThanOrEqual(left, right));
				default: return base.GetComparison(property, obj, value, left, searchType, right);
			};
		}
	}
}

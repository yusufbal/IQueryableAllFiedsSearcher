using IQueryableAllFieldsSearcher.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace IQueryableAllFieldsSearcher.Providers
{
	public class EnumerationSearchExpressionProvider : DefaultSearchExpressionProvider
	{
		private static readonly MethodInfo _stringContainsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
		protected const string ContainsOperator = "co";
		public override IEnumerable<SearchType> GetOperators()
		{
			return base.GetOperators();
		}

		public override Expression GetComparison(PropertyInfo property, ParameterExpression obj, object value, MemberExpression left, SearchType searchType, Expression right)
		{
			switch (searchType)
			{
				case SearchType.Equals: return Expression.Equal(left.CastToObjectAndString().TrimToLower(), right.TrimToLower());
				case SearchType.Contains: return Expression.Call(left.CastToObjectAndString().TrimToLower(), _stringContainsMethod, right.TrimToLower());
				default: return base.GetComparison(property, obj, value, left, searchType, right);
			}
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Reflection;
using System.Text;
using IQueryableAllFieldsSearcher.Infrastructure;

namespace IQueryableAllFieldsSearcher.Providers
{
	public class StringSearchExpressionProvider : DefaultSearchExpressionProvider
	{
		private static readonly MethodInfo StartsWithMethod = typeof(string)
			.GetMethods()
			.First(x => x.Name == "StartsWith" && x.GetParameters().Length == 2);

		private static readonly MethodInfo StringEqualsMethod = typeof(string)
			.GetMethods()
			.First(x => x.Name == "Equals" && x.GetParameters().Length == 2);

		private static readonly MethodInfo ContainsMethod = typeof(string)
			.GetMethods()
			.First(x => x.Name == "Contains" && x.GetParameters().Length == 1);

		private static readonly MethodInfo ToLowerMethod = typeof(string)
			.GetMethods()
			.First(x => x.Name == "ToLower");

		private static readonly MethodInfo _stringContainsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });

		private static ConstantExpression IgnoreCase
			=> Expression.Constant(StringComparison.OrdinalIgnoreCase);

		public override IEnumerable<SearchType> GetOperators()
		{
			return base.GetOperators()
						   .Concat(
							   new[]
							   {
									SearchType.StartsWith,
									SearchType.Contains
							   });
		}

		public override Expression GetComparison(PropertyInfo property, ParameterExpression obj, object value, MemberExpression left, SearchType searchType, Expression right)
		{
			switch (searchType)
			{
				case SearchType.StartsWith: return Expression.Call(left, StartsWithMethod, right, IgnoreCase);
				case SearchType.Contains: return Expression.Call(left.TrimToLower(), _stringContainsMethod, right.TrimToLower());
				case SearchType.Equals: return Expression.Equal(left.TrimToLower(), right.TrimToLower());
				default: return base.GetComparison(property, obj, value, left, searchType, right);
			}
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using IQueryableAllFieldsSearcher.Infrastructure;
using IQueryableAllFieldsSearcher.Interfaces;

namespace IQueryableAllFieldsSearcher.Providers
{
    public class DefaultSearchExpressionProvider : ISearchExpressionProvider
	{
		public virtual IEnumerable<SearchType> GetOperators()
		{
			yield return SearchType.Equals;
		}

		public virtual ConstantExpression GetValue(string input)
		{
			return Expression.Constant(input);
		}

		public virtual Expression GetComparison(PropertyInfo property, ParameterExpression obj, object value,MemberExpression left, SearchType searchType, Expression right)
		{
			switch (searchType)
			{
				case SearchType.Equals: return Expression.Equal(left, right);
				default: throw new ArgumentException($"Invalid Operator.");
			};
		}
	}
}

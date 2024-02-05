using IQueryableAllFieldsSearcher.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace IQueryableAllFieldsSearcher.Interfaces
{
	public interface ISearchExpressionProvider
	{
		IEnumerable<SearchType> GetOperators();

		ConstantExpression GetValue(string input);

		Expression GetComparison(PropertyInfo property, ParameterExpression obj, object value, MemberExpression left, SearchType searchType, Expression right);
	}
}

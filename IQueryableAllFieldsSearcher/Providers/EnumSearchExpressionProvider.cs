using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace IQueryableAllFieldsSearcher.Providers
{
	public class EnumSearchExpressionProvider<TEnum> : EnumerationSearchExpressionProvider where TEnum : struct
	{
		public override ConstantExpression GetValue(string input)
		{
			return Expression.Constant(input);
		}
	}
}

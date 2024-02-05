using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace IQueryableAllFieldsSearcher.Providers
{
	public class DoubleSearchExpressionProvider : ComparableSearchExpressionProvider
	{
		public override ConstantExpression GetValue(string input)
		{
			if (!double.TryParse(input, out var value))
			{
				throw new ArgumentException("Invalid search value.");
			}

			return Expression.Constant(value);
		}
	}
}

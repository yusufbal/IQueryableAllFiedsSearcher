﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace IQueryableAllFieldsSearcher.Providers
{
	public class IntSearchExpressionProvider : ComparableSearchExpressionProvider
	{
		public override ConstantExpression GetValue(string input)
		{
			if (!int.TryParse(input, out var value))
			{
				throw new ArgumentException("Invalid search value.");
			}

			return Expression.Constant(value);
		}
	}
}

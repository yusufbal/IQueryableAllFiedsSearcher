using IQueryableAllFieldsSearcher.Providers;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQueryableAllFieldsSearcher.Attributes
{
	[AttributeUsage(AttributeTargets.Property)]
	public class SearchableDoubleAttribute : SearchableAttribute
	{
		public SearchableDoubleAttribute()
		{
			ExpressionProvider = new DoubleSearchExpressionProvider();
		}
	}
}

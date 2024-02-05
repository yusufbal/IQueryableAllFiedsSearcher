using IQueryableAllFieldsSearcher.Providers;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQueryableAllFieldsSearcher.Attributes
{
	[AttributeUsage(AttributeTargets.Property)]
	public class SearchableShortAttribute : SearchableAttribute
	{
		public SearchableShortAttribute()
		{
			ExpressionProvider = new ShortSearchExpressionProvider();
		}
	}
}

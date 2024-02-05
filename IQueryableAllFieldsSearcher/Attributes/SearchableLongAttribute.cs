using IQueryableAllFieldsSearcher.Providers;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQueryableAllFieldsSearcher.Attributes
{
	[AttributeUsage(AttributeTargets.Property)]
	public class SearchableLongAttribute : SearchableAttribute
	{
		public SearchableLongAttribute()
		{
			ExpressionProvider = new LongSearchExpressionProvider();
		}
	}
}

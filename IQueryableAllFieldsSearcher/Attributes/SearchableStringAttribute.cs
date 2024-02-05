using IQueryableAllFieldsSearcher.Infrastructure;
using IQueryableAllFieldsSearcher.Providers;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQueryableAllFieldsSearcher.Attributes
{
	[AttributeUsage(AttributeTargets.Property)]
	public class SearchableStringAttribute : SearchableAttribute
	{
		public SearchableStringAttribute()
		{
			ExpressionProvider = new StringSearchExpressionProvider();
		}

		public SearchableStringAttribute(SearchType type)
		{
			ExpressionProvider = new StringSearchExpressionProvider();
			SearchType = type;
		}

	}
}

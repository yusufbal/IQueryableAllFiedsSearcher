using IQueryableAllFieldsSearcher.Providers;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQueryableAllFieldsSearcher.Attributes
{
	[AttributeUsage(AttributeTargets.Property)]
	public class SearchableIntAttribute : SearchableAttribute
	{
		public SearchableIntAttribute()
		{
			ExpressionProvider = new IntSearchExpressionProvider();
		}
	}
}

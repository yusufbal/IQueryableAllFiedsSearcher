using IQueryableAllFieldsSearcher.Providers;
using System;

namespace IQueryableAllFieldsSearcher.Attributes
{
	[AttributeUsage(AttributeTargets.Property)]
	public class SearchableDateTimeAttribute : SearchableAttribute
	{
		public SearchableDateTimeAttribute()
		{
			ExpressionProvider = new DateTimeSearchExpressionProvider();
		}
	}
}

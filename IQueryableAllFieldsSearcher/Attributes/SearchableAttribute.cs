using IQueryableAllFieldsSearcher.Infrastructure;
using IQueryableAllFieldsSearcher.Interfaces;
using IQueryableAllFieldsSearcher.Providers;
using System;

namespace IQueryableAllFieldsSearcher.Attributes
{
	[AttributeUsage(AttributeTargets.Property)]
	public class SearchableAttribute : Attribute
	{
		public string Property { get; set; }

		public ISearchExpressionProvider ExpressionProvider { get; set; } = new DefaultSearchExpressionProvider();

		public SearchType SearchType { get; set; } = SearchType.Equals;

		public int Group { get; set; } = 0;

		public SearchLogic SearchLogic { get; set; } = SearchLogic.Or;
    }
}

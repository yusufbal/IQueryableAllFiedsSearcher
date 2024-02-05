using IQueryableAllFieldsSearcher.Providers;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQueryableAllFieldsSearcher.Attributes
{
	[AttributeUsage(AttributeTargets.Property)]
	public class SearchableEnumAttribute : SearchableAttribute
	{
		public SearchableEnumAttribute(Type enumType)
		{
			ExpressionProvider = (EnumerationSearchExpressionProvider)Activator.CreateInstance(
				typeof(EnumSearchExpressionProvider<>).MakeGenericType(enumType));
		}
	}
}

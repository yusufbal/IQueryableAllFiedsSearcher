using IQueryableAllFieldsSearcher.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace IQueryableAllFieldsSearcher.Providers
{
	public class DateTimeSearchExpressionProvider : ComparableSearchExpressionProvider
	{
		public override IEnumerable<SearchType> GetOperators()
		{
			return base.GetOperators()
					   .Concat(
						new[]
						{
							SearchType.DateDayOnly,
							SearchType.DateMonthOnly,
							SearchType.DateYearOnly
						});
		}

		public override Expression GetComparison(PropertyInfo property, ParameterExpression obj, object value, MemberExpression left, SearchType searchType, Expression right)
		{
			var propertyReference = Expression.Property(obj, property);
			switch (searchType)
			{
				case SearchType.DateYearOnly:
					propertyReference = Expression.Property(propertyReference, "Year");
					return Expression.Equal(propertyReference, Expression.Constant(Convert.ToDateTime(value).Year));
				case SearchType.DateMonthOnly:
					propertyReference = Expression.Property(propertyReference, "Month");
					return Expression.Equal(propertyReference, Expression.Constant(Convert.ToDateTime(value).Month));
				case SearchType.DateDayOnly:
					propertyReference = Expression.Property(propertyReference, "Day");
					return Expression.Equal(propertyReference, Expression.Constant(Convert.ToDateTime(value).Day));
				default: return base.GetComparison(property, obj, value, left, searchType, right);
			};
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using IQueryableAllFieldsSearcher.Infrastructure;
using System.Reflection;
using IQueryableAllFieldsSearcher.Attributes;

namespace IQueryableAllFieldsSearcher
{
	public static class Extensions
	{
		public static IQueryable<T> MultipleFilter<T, TValue>(this IQueryable<T> query, TValue searchValue, int searchOnlyGroup = 0)
		{
			var modifiedQuery = query;
			var parameterExpression = ExpressionHelper.Parameter<T>();
			Expression finalExpression = null;


			var searchExpr = Expression.Constant(searchValue);

			var groupedProperties = typeof(T).GetProperties().Where(x => x.GetCustomAttribute(typeof(SearchableAttribute)) != null && (searchOnlyGroup == 0 || (searchOnlyGroup > 0 && ((SearchableAttribute)x.GetCustomAttribute(typeof(SearchableAttribute))).Group == searchOnlyGroup)))
													  .GroupBy(x => ((SearchableAttribute)x.GetCustomAttribute(typeof(SearchableAttribute))).Group)
													  .OrderBy(x => x.Key);

			SearchableAttribute oldSearchAttribute = null;
			foreach (var group in groupedProperties)
			{
				var groupedExpressions = new List<Expression>();
				SearchableAttribute searchAttribute = new SearchableAttribute();
				foreach (var property in group)
				{
					searchAttribute = GetSearchAttribute(property);
					if (property.PropertyType == typeof(DateTime) && !DateTime.TryParse(searchExpr.Value.ToString(), out DateTime val))
						continue;
					if ((property.PropertyType == typeof(decimal) ||
						property.PropertyType == typeof(decimal?) ||
						property.PropertyType == typeof(int) ||
						property.PropertyType == typeof(int?) ||
						property.PropertyType == typeof(long) ||
						property.PropertyType == typeof(long?)) && !searchExpr.Value.ToString().All(char.IsDigit))
						continue;
					var comparisionExpression = GetComparisonExpression(property, parameterExpression, searchExpr.Value);
					groupedExpressions.Add(comparisionExpression);
				}
				if (searchAttribute.SearchLogic == SearchLogic.And)
				{
					if (finalExpression == null)
						finalExpression = Expression.Constant(true);
					if (oldSearchAttribute != null && oldSearchAttribute.SearchLogic == SearchLogic.Or)
					{
						finalExpression = Expression.OrElse(finalExpression, Expression.Constant(true));
					}
					foreach (var expression in groupedExpressions)
					{
						finalExpression = Expression.AndAlso(finalExpression, expression);
					}
				}
				else
				{
					if (finalExpression == null)
						finalExpression = Expression.Constant(false);
					foreach (var expression in groupedExpressions)
					{
						finalExpression = Expression.OrElse(finalExpression, expression);
					}
				}
				oldSearchAttribute = searchAttribute;
			}
			var lambdaExpression = ExpressionHelper.GetLambda<T, bool>(parameterExpression, finalExpression);
			modifiedQuery = ExpressionHelper.CallWhere(modifiedQuery, lambdaExpression);

			return modifiedQuery;
		}

		public static IQueryable<T> MultipleGroupFilter<T, TValue>(this IQueryable<T> query, TValue searchValue)
		{
			var groupedProperties = typeof(T).GetProperties().Where(x => x.GetCustomAttribute(typeof(SearchableAttribute)) != null)
													  .GroupBy(x => ((SearchableAttribute)x.GetCustomAttribute(typeof(SearchableAttribute))).Group)
													  .OrderBy(x => x.Key);
			if (groupedProperties.Count() != searchValue.ToString().Split(';').Length)
			{
				throw new Exception("Group count must equal to ;");
			}
			var result = Enumerable.Empty<T>().AsQueryable();
			int group = 1;
			foreach (var item in searchValue.ToString().Split(";"))
			{
				var groupResult = MultipleFilter(query, item, group);
				result = result.Union(groupResult);
				group++;
			}
			return result;
		}

		private static Expression GetComparisonExpression(PropertyInfo property, ParameterExpression obj, object value)
		{
			var left = ExpressionHelper.GetMemberExpression(obj, property.Name);
			var searchAttribute = GetSearchAttribute(property);
			var constant = searchAttribute.ExpressionProvider.GetValue(value == null ? string.Empty : value.ToString());
			Expression right;
			if (left.Type == typeof(DateTime) && DateTime.TryParse(value.ToString(), out DateTime val))
				constant = Expression.Constant(Convert.ToDateTime(value.ToString()));
			if (constant.Type != left.Type && left.Type.BaseType != typeof(Enum))
			{
				right = Expression.Convert(constant, left.Type);
			}
			else
				right = (Expression)constant;
			return searchAttribute.ExpressionProvider.GetComparison(property, obj, value, left, searchAttribute.SearchType, right);
		}

		private static SearchableAttribute GetSearchAttribute(PropertyInfo property)
		{
			return (SearchableAttribute)property.GetCustomAttribute(typeof(SearchableAttribute));
		}



	}

}

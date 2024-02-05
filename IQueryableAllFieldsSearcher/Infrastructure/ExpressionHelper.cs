using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Linq;
using System.Reflection;
using System.Text;

namespace IQueryableAllFieldsSearcher.Infrastructure
{
	public static class ExpressionHelper
	{
		private static readonly MethodInfo _stringTrimMethod = typeof(string).GetMethod("Trim", new Type[0]);
		private static readonly MethodInfo _stringToLowerMethod = typeof(string).GetMethod("ToLower", new Type[0]);

		private static readonly MethodInfo LambdaMethod = typeof(Expression)
			.GetMethods()
			.First(x => x.Name == "Lambda" && x.ContainsGenericParameters && x.GetParameters().Length == 2);

		private static readonly MethodInfo[] QueryableMethods = typeof(Queryable)
			.GetMethods()
			.ToArray();

		private static MethodInfo GetLambdaFuncBuilder(Type source, Type dest)
		{
			var predicate = typeof(Func<,>).MakeGenericType(source, dest);
			return LambdaMethod.MakeGenericMethod(predicate);
		}

		public static LambdaExpression GetLambda<TSource, TDest>(ParameterExpression obj, Expression arg)
		{
			return GetLambda(typeof(TSource), typeof(TDest), obj, arg);
		}

		public static LambdaExpression GetLambda(Type source, Type dest, ParameterExpression obj, Expression arg)
		{
			var lambdaBuilder = GetLambdaFuncBuilder(source, dest);
			return (LambdaExpression)lambdaBuilder.Invoke(null, new object[] { arg, new[] { obj } });
		}

		public static IQueryable<T> CallWhere<T>(IQueryable<T> query, LambdaExpression predicate)
		{
			var whereMethodBuilder = QueryableMethods
				.First(x => x.Name == "Where" && x.GetParameters().Length == 2)
				.MakeGenericMethod(typeof(T));

			return (IQueryable<T>)whereMethodBuilder
				.Invoke(null, new object[] { query, predicate });
		}

		public static ParameterExpression Parameter<T>()
		{
			return Expression.Parameter(typeof(T), $"{typeof(T).Name}Search");
		}

		public static MemberExpression GetMemberExpression(Expression param, string propertyName)
		{
			var index = propertyName.IndexOf('.');
			while (index != -1)
			{
				param = Expression.Property(param, propertyName.Substring(0, index));
				propertyName = propertyName.Substring(index + 1);
				index = propertyName.IndexOf('.');
			}

			return Expression.Property(param, propertyName);
		}

		public static Expression CastToObjectAndString(this MemberExpression member)
		{
			var objectMemberConvert = Expression.Convert(member, typeof(object));
			var stringMemberConvert = Expression.Convert(objectMemberConvert, typeof(string));
			return stringMemberConvert as Expression;
		}


		public static Expression TrimToLower(this MemberExpression member)
		{
			var trimMemberCall = Expression.Call(member, _stringTrimMethod);
			return Expression.Call(trimMemberCall, _stringToLowerMethod);
		}

		public static Expression TrimToLower(this ConstantExpression constant)
		{
			var trimMemberCall = Expression.Call(constant, _stringTrimMethod);
			return Expression.Call(trimMemberCall, _stringToLowerMethod);
		}

		public static Expression TrimToLower(this Expression constant)
		{
			var trimMemberCall = Expression.Call(constant, _stringTrimMethod);
			return Expression.Call(trimMemberCall, _stringToLowerMethod);
		}

	}
}

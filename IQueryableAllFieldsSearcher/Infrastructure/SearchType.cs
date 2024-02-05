using System;
using System.Collections.Generic;
using System.Text;

namespace IQueryableAllFieldsSearcher.Infrastructure
{
	public enum SearchType
	{
		Equals = 0,
		StartsWith = 1,
		Contains = 2,
		GreaterThan = 3,
		GreaterThanEqualTo = 4,
		LessThan = 5,
		LessThanEqualTo = 6,
		DateYearOnly = 7,
		DateMonthOnly = 8,
		DateDayOnly = 9
	}
}

using IQueryableAllFieldsSearcher.Attributes;
using IQueryableAllFieldsSearcher.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQueryableAllFieldsSearcher.Test
{
	public interface IPerson
	{
		string Name { get; set; }

		string Surname { get; set; }

		string Country { get; set; }

		string City { get; set; }

		DateTime BirthDate { get; set; }

		decimal Salary { get; set; }

		decimal? Class { get; set; }
	}
	public class PersonDefaultString : IPerson
	{
		[SearchableString(SearchType = SearchType.Contains)]
		public string Name { get; set; }

		[SearchableString(SearchType = SearchType.Contains)]
		public string Surname { get; set; }

		public string Country { get; set; }

		public string City { get; set; }

		public DateTime BirthDate { get; set; }

		public decimal Salary { get; set; }

		public decimal? Class { get; set; }
	}

	public class PersonStartsWith : IPerson
	{
		public string Name { get; set; }

		[SearchableString(SearchType = SearchType.StartsWith)]
		public string Surname { get; set; }

		public string Country { get; set; }

		public string City { get; set; }

		public DateTime BirthDate { get; set; }

		public decimal Salary { get; set; }

		public decimal? Class { get; set; }
	}

	public class PersonNameSurnameGroupedOrContains : IPerson
	{
		[SearchableString(SearchType = SearchType.Contains, Group = 1)]
		public string Name { get; set; }

		[SearchableString(SearchType = SearchType.Contains, Group = 2)]
		public string Surname { get; set; }

		public string Country { get; set; }

		public string City { get; set; }

		public DateTime BirthDate { get; set; }

		public decimal Salary { get; set; }

		public decimal? Class { get; set; }
	}

	public class PersonNameSurnameCountryGroupedOrContains : IPerson
	{
		[SearchableString(SearchType = SearchType.Contains, Group = 1)]
		public string Name { get; set; }

		[SearchableString(SearchType = SearchType.Contains, Group = 2)]
		public string Surname { get; set; }

		[SearchableString(SearchType = SearchType.Contains, Group = 2)]
		public string Country { get; set; }

		public string City { get; set; }

		public DateTime BirthDate { get; set; }

		public decimal Salary { get; set; }

		public decimal? Class { get; set; }
	}

	public class PersonNameSurnameCountryGroupedAndContains : IPerson
	{
		[SearchableString(SearchType = SearchType.Contains, Group = 1, SearchLogic=SearchLogic.Or)]
		public string Name { get; set; }

		[SearchableString(SearchType = SearchType.Contains, Group = 2, SearchLogic = SearchLogic.And)]
		public string Surname { get; set; }

		[SearchableString(SearchType = SearchType.Contains, Group = 2, SearchLogic = SearchLogic.And)]
		public string Country { get; set; }

		public string City { get; set; }

		public DateTime BirthDate { get; set; }

		public decimal Salary { get; set; }

		public decimal? Class { get; set; }
	}

	public class PersonNameSurnameBirthYearGroupedOrContains : IPerson
	{
		[SearchableString(SearchType = SearchType.Contains, Group = 1)]
		public string Name { get; set; }

		public string Surname { get; set; }

		public string Country { get; set; }

		public string City { get; set; }

		[SearchableDateTime(SearchType = SearchType.DateYearOnly, Group = 2)]
		public DateTime BirthDate { get; set; }

		public decimal Salary { get; set; }

		public decimal? Class { get; set; }
	}

	public class PersonClassEquals : IPerson
	{
		public string Name { get; set; }

		public string Surname { get; set; }

		public string Country { get; set; }

		public string City { get; set; }

		public DateTime BirthDate { get; set; }

		public decimal Salary { get; set; }

		[SearchableDecimal()]
		public decimal? Class { get; set; }
	}

	public class PersonClassGreaterThan : IPerson
	{
		public string Name { get; set; }

		public string Surname { get; set; }

		public string Country { get; set; }

		public string City { get; set; }

		public DateTime BirthDate { get; set; }

		public decimal Salary { get; set; }

		[SearchableDecimal(SearchType= SearchType.GreaterThan)]
		public decimal? Class { get; set; }
	}

	public class PersonSearchAll : IPerson
	{
		[SearchableString]
		public string Name { get; set; }

		[SearchableString]
		public string Surname { get; set; }

		[SearchableString]
		public string Country { get; set; }

		[SearchableString]
		public string City { get; set; }

		[SearchableDateTime]
		public DateTime BirthDate { get; set; }

		[SearchableDecimal]
		public decimal Salary { get; set; }

		[SearchableDecimal]
		public decimal? Class { get; set; }
	}
}

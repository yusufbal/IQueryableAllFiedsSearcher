using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using IQueryableAllFieldsSearcher;

namespace IQueryableAllFieldsSearcher.Test
{
	[TestClass]
	public class UnitTest
	{
		[TestMethod]
		public void TestDefaultString()
		{
			var data = GenerateDummyDefaultData<PersonDefaultString>();
			var result = data.MultipleFilter("ha").ToList();
			Assert.AreEqual(6, result.Count);
		}

		[TestMethod]
		public void TestStartsWith()
		{
			var data = GenerateDummyDefaultData<PersonStartsWith>();
			var result = data.MultipleFilter("ha").ToList();
			Assert.AreEqual(2, result.Count);
		}

		[TestMethod]
		public void TestNameSurnameGroupedOrContains()
		{
			var data = GenerateDummyDefaultData<PersonNameSurnameGroupedOrContains>();
			var result = data.MultipleFilter("on").ToList();
			Assert.AreEqual(15, result.Count);
		}

		[TestMethod]
		public void TestPersonNameSurnameCountryGroupedOrContains()
		{
			var data = GenerateDummyDefaultData<PersonNameSurnameCountryGroupedOrContains>();
			var result = data.MultipleFilter("ke").ToList();
			Assert.AreEqual(49, result.Count);
		}

		[TestMethod]
		public void TestPersonNameSurnameCountryGroupedAndContains()
		{
			var data = GenerateDummyDefaultData<PersonNameSurnameCountryGroupedAndContains>();
			var result = data.MultipleFilter("ey").ToList();
			Assert.AreEqual(1, result.Count);
		}

		[TestMethod]
		public void TestPersonNameSurnameBirthYearGroupedOrContains()
		{
			var data = GenerateDummyDefaultData<PersonNameSurnameBirthYearGroupedOrContains>();
			var result = data.MultipleGroupFilter("ard;01.01.1968").ToList();
			Assert.AreEqual(19, result.Count);
		}

		[TestMethod]
		public void TestPersonClassEquals()
		{
			var data = GenerateDummyDefaultData<PersonClassEquals>();
			var result = data.MultipleFilter("1").ToList();
			Assert.AreEqual(9, result.Count);
		}

		[TestMethod]
		public void TestPersonClassGreaterThan()
		{
			var data = GenerateDummyDefaultData<PersonClassGreaterThan>();
			var result = data.MultipleFilter("1").ToList();
			Assert.AreEqual(29, result.Count);
		}

		[TestMethod]
		public void TestPersonSearchAll()
		{
			var data = GenerateDummyDefaultData<PersonSearchAll>();
			var result = data.MultipleFilter("Richard").ToList();
			Assert.AreEqual(1, result.Count);
		}



		private IQueryable<T> GenerateDummyDefaultData<T>() where T : IPerson
		{
			var data = new List<T>();
			string[] lines = File.ReadAllLines(@"Data.txt", Encoding.UTF8);
			foreach (string line in lines)
			{
				var obj = Activator.CreateInstance<T>();
				obj.Name = line.Split(';')[0];
				obj.Surname = line.Split(';')[1];
				obj.BirthDate = Convert.ToDateTime(line.Split(';')[2], CultureInfo.GetCultureInfo("en-US").DateTimeFormat);
				obj.Country = line.Split(';')[3];
				obj.City = line.Split(';')[4];
				obj.Salary = Convert.ToDecimal(line.Split(';')[5]);
				if (!string.IsNullOrEmpty(line.Split(';')[6]))
					obj.Class = Convert.ToDecimal(line.Split(';')[6]);
				data.Add(obj);
			}
			return data.AsQueryable();
		}
	}
}

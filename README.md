# IQueryableAllFiedsSearcher: Custom Search Library for IQueryable Objects
## Overview
This library was born out of the need for performing customized searches on IQueryable objects in MVC applications. While using the JqueryDataTablesServerSide library, it was possible to conduct column-based searches, yet the capability for more complex and tailored searches was lacking. To address this need, a basic and useful library has been developed to support .NET Core 3.1 and higher versions, catering to a broader audience.

## Features
* **Multiple Search:** Ability to search across all fields of IQueryable objects.
* **Search Type:** Option to specify 'and'/'or' conditions when searching within fields.
* **Grouping:** Advanced searching by grouping 'and'/'or' type searches.
* **Contains/Starts With/Ends With Search:** Enhanced search capabilities for string expressions.
* **Year/Month/Day Based Search:** Searching dates based on year, month, or day.
* **Greater/Equal or Greater/Less/Equal or Less Search:** Advanced searching for numerical expressions.
* **Bulk Search:** Search for multiple conditions separated by a semicolon (;), such as finding names containing "ard" and those born in the year 1968.

## Inspiration and Development
This library was inspired by the specific needs for customized search capabilities within an existing MVC application. During its development, attributes and methods from the JqueryDataTablesServerSide (https://github.com/fingers10/JqueryDataTablesServerSide) library were taken as a basis, but were customized and extended to support a wider range of use cases.

## Example Usage
To utilize the advanced search capabilities, properties within your models can be marked with attributes to indicate they are searchable. Here's how you can implement and use these searches:

```cs
[SearchableString]
public string Name { get; set; }

// For a simple search across all fields with the condition "on"
var result = data.MultipleFilter("on").ToList();

// For a more complex, grouped search with conditions separated by a semicolon ";"
var result = data.MultipleGroupFilter("ard;01.01.1968").ToList();

```
You can see and test all usage examples and tests together with the sample data set in the IQueryableAllFieldsSearcher.Test project.

## License
This project is licensed under the MIT License. For more information, please refer to the LICENSE file.

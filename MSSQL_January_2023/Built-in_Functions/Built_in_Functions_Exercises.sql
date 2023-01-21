--Queries for SoftUni Database

USE SoftUni;
GO

--Find Names of All Employees by First Name

 SELECT 
	   [FirstName],
	   [LastName] 
  FROM Employees
 WHERE [FirstName] LIKE 'Sa%';

 --Find Names of All Employees by Last Name 

  SELECT 
	   [FirstName],
	   [LastName] 
  FROM Employees
 WHERE [LastName] LIKE '%ei%';

 --Find First Names of All Employees

 SELECT FirstName 
   FROM Employees
  WHERE DepartmentID IN (3, 10) AND DATEPART(year,HireDate) BETWEEN 1995 AND 2005;

  --Find All Employees Except Engineers

SELECT FirstName, LastName
  FROM Employees
 WHERE JobTitle NOT LIKE '%engineer%';

 --Find Towns with Name Length

 SELECT [Name] 
   FROM Towns
  WHERE LEN([Name]) IN (5 ,6)
ORDER BY [Name];

--Find Towns Starting With

SELECT TownID,
	   [Name] 
  FROM Towns
 WHERE [Name] LIKE '[MKBE]%' 
ORDER BY [Name];

--Find Towns Not Starting With

SELECT TownID,
	   [Name] 
  FROM Towns
 WHERE [Name] NOT LIKE '[RBD]%' 
ORDER BY [Name];

--Create View Employees Hired After 2000 Year
GO

CREATE VIEW V_EmployeesHiredAfter2000 AS
	 SELECT FirstName, LastName
	   FROM Employees
	  WHERE DATEPART(year, HireDate) > 2000;

GO
--Length of Last Name

SELECT FirstName, LastName 
  FROM Employees
 WHERE LEN(LastName) = 5;

 --Rank Employees by Salary

  SELECT EmployeeID,
	     FirstName, 
	     LastName, 
	     Salary,
	     DENSE_RANK() OVER (PARTITION BY Salary ORDER BY EmployeeID) AS [Rank]
    FROM Employees
   WHERE Salary BETWEEN 10000 AND 50000
ORDER BY Salary DESC;

--Find All Employees with Rank 2

WITH RankedEmployees AS
(
  SELECT EmployeeID,
	     FirstName, 
	     LastName, 
	     Salary,
	     DENSE_RANK() OVER (PARTITION BY Salary ORDER BY EmployeeID) AS [Rank]
    FROM Employees
   WHERE Salary BETWEEN 10000 AND 50000
)
  SELECT EmployeeID,
	     FirstName, 
	     LastName, 
	     Salary,
	     [Rank]
    FROM RankedEmployees
   WHERE [Rank] = 2
ORDER BY Salary DESC;

--Part II � Queries for Geography Database

USE [Geography]
GO

--Countries Holding 'A' 3 or More Times

SELECT CountryName,
	   IsoCode
  FROM Countries
 WHERE LEN(CountryName) - LEN(REPLACE(CountryName, 'a', '')) >= 3
 ORDER BY IsoCode;


 --Mix of Peak and River Names

 SELECT p.PeakName, 
		r.RiverName,
		LOWER(CONCAT(p.PeakName, SUBSTRING(r.RiverName, 2, LEN(r.RiverName)))) AS Mix
   FROM Peaks AS p, Rivers AS r
  WHERE RIGHT(LOWER(p.PeakName), 1) = LEFT(LOWER(r.RiverName), 1)
ORDER BY Mix;

--Part III � Queries for Diablo Database
USE Diablo;
GO

--Games From 2011 and 2012 Year

SELECT TOP(50)
	   [Name],
	   FORMAT([Start], 'yyyy-MM-dd') AS [Start]
  FROM Games
WHERE DATEPART(year, [Start]) IN (2011, 2012)
ORDER BY [Start], [Name];

--User Email Providers

SELECT 
	 Username,
	 SUBSTRING(Email, CHARINDEX('@', Email) + 1, LEN(Email)) AS [Email Provider]
FROM Users
ORDER BY [Email Provider], Username;

--Get Users with IP Address Like Pattern

SELECT Username,
	   IpAddress
  FROM Users
 WHERE IpAddress LIKE '___.1%.%.___'
 ORDER BY Username;

 --Show All Games with Duration & Part of the Day

SELECT 
	[Name] AS [Game],
	CASE
		WHEN DATEPART(HOUR, [Start]) >= 0 AND DATEPART(HOUR, [Start]) < 12 THEN 'Morning'
		WHEN DATEPART(HOUR, [Start]) >= 12 AND DATEPART(HOUR, [Start]) < 18 THEN 'Afternoon'
		WHEN DATEPART(HOUR, [Start]) >= 18 AND DATEPART(HOUR, [Start]) < 24 THEN 'Evening'
	END AS [Part of the Day],
	CASE
		WHEN Duration <= 3 THEN 'Extra Short'
		WHEN Duration BETWEEN 4 AND 6 THEN 'Short'
		WHEN Duration > 6 THEN 'Long'
		ELSE 'Extra Long'
	END AS [Duration]
FROM Games
ORDER BY [Name], [Duration], [Part of the Day];

-- Part IV – Date Functions Queries
USE Orders;
GO

-- Orders Table

SELECT 
	ProductName,
	OrderDate,
	DATEADD(DAY, 3, OrderDate) AS [Pay Due],
	DATEADD(MONTH, 1, OrderDate) AS [Deliver Due]
FROM Orders;

-- People Table

CREATE TABLE People
(
	Id INT IDENTITY PRIMARY KEY,
	[Name] NVARCHAR(50) NOT NULL,
	BirthDate DATETIME2 NOT NULL
)

INSERT INTO People
VALUES ('Victor', '2000-12-07'),
	   ('Steven', '1992-09-10'),
	   ('Stephen', '1910-09-19'),
	   ('John', '2010-01-06');


SELECT 
	 [Name],
	 DATEDIFF(YEAR, BirthDate, GETDATE()) AS [Age in Years],
	 DATEDIFF(MONTH, BirthDate, GETDATE()) AS [Age in Months],
	 DATEDIFF(DAY, BirthDate, GETDATE()) AS [Age in Days],
	 DATEDIFF(MINUTE, BirthDate, GETDATE()) AS [Age in Minutes]
FROM People;
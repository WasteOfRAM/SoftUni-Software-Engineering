--Part I � Queries for SoftUni Database

USE SoftUni;
GO

--Employee Address

   SELECT TOP (5)
			e.EmployeeID,
			e.JobTitle,
			a.AddressID,
			a.AddressText
    FROM Employees AS e
    JOIN Addresses AS a 
      ON e.AddressID = a.AddressID
ORDER BY e.AddressID; 


--Addresses with Towns

SELECT TOP(50)
		e.FirstName,
		e.LastName,
		t.[Name],
		a.AddressText
  FROM Employees AS e
  JOIN Addresses AS a ON e.AddressID = a.AddressID
  JOIN Towns AS t ON a.TownID = t.TownID
ORDER BY e.FirstName, e.LastName;


--Sales Employees

SELECT
	   e.EmployeeID,
	   e.FirstName,
	   e.LastName,
	   d.[Name]
  FROM Departments AS d
  JOIN Employees AS e ON d.DepartmentID = e.DepartmentID
 WHERE d.[Name] = 'Sales'
 ORDER BY e.EmployeeID;

 --Employee Departments

  SELECT TOP (5)
		 e.EmployeeID,
		 e.FirstName,
		 e.Salary,
		 d.[Name]
    FROM Employees AS e
    JOIN Departments AS d ON e.DepartmentID = d.DepartmentID
   WHERE e.Salary > 15000
ORDER BY e.DepartmentID;

--Employees Without Projects

   SELECT TOP (3)
	      e.EmployeeID,
	      e.FirstName
     FROM Employees AS e
LEFT JOIN EmployeesProjects AS ep 
	   ON e.EmployeeID = ep.EmployeeID
    WHERE ep.EmployeeID IS NULL
 ORDER BY e.EmployeeID;


--Employees Hired After

   SELECT 
		  e.FirstName,
		  e.LastName,
	 	  e.HireDate,
		  d.[Name] AS [DeptName]
     FROM Employees AS e
     JOIN Departments AS d 
	 ON (e.DepartmentID = d.DepartmentID 
	 AND e.HireDate > '1/1/1999' 
	 AND d.[Name] IN ('Sales', 'Finance'))
ORDER BY e.HireDate;

--Employees With Project

  SELECT TOP (5)
		 e.EmployeeID,
		 e.FirstName,
		 p.[Name] AS [ProjectName]
    FROM Employees AS e
    JOIN EmployeesProjects AS ep
      ON e.EmployeeID = ep.EmployeeID
    JOIN Projects AS p
      ON p.ProjectID = ep.ProjectID
   WHERE p.StartDate > '08/13/2002' AND p.EndDate IS NULL
ORDER BY e.EmployeeID;


--Employee 24

  SELECT 
		 e.EmployeeID,
		 e.FirstName,
		 CASE
			WHEN YEAR(p.StartDate) >= 2005 THEN NULL
			ELSE p.[Name]
		 END AS [ProjectName]
    FROM Employees AS e
    JOIN EmployeesProjects AS ep
      ON e.EmployeeID = ep.EmployeeID
    JOIN Projects AS p
      ON p.ProjectID = ep.ProjectID
   WHERE e.EmployeeID = 24;


--Employee Manager

SELECT 
	   e.EmployeeID,
	   e.FirstName,
	   e.ManagerID,
	   m.FirstName
  FROM Employees AS e
  JOIN Employees AS m ON e.ManagerID = m.EmployeeID
 WHERE e.ManagerID IN (3, 7)
ORDER BY e.EmployeeID;

--------------------

SELECT 
	   e.EmployeeID,
	   e.FirstName,
	   e.ManagerID,
	   m.FirstName
  FROM Employees AS e, Employees AS m
 WHERE e.ManagerID IN (3, 7) AND e.ManagerID = m.EmployeeID
ORDER BY e.EmployeeID;


--Employees Summary

  SELECT TOP (50)
		 e.EmployeeID,
		 CONCAT_WS(' ',e.FirstName, e.LastName) AS [EmployeeName],
		 CONCAT_WS(' ',m.FirstName, m.LastName) AS [ManagerName],
		 d.[Name] AS [DepartmentName]
    FROM Employees AS e
    JOIN Employees AS m
      ON e.ManagerID = m.EmployeeID
    JOIN Departments AS d
      ON e.DepartmentID = d.DepartmentID
ORDER BY e.EmployeeID;

--Min Average Salary

SELECT 
	   MIN(a.AvgSalaryByDep) AS MinAverageSalary
  FROM 
       (
		  SELECT 
				 AVG(Salary) AS AvgSalaryByDep
			FROM Employees
		GROUP BY DepartmentID
       ) AS a;

------------------

WITH AvrSalary AS
(
	SELECT 
		  AVG(Salary) AS AvgSalaryByDep
	 FROM Employees
 GROUP BY DepartmentID
)
SELECT MIN(AvgSalaryByDep) AS MinAverageSalary FROM AvrSalary;


--Part II � Queries for Geography Database

USE [Geography];
GO

--Highest Peaks in Bulgaria

   SELECT 
		  mc.CountryCode,
		  m.MountainRange,
		  p.PeakName,
		  p.Elevation
     FROM Mountains AS m
     JOIN MountainsCountries AS mc
       ON m.Id = mc.MountainId
     JOIN Peaks AS p
       ON p.MountainId = mc.MountainId
    WHERE mc.CountryCode = 'BG' AND p.Elevation > 2835
 ORDER BY p.Elevation DESC;

 --Count Mountain Ranges

 SELECT
		mc.CountryCode,
		COUNT(m.MountainRange) AS [MountainRanges]
   FROM Mountains AS m
   JOIN MountainsCountries AS mc
     ON m.Id = mc.MountainId
WHERE mc.CountryCode IN ('BG', 'US', 'RU')
GROUP BY mc.CountryCode;


--Countries With or Without Rivers

   SELECT TOP (5)
		  c.CountryName,
		  r.RiverName
     FROM Continents AS cont
     JOIN Countries AS c
	   ON c.ContinentCode = cont.ContinentCode
LEFT JOIN CountriesRivers AS cr
	   ON cr.CountryCode = c.CountryCode
LEFT JOIN Rivers AS r
	   ON r.Id = cr.RiverId
    WHERE cont.ContinentName = 'Africa'
 ORDER BY c.CountryName;


 --Continents and Currencies
 SELECT 
		ContinentCode,
		CurrencyCode,
		CurrencyUsage
   FROM 
   		(
			SELECT 
					*,
					DENSE_RANK() OVER (PARTITION BY ContinentCode ORDER BY CurrencyUsage DESC) AS CurrencyRank
			FROM 
					(
						SELECT ContinentCode,
								CurrencyCode,
								COUNT(*) AS CurrencyUsage
							FROM Countries
						GROUP BY ContinentCode, CurrencyCode
						HAVING COUNT(*) > 1
					)
				AS CurrencyUsageSub
		)
	AS CurrencyRankSub
 WHERE CurrencyRank = 1;

--Countries Without any Mountains

SELECT
	   COUNT(*) AS [Count]
  FROM MountainsCountries AS mc
 RIGHT JOIN Countries AS c
    ON mc.CountryCode = c.CountryCode
 WHERE mc.CountryCode IS NULL;


 --Highest Peak and Longest River by Country

   SELECT 
   TOP(5) c.CountryName,
		  MAX(p.Elevation) AS HighestPeakElevation,
		  MAX(r.[Length]) AS LongestRiverLength
     FROM Countries AS c
LEFT JOIN MountainsCountries AS mc
       ON c.CountryCode = mc.CountryCode
LEFT JOIN Peaks AS p
       ON p.MountainId = mc.MountainId
LEFT JOIN CountriesRivers AS cr 
       ON cr.CountryCode = c.CountryCode
LEFT JOIN Rivers AS r
       ON r.Id = cr.RiverId
 GROUP BY c.CountryName
 ORDER BY HighestPeakElevation DESC,
		  LongestRiverLength DESC,
		  c.CountryName;

--Highest Peak Name and Elevation by Country

 SELECT 
 TOP (5) CountryName AS [Country],
		 ISNULL(PeakName, '(no highest peak)') AS [Highest Peak Name],
		 ISNULL(Elevation, 0) AS [Highest Peak Elevation],
		 ISNULL(MountainRange, '(no mountain)') AS [Mountain]
   FROM
	    (
		    SELECT 
				   c.CountryName,
				   p.PeakName,
				   p.Elevation,
				   m.MountainRange,
				   DENSE_RANK() OVER (PARTITION BY c.CountryName ORDER BY p.Elevation DESC) AS PeakRank
				   FROM Countries AS c
		 LEFT JOIN MountainsCountries AS mc
			    ON c.CountryCode = mc.CountryCode
		 LEFT JOIN Mountains AS m
			    ON mc.MountainId = m.Id
		 LEFT JOIN Peaks AS p
			    ON mc.MountainId = p.MountainId
		 )
	  AS PeakRankSubquery
   WHERE PeakRank = 1
ORDER BY CountryName, PeakName;
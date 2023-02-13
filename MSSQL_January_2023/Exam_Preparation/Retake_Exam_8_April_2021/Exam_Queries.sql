--Section 1. DDL (30 pts)

CREATE DATABASE [Service]
GO

USE [Service]
GO

--Users

CREATE TABLE Users
(
	Id INT IDENTITY PRIMARY KEY,
	Username VARCHAR(30) NOT NULL UNIQUE,
	[Password] VARCHAR(50) NOT NULL,
	[Name] VARCHAR(50),
	Birthdate DATETIME,
	Age INT CHECK (Age BETWEEN 14 AND 110),
	Email VARCHAR(50) NOT NULL
);

--Departments

CREATE TABLE Departments
(
	Id INT IDENTITY PRIMARY KEY,
	[Name] VARCHAR(50) NOT NULL
);

--Employees

CREATE TABLE Employees
(
	Id INT IDENTITY PRIMARY KEY,
	FirstName VARCHAR(25),
	LastName VARCHAR(25),
	Birthdate DATETIME,
	Age INT CHECK (Age BETWEEN 18 AND 110),
	DepartmentId INT FOREIGN KEY REFERENCES Departments(Id)
);

--Categories

CREATE TABLE Categories
(
	Id INT IDENTITY PRIMARY KEY,
	[Name] VARCHAR(50),
	DepartmentId INT NOT NULL FOREIGN KEY REFERENCES Departments(Id)
);

--Status

CREATE TABLE [Status]
(
	Id INT IDENTITY PRIMARY KEY,
	[Label] VARCHAR(50) NOT NULL 
);

--Reports

CREATE TABLE Reports
(
	Id INT IDENTITY PRIMARY KEY,
	CategoryId INT NOT NULL FOREIGN KEY REFERENCES Categories(Id),
	StatusId INT NOT NULL FOREIGN KEY REFERENCES [Status](Id),
	OpenDate DATETIME NOT NULL,
	CloseDate DATETIME,
	[Description] VARCHAR(200) NOT NULL,
	UserId INT NOT NULL FOREIGN KEY REFERENCES Users(Id),
	EmployeeId INT FOREIGN KEY REFERENCES Employees(Id)
);


--Section 2. DML (10 pts)

--Insert

INSERT INTO Employees (FirstName, LastName, Birthdate, DepartmentId)
	 VALUES ('Marlo', 'O''Malley', '1958-9-21', 1),
			('Niki', 'Stanaghan', '1969-11-26', 4),
			('Ayrton', 'Senna', '1960-03-21', 9),
			('Ronnie', 'Peterson', '1944-02-14', 9),
			('Giovanna', 'Amati', '1959-07-20', 5);

INSERT INTO Reports (CategoryId, StatusId, OpenDate, CloseDate, [Description], UserId, EmployeeId)
	 VALUES (1, 1, '2017-04-13', NULL, 'Stuck Road on Str.133', 6, 2),
			(6, 3, '2015-09-05', '2015-12-06', 'Charity trail running', 3, 5),
			(14, 2, '2015-09-07', NULL, 'Falling bricks on Str.58', 5, 2),
			(4, 3, '2017-07-03', '2017-07-06', 'Cut off streetlight on Str.11', 1, 1);


--Update

UPDATE Reports
   SET CloseDate = GETDATE()
 WHERE CloseDate IS NULL;


 --Delete

 DELETE FROM Reports
       WHERE StatusId = 4


--Section 3. Querying (40 pts)

--Unassigned Reports

  SELECT
		 r.[Description],
		 FORMAT(r.OpenDate, 'dd-MM-yyyy') AS OpenDate
    FROM Reports AS r
   WHERE EmployeeId IS NULL
ORDER BY r.[OpenDate], r.[Description];


--Reports & Categories

  SELECT
		 r.[Description],
		 c.[Name] AS CategoryName
	FROM Reports AS r
	JOIN Categories AS c
	  ON r.CategoryId = c.Id
ORDER BY r.[Description], c.[Name];

--Most Reported Category

   SELECT TOP(5)
		  c.[Name],
	      COUNT(c.[Name]) AS ReportsNumber
	 FROM Categories AS c
     JOIN Reports AS r
	   ON c.Id = r.CategoryId
 GROUP BY c.[Name]
 ORDER BY ReportsNumber DESC, c.[Name];


--Birthday Report

  SELECT 
		 u.Username,
		 c.[Name] AS CategoryName
    FROM Users AS u
	JOIN Reports AS r
	  ON u.Id = r.UserId
	JOIN Categories AS c
	  ON r.CategoryId = c.Id
   WHERE DAY(u.Birthdate) = DAY(r.OpenDate) AND MONTH(u.Birthdate) = MONTH(r.OpenDate)
ORDER BY u.Username, c.[Name];

--User per Employee

   SELECT 
		  CONCAT_WS(' ', e.FirstName, e.LastName) AS FullName,
		  COUNT(u.Id) AS UsersCount
     FROM Employees AS e
LEFT JOIN Reports AS r
	   ON e.Id = r.EmployeeId
LEFT JOIN Users AS u
	   ON r.UserId = u.Id
 GROUP BY e.FirstName, e.LastName
 ORDER BY UsersCount DESC, FullName;


--Full Info

   SELECT
		  CASE
			   WHEN e.FirstName IS NULL THEN 'None'
			   ELSE CONCAT_WS(' ', e.FirstName, e.LastName)
		   END AS Employee,
		  ISNULL(d.[Name], 'None') AS Department,
		  ISNULL(c.[Name], 'None') AS Category,
		  ISNULL(r.[Description], 'None') AS [Description],
		  FORMAT(r.OpenDate, 'dd.MM.yyyy') AS OpneDate,
		  ISNULL(s.[Label], 'None') AS [Status],
		  ISNULL(u.[Name], 'None') AS [User]
	 FROM Reports AS r
LEFT JOIN Employees AS e
	   ON r.EmployeeId = e.Id
LEFT JOIN Categories AS c
	   ON r.CategoryId = c.Id
LEFT JOIN Departments AS d
	   ON e.DepartmentId = d.Id
LEFT JOIN [Status] AS s
	   ON r.StatusId = s.Id
LEFT JOIN Users AS u
	   ON r.UserId = u.Id
 ORDER BY e.FirstName DESC, e.LastName DESC, Department, Category, r.[Description], r.OpenDate, [Status], [User];


--Section 4. Programmability (20 pts)

--Hours to Complete
GO

CREATE FUNCTION udf_HoursToComplete(@StartDate DATETIME, @EndDate DATETIME)
	RETURNS INT
			 AS
	      BEGIN
				IF @StartDate IS NULL OR @EndDate IS NULL
				   BEGIN
						 RETURN 0;
					 END;

			    RETURN DATEDIFF(HOUR, @StartDate, @EndDate);

		    END;

GO

--Assign Employee

CREATE PROCEDURE usp_AssignEmployeeToReport(@EmployeeId INT, @ReportId INT)
			  AS
		   BEGIN
				 DECLARE @empDepartment INT = (
												SELECT
													   DepartmentId
												  FROM Employees
												 WHERE Id = @EmployeeId
										      );

				 DECLARE @reportDepartment INT = (
													 SELECT
															c.DepartmentId
													   FROM Reports AS r
													   JOIN Categories AS c
													     ON r.CategoryId = c.Id
													  WHERE r.Id = @ReportId
												 );

				  IF @empDepartment <> @reportDepartment
					 BEGIN
						   ;THROW 51000, 'Employee doesn''t belong to the appropriate department!', 1
					   END;

				  
				  UPDATE Reports
				     SET EmployeeId = @EmployeeId
				   WHERE Id = @ReportId;
		     END;
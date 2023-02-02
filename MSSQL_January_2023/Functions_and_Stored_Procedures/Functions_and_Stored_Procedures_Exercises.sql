--Part I � Queries for SoftUni Database

USE SoftUni;
GO

--Employees with Salary Above 35000

CREATE PROCEDURE usp_GetEmployeesSalaryAbove35000
	AS
 BEGIN
		SELECT
			   FirstName,
			   LastName
		  FROM Employees
		 WHERE Salary > 35000
   END;

GO

EXEC dbo.usp_GetEmployeesSalaryAbove35000;

GO
--Employees with Salary Above Number

CREATE PROC usp_GetEmployeesSalaryAboveNumber @Salary DECIMAL(18, 4)
	AS
	   SELECT 
			  FirstName,
			  LastName
	     FROM Employees
		WHERE Salary >= @Salary;

GO

EXEC dbo.usp_GetEmployeesSalaryAboveNumber 48100;

GO

--Town Names Starting With

CREATE PROC usp_GetTownsStartingWith @InputString NVARCHAR(MAX)
	AS
	   SELECT
			  [Name]
	     FROM Towns
		WHERE [Name] LIKE @InputString + '%';

GO

EXEC dbo.usp_GetTownsStartingWith 'b';

GO

--Employees from Town
CREATE PROC usp_GetEmployeesFromTown @TownName NVARCHAR(MAX)
	AS
	    SELECT 
			   e.FirstName,
			   e.LastName
		  FROM Employees AS e
		  JOIN Addresses AS a
			ON e.AddressID = a.AddressID
		  JOIN Towns AS t
			ON t.TownID = a.TownID
		 WHERE t.[Name] = @TownName;

GO

EXEC dbo.usp_GetEmployeesFromTown 'Kent';
GO


--Salary Level Function

CREATE FUNCTION ufn_GetSalaryLevel(@salary DECIMAL(18,4))
RETURNS NVARCHAR(MAX)
	AS
		BEGIN
			  DECLARE @salaryLevel NVARCHAR(MAX) = 'Average';

			  IF (@salary < 30000)
				 SET  @salaryLevel = 'Low'
			  ELSE IF (@salary > 50000)
				 SET @salaryLevel = 'High'

			  RETURN @salaryLevel;
		  END;

GO

SELECT
	   Salary,
	   dbo.ufn_GetSalaryLevel(Salary) AS [Salary Level]
  FROM Employees;


GO

--Employees by Salary Level

CREATE PROC usp_EmployeesBySalaryLevel @salaryLevel NVARCHAR(MAX)
	AS
	   SELECT
			  FirstName,
			  LastName
	     FROM Employees
		WHERE dbo.ufn_GetSalaryLevel(Salary) = @salaryLevel;

GO

EXEC usp_EmployeesBySalaryLevel 'high';

GO

--Define Function

CREATE OR ALTER FUNCTION ufn_IsWordComprised (@setOfLetters NVARCHAR(MAX), @word NVARCHAR(MAX))
RETURNS BIT
	 AS 
	    BEGIN
			   DECLARE @result BIT = 0;

			   IF (@word NOT LIKE CONCAT('%[^', @setOfLetters,']%'))
				  SET @result = 1;
			   
			   RETURN @result;  
		  END;

GO

SELECT dbo.ufn_IsWordComprised('oistmiahf', 'Sofia'); -- 1
SELECT dbo.ufn_IsWordComprised('oistmiahf', 'halves'); -- 0
SELECT dbo.ufn_IsWordComprised('bobr', 'Rob'); -- 1
SELECT dbo.ufn_IsWordComprised('pppp', 'Guy'); -- 0

GO

--*Delete Employees and Departments

CREATE PROC usp_DeleteEmployeesFromDepartment @departmentId INT
    AS
 BEGIN
		DECLARE @employeesToDel TABLE (Id INT)
		INSERT INTO @employeesToDel
					SELECT
						   EmployeeID
					  FROM Employees
					 WHERE DepartmentID = @departmentId;

		DELETE
		  FROM EmployeesProjects
		 WHERE EmployeeID IN (SELECT * FROM @employeesToDel);

		 ALTER TABLE Departments
		 ALTER COLUMN ManagerId INT;

		 UPDATE Departments
		    SET ManagerID = NULL
		  WHERE ManagerID IN (SELECT * FROM @employeesToDel);

		 UPDATE Employees
		    SET ManagerID = NULL
		  WHERE ManagerID IN (SELECT * FROM @employeesToDel);

		 DELETE
		   FROM Employees
		  WHERE DepartmentID = @departmentId;

		 DELETE
		   FROM Departments
		  WHERE DepartmentID = @departmentId;

		  SELECT
				 COUNT(*)
		    FROM Employees
		   WHERE DepartmentID = @departmentId;
   END;

GO

BEGIN TRANSACTION

EXEC dbo.usp_DeleteEmployeesFromDepartment 7;

ROLLBACK TRANSACTION

--Part II Queries for Bank Database

USE Bank;
GO

--Find Full Name

CREATE PROC usp_GetHoldersFullName
	AS
	   SELECT 
			  CONCAT_WS(' ', FirstName, LastName) AS [Full Name]
	     FROM AccountHolders;

GO

EXEC usp_GetHoldersFullName;

GO
--People with Balance Higher Than

CREATE PROC usp_GetHoldersWithBalanceHigherThan (@higerThan MONEY)
	AS
	   SELECT
			  ah.FirstName,
			  ah.LastName
	     FROM AccountHolders AS ah
		 JOIN
			  (
				  SELECT
			             a.AccountHolderId,
			             SUM(a.Balance) AS TotalMoney
	                FROM Accounts AS a
				GROUP BY a.AccountHolderId
				  HAVING SUM(a.Balance) > @higerThan
			  ) AS GroupedAccounts
		   ON ah.Id = AccountHolderId
	 ORDER BY ah.FirstName, ah.LastName;


GO

EXEC dbo.usp_GetHoldersWithBalanceHigherThan 550;

GO

--Future Value Function

CREATE FUNCTION ufn_CalculateFutureValue (@sum DECIMAL(18, 4), @yearlyInterestRate FLOAT, @years INT)
RETURNS DECIMAL(18, 4)
	 AS
	    BEGIN
				DECLARE @result DECIMAL(18, 4) = @sum * POWER(1 + @yearlyInterestRate, @years);

				RETURN @result;
		  END;

GO

SELECT dbo.ufn_CalculateFutureValue(1000, 0.1, 5) AS [Output]; -- 1610.5100

GO

--Calculating Interest

CREATE PROC usp_CalculateFutureValueForAccount (@accountId INT, @interestRate FLOAT)
	AS
	   SELECT
			  a.Id AS [Account Id],
			  ah.FirstName AS [First Name],
			  ah.LastName AS [Last Name],
			  a.Balance AS [Current Balance],
			  dbo.ufn_CalculateFutureValue(a.Balance, @interestRate, 5) AS [Balance in 5 years]
	     FROM AccountHolders AS ah
		 JOIN Accounts AS a
		   ON a.AccountHolderId = ah.Id
		WHERE a.Id = @accountId;

GO

EXEC dbo.usp_CalculateFutureValueForAccount 1, 0.1;

GO

--Part III � Queries for Diablo Database

USE Diablo;
GO

--*Cash in User Games Odd Rows

CREATE FUNCTION ufn_CashInUsersGames (@gameName NVARCHAR(MAX))
RETURNS TABLE
	AS  
	  RETURN 
		 SELECT
				SUM(Cash) AS SumCash
		   FROM 
				(
					SELECT 
						   ug.Cash,
            	           ROW_NUMBER() OVER (ORDER BY ug.Cash DESC) AS [Row]
		              FROM Games AS g
		              JOIN UsersGames AS ug
			            ON g.Id = ug.GameId
			         WHERE g.[Name] = @gameName
				) AS NumberedRows
		 WHERE [Row] % 2 <> 0;
	     
				  
GO

SELECT dbo.ufn_CashInUsersGames('Love in a mist');
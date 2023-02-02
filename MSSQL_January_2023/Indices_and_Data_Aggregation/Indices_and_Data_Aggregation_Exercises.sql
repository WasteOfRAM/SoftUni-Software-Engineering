--Part I � Queries for Gringotts Database

USE Gringotts
GO

--Records' Count

SELECT
	   COUNT(*) AS [Count]
  FROM WizzardDeposits;

--Longest Magic Wand

SELECT 
	   MAX(MagicWandSize) AS LongestMagicWand
  FROM WizzardDeposits;

--Longest Magic Wand per Deposit Groups

  SELECT 
		 w.DepositGroup,
	     MAX(w.MagicWandSize) AS LongestMagicWand
    FROM WizzardDeposits AS w
GROUP BY w.DepositGroup;

--Smallest Deposit Group per Magic Wand Size

  SELECT
 TOP (2) w.DepositGroup
    FROM WizzardDeposits AS w
GROUP BY w.DepositGroup
ORDER BY AVG(w.MagicWandSize);

--Deposits Sum

  SELECT 
		 w.DepositGroup,
	     SUM(w.DepositAmount) AS TotalSum
    FROM WizzardDeposits AS w
GROUP BY w.DepositGroup;

--Deposits Sum for Ollivander Family

  SELECT 
		 w.DepositGroup,
	     SUM(w.DepositAmount) AS TotalSum
    FROM WizzardDeposits AS w
GROUP BY w.DepositGroup, w.MagicWandCreator
  HAVING w.MagicWandCreator = 'Ollivander family';

--Deposits Filter

  SELECT 
		 w.DepositGroup,
	     SUM(w.DepositAmount) AS TotalSum
    FROM WizzardDeposits AS w
GROUP BY w.DepositGroup, w.MagicWandCreator
  HAVING w.MagicWandCreator = 'Ollivander family' AND SUM(w.DepositAmount) < 150000
ORDER BY TotalSum DESC;

--Deposit Charge

  SELECT
		 w.DepositGroup,
		 w.MagicWandCreator,
		 MIN(w.DepositCharge) AS MinDepositCharge
    FROM WizzardDeposits AS w
GROUP BY w.DepositGroup, w.MagicWandCreator
ORDER BY w.MagicWandCreator, w.DepositGroup;


--Age Groups
  SELECT
	     AgeGroup,
	     COUNT(*) AS WizardCount
    FROM
         (
	      SELECT
			     CASE
				      WHEN w.Age <= 10 THEN '[0-10]'
				      WHEN w.Age BETWEEN 11 AND 20 THEN '[11-20]'
				      WHEN w.Age BETWEEN 21 AND 30 THEN '[21-30]'
				      WHEN w.Age BETWEEN 31 AND 40 THEN '[31-40]'
				      WHEN w.Age BETWEEN 41 AND 50 THEN '[41-50]'
				      WHEN w.Age BETWEEN 51 AND 60 THEN '[51-60]'
				      ELSE '[61+]'
			      END AS AgeGroup
		    FROM WizzardDeposits AS w
		  ) AS AgeGroupSubQuery
GROUP BY AgeGroup;


--First Letter
  SELECT
		  FirstLetter
    FROM
	     (
	      SELECT
			     LEFT(w.FirstName, 1) AS FirstLetter
		    FROM WizzardDeposits AS w
		   WHERE w.DepositGroup = 'Troll Chest'
	     ) AS FirstLetterSub
GROUP BY FirstLetter
ORDER BY FirstLetter;


--Average Interest

  SELECT
         w.DepositGroup,
		 w.IsDepositExpired,
		 AVG(w.DepositInterest) AS AverageInterest
    FROM WizzardDeposits AS w
   WHERE w.DepositStartDate > '01/01/1985'
GROUP BY w.DepositGroup, w.IsDepositExpired
ORDER BY w.DepositGroup DESC, w.IsDepositExpired;


--*Rich Wizard, Poor Wizard
SELECT 
	   SUM(DepositDifference) AS SumDifference 
  FROM
      (
	    SELECT
	           DepositAmount - LEAD(DepositAmount) OVER (ORDER BY Id) AS DepositDifference
          FROM WizzardDeposits
	  ) AS DepositAmountDifference;
	  

--Part II � Queries for SoftUni Database

USE SoftUni;
GO

--Departments Total Salaries

  SELECT
		 e.DepartmentID,
		 SUM(Salary) AS TotalSalary
    FROM Employees as e
GROUP BY e.DepartmentID
ORDER BY e.DepartmentID;


--Employees Minimum Salaries

  SELECT
		 e.DepartmentID,
		 MIN(Salary) AS MinimumSalary
    FROM Employees as e
   WHERE e.DepartmentID IN (2, 5, 7) AND e.HireDate > '01/01/2000'
GROUP BY e.DepartmentID;


--Employees Average Salaries

SELECT
	   *
  INTO #EmployeesTemp
  FROM Employees
 WHERE Salary > 30000;

DELETE FROM #EmployeesTemp WHERE ManagerID = 42;

UPDATE #EmployeesTemp
   SET Salary += 5000
 WHERE DepartmentID = 1;

  SELECT
	     et.DepartmentID,
		 AVG(Salary) AS AverageSalary
    FROM #EmployeesTemp AS et
GROUP BY et.DepartmentID;


--Employees Maximum Salaries

  SELECT
		 e.DepartmentID,
		 MAX(e.Salary)
    FROM Employees AS e
GROUP BY e.DepartmentID
  HAVING MAX(e.Salary) NOT BETWEEN 30000 AND 70000;


--Employees Count Salaries

SELECT 
	   COUNT(*) AS [Count]
  FROM Employees
 WHERE ManagerID IS NULL;


 --*3rd Highest Salary

 SELECT DISTINCT
	    DepartmentID,
		Salary
   FROM (
           SELECT
		          e.DepartmentID,
		          e.Salary,
		          DENSE_RANK() OVER (PARTITION BY e.DepartmentID ORDER BY e.Salary DESC) AS SalaryRank
             FROM Employees AS e
	     ) AS RankedSalarys
   WHERE SalaryRank = 3;


--**Salary Challenge

SELECT TOP(10)
	   e.FirstName,
	   e.LastName,
	   e.DepartmentID
  FROM Employees AS e
  JOIN (
		  SELECT
				 e.DepartmentID,
				 AVG(e.Salary) AS DepAvgSalary
			FROM Employees AS e
		GROUP BY e.DepartmentID
	   ) AS AvgSalaryByDepartment
	ON e.DepartmentID = AvgSalaryByDepartment.DepartmentID
 WHERE e.Salary > AvgSalaryByDepartment.DepAvgSalary;
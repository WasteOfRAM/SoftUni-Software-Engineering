--Part I - Queries for Bank Database

USE Bank;
GO

--Create Table Logs

CREATE TABLE Logs 
(
	LogId INT IDENTITY PRIMARY KEY,
	AccountID INT NOT NULL FOREIGN KEY (AccountID) REFERENCES Accounts(Id),
	OldSum MONEY NOT NULL,
	NewSum MONEY NOT NULL
);

GO

CREATE OR ALTER TRIGGER tr_LogAccountSumChange
ON Accounts 
FOR UPDATE 
	AS 
		BEGIN
			INSERT INTO Logs
			SELECT
				   i.Id,
				   d.Balance,
				   i.Balance
			  FROM inserted AS i
			  JOIN deleted AS d
				ON i.Id = d.Id
		END;

GO

UPDATE Accounts
   SET Balance = 123.12
 WHERE Id = 1;

--Create Table Emails

CREATE TABLE NotificationEmails
(
	Id INT IDENTITY PRIMARY KEY,
	Recipient INT NOT NULL,
	[Subject] VARCHAR(MAX) NOT NULL,
	Body VARCHAR(MAX) NOT NULL
);

GO

CREATE TRIGGER tr_SentNotifications
ON Logs
FOR INSERT
	AS
	   BEGIN
			 DECLARE @AccountId INT = (SELECT TOP(1) AccountID FROM Logs ORDER BY LogId DESC);
			 DECLARE @old MONEY = (SELECT TOP(1) OldSum FROM Logs ORDER BY LogId DESC);
			 DECLARE @new MONEY = (SELECT TOP(1) NewSum FROM Logs ORDER BY LogId DESC);
			 DECLARE @subject VARCHAR(MAX) = CONCAT('Balance change for account: ', @AccountId);
			 DECLARE @body VARCHAR(MAX) = CONCAT('On ', FORMAT(GETDATE(), 'MMM d yyyy h:mmtt'),' your balance was changed from ', @old, ' to ',@new , '.');

			 INSERT INTO NotificationEmails
			 VALUES (@AccountId, @subject, @body);
	   END;


--Deposit Money
GO

CREATE PROCEDURE usp_DepositMoney(@AccountId INT, @MoneyAmount Money)
	AS
	   BEGIN
			 IF(@MoneyAmount > 0)
			  BEGIN
					UPDATE Accounts
					   SET Balance += FORMAT(@MoneyAmount, 'G')
					 WHERE Id = @AccountId;
				END
		 END;

GO

EXEC dbo.usp_DepositMoney 1, 10;
GO

--Withdraw Money Procedure

CREATE PROCEDURE usp_WithdrawMoney (@AccountId INT, @MoneyAmount MONEY)
	AS
	   BEGIN
			 IF(@MoneyAmount > 0)
			  BEGIN
					UPDATE Accounts
					   SET Balance -= @MoneyAmount
					 WHERE Id = @AccountId;
				END;
	     END;

GO

EXEC dbo.usp_WithdrawMoney 5, 25;

GO

--Money Transfer

CREATE PROCEDURE usp_TransferMoney(@SenderId INT, @ReceiverId INT, @Amount MONEY)
	AS
	   BEGIN
			 BEGIN TRANSACTION

			 DECLARE @SenderBalance MONEY = (SELECT Balance FROM Accounts WHERE Id = @SenderId);

			 IF @SenderBalance < @Amount
				BEGIN
					  ROLLBACK
					  ;THROW 51000, 'Insufficient amount', 1
					  RETURN
				  END;

			 EXEC dbo.usp_WithdrawMoney @SenderId, @Amount;
			 EXEC dbo.usp_DepositMoney @ReceiverId, @Amount;

			 COMMIT
	     END;

GO

--Part III - Queries for SoftUni Database

USE SoftUni;
GO

--Employees with Three Projects

CREATE PROCEDURE usp_AssignProject(@emloyeeId INT, @projectID INT)
	AS
       BEGIN
			  BEGIN TRANSACTION

			  IF ((SELECT COUNT(*) FROM EmployeesProjects WHERE EmployeeID = @emloyeeId) >= 3)
				BEGIN
					  ROLLBACK;
					  RAISERROR ('The employee has too many projects!', 16, 1);
					  RETURN
				  END;

			  INSERT INTO EmployeesProjects
			       VALUES (@emloyeeId, @projectID);

			  COMMIT;
	     END;

GO

--Delete Employees

CREATE TABLE Deleted_Employees
(
	EmployeeId INT IDENTITY PRIMARY KEY, 
	FirstName VARCHAR(50), 
	LastName VARCHAR(50), 
	MiddleName VARCHAR(50), 
	JobTitle VARCHAR(50), 
	DepartmentId INT, 
	Salary MONEY
);

GO

CREATE TRIGGER tr_Delete_Employee
ON Employees
FOR DELETE
AS
   BEGIN
		  INSERT INTO Deleted_Employees (FirstName, LastName, MiddleName, JobTitle, DepartmentID, Salary)
		       SELECT
					  FirstName,
					  LastName,
					  MiddleName,
					  JobTitle,
					  DepartmentID,
					  Salary
			     FROM deleted
     END;

GO


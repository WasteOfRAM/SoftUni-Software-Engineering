--Create Database

CREATE DATABASE Minions;
GO
USE Minions;

--Create Tables
CREATE TABLE Minions (
	Id INT NOT NULL,
	[Name] NVARCHAR(50) NOT NULL,
	Age INT NULL,
	CONSTRAINT PK_MinionsId PRIMARY KEY (Id)
);

CREATE TABLE Towns (
	Id INT NOT NULL,
	[Name] NVARCHAR(50) NOT NULL,
	CONSTRAINT PK_TownId PRIMARY KEY (Id)
);

--Alter Minions Table
ALTER TABLE Minions
ADD TownId INT;

ALTER TABLE Minions
ADD CONSTRAINT FK_TownId_Id FOREIGN KEY (TownId) REFERENCES Towns (Id);

--Insert Records in Both Tables
INSERT INTO Towns (Id, [Name])
VALUES (1, 'Sofia'),
	   (2, 'Plovdiv'),
	   (3, 'Varna');


INSERT INTO Minions (Id, [Name], Age, TownId)
VALUES (1, 'Kevin', 22, 1),
       (2, 'Bob', 12, 3),
	   (3, 'Steward', NULL, 2);

--Truncate Table Minions
TRUNCATE TABLE Minions;

--Drop All Tables
DROP TABLE Minions, Towns;

--Create Table People

CREATE TABLE People (
	Id INT NOT NULL IDENTITY(1, 1) PRIMARY KEY,
	[Name] NVARCHAR(200) NOT NULL,
	Picture VARBINARY(max) NULL,
	Height DECIMAL(3, 2) NULL,
	Weigth DECIMAL(5, 2) NULL,
	Gender NCHAR(1) NOT NULL,
	Birthdate DATE NOT NULL,
	Biography NVARCHAR(max) NULL
);

INSERT INTO People
VALUES ('Pesho', NULL, 1.70, 74.50, 'm', '1965-12-03', NULL),
	   ('Mira', NULL, 1.74, 53.5, 'f', '1999-04-08', 'Something'),
	   ('Ivan', NULL, 1.80, 80, 'm', '1977-01-03', 'What'),
	   ('Elena', NULL, 1.50, 100.25, 'f', '1909-12-03', '??'),
	   ('Pesho 2', NULL, 1.70, 74.50, 'm', '1965-12-03', NULL)
;

--Create Table Users

CREATE TABLE Users (
	Id INT NOT NULL IDENTITY PRIMARY KEY,
	Username VARCHAR(30) NOT NULL,
	[Password] VARCHAR(26) NOT NULL,
	ProfilePicture VARBINARY(max) NULL,
	LastLoginTIme DATETIME2 NULL,
	IsDeleted BIT NOT NULL DEFAULT 0
);

INSERT INTO Users (Username, [Password], IsDeleted)
VALUES ('Test', 'sdgsgseegs', 1),
       ('Test2', 'sdgsgseesdfggs', 0),
	   ('Test3', 'sdgsgsdgseegs', 1),
	   ('Test4', 'sdgssdggseegs', 0),
	   ('Test5', 'sdgsdgsgseegs', 1);


--Change Primary Key

ALTER TABLE Users
DROP CONSTRAINT PK__Users__3214EC07D7A12A74;

ALTER TABLE Users
ADD CONSTRAINT PK_IdUsername PRIMARY KEY (Id, Username);

--Add Check Constraint

ALTER TABLE Users
ADD CONSTRAINT CHK_PasswordLen CHECK(LEN([Password]) >= 5);

--Set Default Value of a Field

ALTER TABLE Users
ADD CONSTRAINT DF_LastLoginTime DEFAULT GETDATE() FOR LastLoginTIme;


INSERT INTO Users (Username, [Password], IsDeleted)
VALUES ('Date Default', 'sdgsgseegs343214', 0);

--Set Unique Field

ALTER TABLE Users
DROP CONSTRAINT PK_IdUsername;

ALTER TABLE Users
ADD CONSTRAINT PK_Id PRIMARY KEY (Id);

ALTER TABLE Users
ADD CONSTRAINT UC_Username UNIQUE (Username);

ALTER TABLE Users
ADD CONSTRAINT CHK_UsernameLength CHECK(LEN(Username) >= 3);

--Movies Database
CREATE DATABASE Movies
Go
USE Movies;

CREATE TABLE Directors 
(
	Id INT NOT NULL,
	DirectorName NVARCHAR(100) NOT NULL,
	Notes NVARCHAR(MAX),
	CONSTRAINT PK_DirectorId PRIMARY KEY (Id)
);

INSERT INTO Directors 
	(Id, DirectorName)
VALUES
	(1, 'Some Name'),
	(2, 'Some Other Name'),
	(3, 'Christopher Nolan'),
	(4, 'Michel Man'),
	(5, 'Dont Know');

CREATE TABLE Genres
(
	Id INT NOT NULL,
	GenreName NVARCHAR(20) NOT NULL,
	Notes NVARCHAR(MAX),
	CONSTRAINT PK_GenreId PRIMARY KEY (Id)
);

INSERT INTO Genres
	(Id, GenreName)
VALUES
	(1, 'Scifi'),
	(2, 'Action'),
	(3, 'Comedy'),
	(4, 'Horror'),
	(5, 'Drama');

CREATE TABLE Categories
(
	Id INT NOT NULL,
	CategoryName NVARCHAR(20) NOT NULL,
	Notes NVARCHAR(MAX),
	CONSTRAINT PK_CategoryId PRIMARY KEY (Id)
);

INSERT INTO Categories
	(Id, CategoryName)
VALUES
	(1, 'Is'),
	(2, 'This'),
	(3, 'Not'),
	(4, 'Same as'),
	(5, 'Genres?');

CREATE TABLE Movies 
(
	Id INT NOT NULL,
	Title NVARCHAR(100) NOT NULL,
	DirectorId INT NOT NULL,
	CopyrightYear DATETIME2 NOT NULL,
	[Length] DECIMAL(5,2),
	GenreId INT NOT NULL,
	CategoryId INT NOT NULL,
	Rating INT,
	Notes NVARCHAR(MAX),
	CONSTRAINT PK_MovieId PRIMARY KEY (Id),
	CONSTRAINT FK_DirectorId FOREIGN KEY (DirectorId) REFERENCES Directors(Id),
	CONSTRAINT FK_GenreId FOREIGN KEY (GenreId) REFERENCES Genres(Id),
	CONSTRAINT FK_CategoryId FOREIGN KEY (CategoryId) REFERENCES Categories(Id)
)

INSERT INTO Movies
	(Id, Title, DirectorId, CopyrightYear, GenreId, CategoryId)
VALUES
	(1, 'Interstelar', 3, '2014-02-02', 1, 5),
	(2, 'HEAT', 4, '1995-02-02', 2, 4),
	(3, 'Dont', 1, '1520-02-02', 3, 5),
	(4, 'Care', 5, '1520-02-02', 2, 2),
	(5, 'Now',2 , '1520-02-02', 4, 4);

--Car Rental Database

CREATE DATABASE CarRental
GO
USE CarRental;

CREATE TABLE Categories 
(
	Id INT CONSTRAINT PK_CategoriesId PRIMARY KEY,
	CategoryName NVARCHAR(20) NOT NULL,
	DailyRate DECIMAL(10,2) NOT NULL,
	WeeklyRate DECIMAL(10,2) NOT NULL,
	MonthlyRate DECIMAL(10,2) NOT NULL,
	WeekendRate DECIMAL(10,2) NOT NULL
)

INSERT INTO Categories
VALUES
	(1, 'Economy', 50.00, 300.00, 2000.00, 125.00),
	(2, 'Delux', 100.00, 500.00, 3000.00, 250.00),
	(3, 'More Delux', 300.00, 1000.00, 7000.00, 500.00);

CREATE TABLE Cars
(
	Id INT CONSTRAINT PK_CarsId PRIMARY KEY,
	PlateNumber NVARCHAR(7) NOT NULL,
	Manufacturer NVARCHAR(10) NOT NULL,
	Model NVARCHAR(10) NOT NULL,
	CarYear DATETIME2,
	CategoryId INT CONSTRAINT FK_CategoryId FOREIGN KEY REFERENCES Categories(Id),
	Doors TINYINT NOT NULL CONSTRAINT DF_CarsDoors DEFAULT 4,
	Picture VARBINARY(MAX) CONSTRAINT CHK_FileSize CHECK(LEN(Picture) >= 2000000),
	Condition NVARCHAR(10),
	Available BIT CONSTRAINT DF_CarsAvaliable DEFAULT 1
)

INSERT INTO Cars
	(Id, PlateNumber, Manufacturer, Model, CategoryId, Doors, Available)
VALUES
	(1, '3525', 'Honda', 'Civic', 1, 3, 1),
	(2, '35466', 'BMW', '7', 2, 4, 0),
	(3, '354664', 'Boing', '777', 3, 2, 1);

CREATE TABLE Employees
(
	Id INT CONSTRAINT PK_EmployeeId PRIMARY KEY,
	FirstName NVARCHAR(100) NOT NULL,
	LastName NVARCHAR(100) NOT NULL,
	Title NVARCHAR(15),
	Notes NVARCHAR(MAX)
);

INSERT INTO Employees
	(Id, FirstName, LastName)
VALUES
	(1, 'Human', 'Person'),
	(2, 'Georgi', 'Petkov'),
	(3, 'REDACTED', 'REDACTED');

CREATE TABLE Customers
(
	Id INT CONSTRAINT PK_CustomerId PRIMARY KEY,
	DriverLicenceNumber INT NOT NULL,
	FullName NVARCHAR(100) NOT NULL,
	[Address] NVARCHAR(200) NOT NULL,
	City NVARCHAR(20),
	ZIPCode NVARCHAR(10),
	Notes NVARCHAR(MAX)
);

INSERT INTO Customers
	(Id, DriverLicenceNumber, FullName, [Address])
VALUES
	(1, 54231, 'Regular Human', 'The Woods'),
	(2, 7891, 'Georgi Petkov', 'Here'),
	(3, 879621, 'REDACTED', 'REDACTED');


CREATE TABLE RentalOrders
(
	Id INT CONSTRAINT PK_RentalOrderId PRIMARY KEY,
	EmployeeId INT NOT NULL CONSTRAINT FK_EmployeeId FOREIGN KEY REFERENCES Employees(Id),
	CustomerId INT NOT NULL CONSTRAINT FK_CustomerId FOREIGN KEY REFERENCES Customers(Id),
	CarId INT NOT NULL CONSTRAINT FK_CarId FOREIGN KEY REFERENCES Cars(Id),
	TankLevel DECIMAL,
	KilometrageStart DECIMAL,
	KilometrageEnd DECIMAL,
	TotalKilometrage DECIMAL,
	StartDate DATETIME2,
	EndDate DATETIME2,
	TotalDays INT,
	RateApplied NVARCHAR(10),
	TaxRate DECIMAL,
	OrderStatus NVARCHAR(20),
	Notes NVARCHAR(MAX)
)

INSERT INTO RentalOrders
	(Id, EmployeeId, CustomerId, CarId)
VALUES
	(1, 1, 3, 2),
	(2, 2, 2, 1),
	(3, 3, 1, 3);


--Hotel Database

CREATE DATABASE Hotel
GO
USE Hotel

CREATE TABLE Employees
(
	Id INT NOT NULL CONSTRAINT PK_EmployeeId PRIMARY KEY,
	FirstName NVARCHAR(100) NOT NULL,
	LastName NVARCHAR(100) NOT NULL,
	Title NVARCHAR(20),
	Notes NVARCHAR(MAX)
);

INSERT INTO Employees (Id, FirstName, LastName)
	VALUES (1, 'Name', 'LName'),
		   (2, '2Name', '2LName'),
		   (3, 'Petur', 'Petrov');

CREATE TABLE Customers
(
	AccountNumber INT NOT NULL CONSTRAINT PK_CustomerAN PRIMARY KEY,
	FirstName NVARCHAR(100) NOT NULL,
	LastName NVARCHAR(100) NOT NULL,
	PhoneNumber NVARCHAR(10) NOT NULL,
	EmergencyName NVARCHAR(100),
	EmergencyNumber NVARCHAR(10),
	Notes NVARCHAR(MAX)
)

INSERT INTO Customers (AccountNumber, FirstName, LastName, PhoneNumber)
	VALUES (1, 'Name', 'LName', '8564654'),
		   (2, '2Name', '2LName', '65462'),
		   (3, 'Petur', 'Petrov', '65468');

CREATE TABLE RoomStatus
(
	RoomStatus NVARCHAR(20) NOT NULL CONSTRAINT PK_RoomStatus PRIMARY KEY,
	Notes NVARCHAR(MAX)
);

INSERT INTO RoomStatus (RoomStatus)
	VALUES ('Free'),
		   ('Occupied'),
		   ('Maintenance');


CREATE TABLE RoomTypes
(
	RoomType NVARCHAR(20) NOT NULL CONSTRAINT PK_RoomType PRIMARY KEY,
	Notes NVARCHAR(MAX)
);

INSERT INTO RoomTypes (RoomType)
	VALUES ('Small'),
		   ('Normal'),
		   ('Apartment');

CREATE TABLE BedTypes 
(
	BedType NVARCHAR(10) NOT NULL CONSTRAINT PK_BedType PRIMARY KEY,
	Notes NVARCHAR(MAX)
)

INSERT INTO BedTypes (BedType)
	VALUES ('Single'),
		   ('Double'),
		   ('King');


CREATE TABLE Rooms
(
	RoomNumber INT NOT NULL CONSTRAINT PK_RoomNumber PRIMARY KEY,
	RoomType NVARCHAR(20) NOT NULL CONSTRAINT FK_RoomType FOREIGN KEY REFERENCES RoomTypes(RoomType),
	BedType NVARCHAR(10) NOT NULL CONSTRAINT FK_BedType FOREIGN KEY REFERENCES BedTypes(BedType),
	Rate DECIMAL(10,2) NOT NULL,
	RoomStatus NVARCHAR(20) NOT NULL CONSTRAINT FK_RoomStatus FOREIGN KEY REFERENCES RoomStatus(RoomStatus),
	Notes NVARCHAR(MAX)
);

INSERT INTO Rooms (RoomNumber, RoomType, BedType, Rate, RoomStatus)
	VALUES (101, 'Small', 'Single', 20.00, 'Maintenance'),
		   (102, 'Normal', 'Double', 37.00, 'Occupied'),
		   (103, 'Apartment', 'King', 100.00, 'Free');


CREATE TABLE Payments
(
	Id INT NOT NULL CONSTRAINT PK_PaymentId PRIMARY KEY,
	EmployeeId INT NOT NULL CONSTRAINT FK_EmployeeId FOREIGN KEY REFERENCES Employees(Id),
	PaymentDate DATETIME2,
	AccountNumber INT NOT NULL CONSTRAINT FK_AccountNumber FOREIGN KEY REFERENCES Customers(AccountNumber),
	FirstDateOccupied DATETIME2,
	LastDateOccupied DATETIME2,
	TotalDays SMALLINT,
	AmountCharged DECIMAL,
	TaxRate DECIMAL,
	TaxAmount DECIMAL,
	PaymentTotal DECIMAL,
	Notes NVARCHAR(MAX)
);

INSERT INTO Payments (Id, EmployeeId, AccountNumber, TaxRate)
	VALUES (1, 3, 3, 20),
		   (2, 1, 1, 30),
		   (3, 2, 2, 40);


CREATE TABLE Occupancies
(
	Id INT NOT NULL CONSTRAINT PK_OccupancieId PRIMARY KEY,
	EmployeeId INT NOT NULL CONSTRAINT FK_EmployeeId2 FOREIGN KEY REFERENCES Employees(Id),
	DateOccupied DATETIME2,
	AccountNumber INT NOT NULL CONSTRAINT FK_AccountNumber2 FOREIGN KEY REFERENCES Customers(AccountNumber),
	RoomNumber INT NOT NULL CONSTRAINT FK_RoomNumber FOREIGN KEY REFERENCES Rooms(RoomNumber),
	RateApplied DECIMAL,
	PhoneCharge DECIMAL,
	Notes NVARCHAR(MAX)
)

INSERT INTO Occupancies (Id, EmployeeId, AccountNumber, RoomNumber)
	VALUES (1, 3, 3, 101),
		   (2, 1, 1, 102),
		   (3, 2, 2, 103);


--Create SoftUni Database

CREATE DATABASE SoftUni;
GO

USE SoftUni;

CREATE TABLE Towns
(
	Id INT IDENTITY(1, 1) CONSTRAINT PK_TownsId PRIMARY KEY,
	[Name] NVARCHAR(100) NOT NULL
);

CREATE TABLE Addresses
(
	Id INT IDENTITY(1, 1) CONSTRAINT PK_AddressesId PRIMARY KEY,
	AddressText NVARCHAR(MAX) NOT NULL,
	TownId INT NOT NULL CONSTRAINT FK_TownId FOREIGN KEY REFERENCES Towns(Id)
);

CREATE TABLE Departments
(
	Id INT IDENTITY(1, 1) CONSTRAINT PK_DepartmentId PRIMARY KEY,
	[Name] NVARCHAR(MAX) NOT NULL
)

CREATE TABLE Employees
(
	Id INT IDENTITY(1, 1) CONSTRAINT PK_EmployeeId PRIMARY KEY,
	FirstName NVARCHAR(100) NOT NULL,
	MiddleName NVARCHAR(100),
	LastName NVARCHAR(100) NOT NULL,
	JobTitle NVARCHAR(100) NOT NULL,
	DepartmentId INT NOT NULL CONSTRAINT FK_DepartmentId FOREIGN KEY REFERENCES Departments(Id),
	HireDate DATETIME2,
	Salary DECIMAL NOT NULL,
	AddressId INT CONSTRAINT FK_AddressId FOREIGN KEY REFERENCES Addresses(Id)
)


--Backup Database


--Basic Insert

INSERT INTO Towns
	VALUES ('Sofia'),
	       ('Plovdiv'),
		   ('Varna'),
		   ('Burgas')

INSERT INTO Departments
	VALUES ('Engineering'),
	       ('Sales'),
		   ('Marketing'),
		   ('Software Development'),
		   ('Quality Assurance');

INSERT INTO Employees (FirstName, MiddleName, LastName, JobTitle, DepartmentId, HireDate, Salary)
	VALUES ('Ivan', 'Ivanov', 'Ivanov', '.NET Developer', 4, '02-01-2013', 3500.00),
	       ('Petar', 'Petrov', 'Petrov', 'Senior Engineer', 1, '03-02-2004', 4000.00),
		   ('Maria', 'Petrova', 'Ivanova', 'Intern', 5, '08-28-2016', 525.25),
		   ('Georgi', 'Teziev', 'Ivanov', '.CEO', 2, '12-09-2007', 3000.00),
		   ('Peter', 'Pan', 'Pan', 'Intern', 3, '08-28-2016', 599.88);

--Basic Select All Fields

SELECT * FROM Towns;
SELECT * FROM Departments;
SELECT * FROM Employees;

--Basic Select All Fields and Order Them

SELECT * FROM Towns ORDER BY [Name];
SELECT * FROM Departments ORDER BY [Name];
SELECT * FROM Employees ORDER BY Salary DESC;

--Basic Select Some Fields

SELECT [Name] FROM Towns ORDER BY [Name];
SELECT [Name] FROM Departments ORDER BY [Name];
SELECT FirstName, LastName, JobTitle, Salary FROM Employees ORDER BY Salary DESC;

--Increase Employees Salary

UPDATE Employees
SET Salary = Salary * 1.10;

SELECT Salary FROM Employees;

--Decrease Tax Rate

USE Hotel;

UPDATE Payments
SET TaxRate = TaxRate * 0.97;

SELECT TaxRate FROM Payments;

--Delete All Records
USE Hotel;
TRUNCATE TABLE Occupancies;
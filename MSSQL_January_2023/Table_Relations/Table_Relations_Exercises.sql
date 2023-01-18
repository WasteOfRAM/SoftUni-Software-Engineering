CREATE DATABASE TableRelations;
GO

USE TableRelations;

-- One-To-One Relationship

CREATE TABLE Passports 
(
    PassportID INT NOT NULL CONSTRAINT PK_PassportID PRIMARY KEY,
    PassportNumber NVARCHAR(8)
);

INSERT INTO Passports
VALUES (101, 'N34FG21B'),
       (102, 'K65LO4R7'),
       (103, 'ZE657QP2');


CREATE TABLE Persons
(
    PersonID INT IDENTITY CONSTRAINT PK_PersonID PRIMARY KEY,
    FirstName NVARCHAR(50) NOT NULL,
    Salary DECIMAL NOT NULL,
    PassportID INT CONSTRAINT FK_PassportID FOREIGN KEY REFERENCES Passports(PassportID)
);

INSERT INTO Persons
VALUES ('Roberto', 43300.00, 102),
       ('Tom', 56100.00, 103),
       ('Yana', 60200.00, 101);


-- One-To-Many Relationship

CREATE TABLE Models
(
    ModelID INT IDENTITY(101, 1) CONSTRAINT PK_ModelID PRIMARY KEY,
    [Name] NVARCHAR(50) NOT NULL,
    ManufacturerID INT NOT NULL
)

INSERT INTO Models
VALUES ('X1', 1),
       ('i6', 1),
       ('Model S', 2),
       ('Model X', 2),
       ('Model 3', 2),
       ('Nova', 3);


CREATE TABLE Manufacturers
(
    ManufacturerID INT IDENTITY CONSTRAINT PK_ManufacturerID PRIMARY KEY,
    [Name] NVARCHAR(50) NOT NULL,
    EstablishedOn DATE NOT NULL
)

INSERT INTO Manufacturers
VALUES ('BMW', '07/03/1916'),
       ('Tesla', '01/01/2003'),
       ('Lada', '01/05/1966');

ALTER TABLE Models
ADD CONSTRAINT FK_ManufacturerID FOREIGN KEY (ManufacturerID) REFERENCES Manufacturers(ManufacturerID);


-- Many-To-Many Relationship

CREATE TABLE Students
(
    StudentID INT IDENTITY CONSTRAINT PK_StudentID PRIMARY KEY,
    [Name] NVARCHAR(50) NOT NULL
)

INSERT INTO Students
     VALUES ('Mila'),
            ('Toni'),
            ('Ron');

CREATE TABLE Exams
(
    ExamID INT IDENTITY(101, 1) CONSTRAINT PK_ExamID PRIMARY KEY,
    [Name] NVARCHAR(50) NOT NULL
)

INSERT INTO Exams
     VALUES ('SpringMVC'),
            ('Neo4j'),
            ('Oracle 11g');


CREATE TABLE StudentsExams
(
    StudentID INT NOT NULL CONSTRAINT FK_StudentID FOREIGN KEY REFERENCES Students(StudentID),
    ExamID INT NOT NULL CONSTRAINT FK_ExamID FOREIGN KEY REFERENCES Exams(ExamID),
    CONSTRAINT PK_StudentExamID PRIMARY KEY (StudentID, ExamID)
)


-- Self-Referencing

CREATE TABLE Teachers
(
    TeacherID INT IDENTITY(101, 1) CONSTRAINT PK_TeacherID PRIMARY KEY,
    [Name] NVARCHAR(50) NOT NULL,
    ManagerID INT CONSTRAINT FK_TeacherID FOREIGN KEY REFERENCES Teachers(TeacherID)
)

INSERT INTO Teachers
     VALUES ('John', NULL),
            ('Maya', 106),
            ('Silvia', 106),
            ('Ted', 105),
            ('Mark', 101),
            ('Greta', 101);


-- Online Store Database

CREATE DATABASE OnlineStore;
GO

USE OnlineStore;

CREATE TABLE ItemTypes
(
	ItemTypeID INT IDENTITY CONSTRAINT PK_ItemTypeID PRIMARY KEY,
	[Name] NVARCHAR(50) NOT NULL
);

CREATE TABLE Items
(
	ItemID INT IDENTITY CONSTRAINT PK_ItemID PRIMARY KEY,
	[Name] NVARCHAR(50) NOT NULL,
	ItemTypeID INT CONSTRAINT FK_ItemTypeID FOREIGN KEY REFERENCES ItemTypes(ItemTypeID)
);

CREATE TABLE Cities
(
	CityID INT IDENTITY CONSTRAINT PK_CityID PRIMARY KEY,
	[Name] NVARCHAR(50) NOT NULL
);

CREATE TABLE Customers
(
	CustomerID INT IDENTITY CONSTRAINT PK_CustomerID PRIMARY KEY,
	[Name] NVARCHAR(50) NOT NULL,
	Birthday DATE,
	CityID INT CONSTRAINT FK_CityID FOREIGN KEY REFERENCES Cities(CityID)
);

CREATE TABLE Orders
(
	OrderID INT IDENTITY CONSTRAINT PK_OrderID PRIMARY KEY,
	CustomerID INT CONSTRAINT FK_CustomerID FOREIGN KEY REFERENCES Customers(CustomerID)
);

CREATE TABLE OrderItems
(
	OrderID INT NOT NULL CONSTRAINT FK_OrderID FOREIGN KEY REFERENCES Orders(OrderID),
	ItemID INT NOT NULL CONSTRAINT FK_ItemID FOREIGN KEY REFERENCES Items(ItemID),
	CONSTRAINT PK_OrderItemID PRIMARY KEY (OrderID, ItemID)
);


--University Database

CREATE DATABASE UniData;
GO

USE UniData;

CREATE TABLE Majors
(
	MajorID INT IDENTITY PRIMARY KEY,
	[Name] NVARCHAR(50) NOT NULL
);

CREATE TABLE Students
(
	StudentID INT IDENTITY PRIMARY KEY,
	StudentNumber INT NOT NULL,
	StudentName NVARCHAR(50) NOT NULL,
	MajorID INT NOT NULL FOREIGN KEY REFERENCES Majors(MajorID)
);

CREATE TABLE Payments
(
	PaymentID INT IDENTITY PRIMARY KEY,
	PaymentDate DATETIME2 NOT NULL,
	PaymentAmount DECIMAL NOT NULL,
	StudentID INT FOREIGN KEY REFERENCES Students(StudentID)
);

CREATE TABLE Subjects
(
	SubjectID INT IDENTITY PRIMARY KEY,
	SubjectName NVARCHAR(50) NOT NULL
);

CREATE TABLE Agenda
(
	StudentID INT FOREIGN KEY REFERENCES Students(StudentID),
	SubjectID INT FOREIGN KEY REFERENCES Subjects(SubjectID),
	CONSTRAINT PK_StudentSubjectID PRIMARY KEY (StudentID, SubjectID)
);


--*Peaks in Rila

USE [Geography];
GO

  SELECT m.MountainRange, p.PeakName, p.Elevation 
    FROM Mountains AS m 
    JOIN Peaks AS p ON m.Id = p.MountainId
   WHERE m.MountainRange = 'Rila'
ORDER BY p.Elevation DESC;
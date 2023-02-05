--Section 1. DDL (30 pts)

CREATE DATABASE Zoo;
GO

USE Zoo;
GO

--Section 1. DDL (30 pts)

--Owners

CREATE TABLE Owners
(
	Id INT IDENTITY PRIMARY KEY,
	Name VARCHAR(50) NOT NULL,
	PhoneNumber VARCHAR(15) NOT NULL,
	Address VARCHAR(50)
);

--AnimalTypes

CREATE TABLE AnimalTypes
(
	Id INT IDENTITY PRIMARY KEY,
	AnimalType VARCHAR(30) NOT NULL
);

--Cages

CREATE TABLE Cages
(
	Id INT IDENTITY PRIMARY KEY,
	AnimalTypeId INT NOT NULL FOREIGN KEY REFERENCES AnimalTypes(Id)
);

--Animals

CREATE TABLE Animals
(
	Id INT IDENTITY PRIMARY KEY,
	Name VARCHAR(30) NOT NULL,
	BirthDate DATE NOT NULL,
	OwnerId INT FOREIGN KEY REFERENCES Owners(id),
	AnimalTypeId INT NOT NULL FOREIGN KEY REFERENCES AnimalTypes(Id)
);

--AnimalsCages

CREATE TABLE AnimalsCages
(
	CageId INT NOT NULL FOREIGN KEY REFERENCES Cages(Id),
	AnimalId INT NOT NULL FOREIGN KEY REFERENCES Animals(Id),
	PRIMARY KEY (CageId, AnimalId)
);

--VolunteersDepartments

CREATE TABLE VolunteersDepartments
(
	Id INT IDENTITY PRIMARY KEY,
	DepartmentName VARCHAR(30) NOT NULL
);

--Volunteers

CREATE TABLE Volunteers
(
	Id INT IDENTITY PRIMARY KEY,
	Name VARCHAR(50) NOT NULL,
	PhoneNumber VARCHAR(15) NOT NULL,
	Address VARCHAR(50),
	AnimalId INT FOREIGN KEY REFERENCES Animals(Id),
	DepartmentId INT NOT NULL FOREIGN KEY REFERENCES VolunteersDepartments(Id)
);


--Section 2. DML (10 pts)

--Insert

INSERT INTO Volunteers (Name, PhoneNumber, Address, AnimalId, DepartmentId)
	 VALUES ('Anita Kostova', '0896365412', 'Sofia, 5 Rosa str.', 15, 1),
			('Dimitur Stoev', '0877564223', NULL, 42, 4),
			('Kalina Evtimova', '0896321112', 'Silistra, 21 Breza str.', 9, 7),
			('Stoyan Tomov', '0898564100', 'Montana, 1 Bor str.', 18, 8),
			('Boryana Mileva', '0888112233', NULL, 31, 5);

INSERT INTO Animals (Name, BirthDate, OwnerId, AnimalTypeId)
	 VALUES ('Giraffe', '2018-09-21', 21, 1),
			('Harpy Eagle', '2015-04-17', 15, 3),
			('Hamadryas Baboon', '2017-11-02', NULL, 1),
			('Tuatara', '2021-06-30', 2, 4);


--Update

UPDATE Animals
   SET OwnerId = (SELECT Id FROM Owners WHERE Name = 'Kaloqn Stoqnov')
 WHERE OwnerId IS NULL;

--Delete

DELETE FROM Volunteers
	  WHERE DepartmentId = 2;

DELETE FROM VolunteersDepartments
	  WHERE DepartmentName = 'Education program assistant';


--Section 3. Querying (40 pts)

--Volunteers

  SELECT
		 Name,
		 PhoneNumber,
		 Address,
		 AnimalId,
		 DepartmentId
	FROM Volunteers
ORDER BY Name, AnimalId, DepartmentId;


--Animals data

  SELECT 
	     a.Name,
		 t.AnimalType,
		 FORMAT(a.BirthDate, 'dd.MM.yyyy') AS BirthDate
    FROM Animals AS a
    JOIN AnimalTypes AS t
      ON a.AnimalTypeId = t.Id
ORDER BY a.Name;


--Owners and Their Animals

  SELECT TOP (5)
		 o.Name AS Owner,
		 COUNT(a.OwnerId) AS CountOfAnimals
	FROM Owners AS o
	JOIN Animals AS a
	  ON o.Id = a.OwnerId
GROUP BY o.Name, a.OwnerId
ORDER BY CountOfAnimals DESC, o.Name;

--Owners, Animals and Cages

  SELECT
		 CONCAT_WS('-', o.Name, a.Name) AS OwnersAnimals,
		 o.PhoneNumber,
		 ac.CageId
    FROM Owners AS o
	JOIN Animals AS a
	  ON o.Id = a.OwnerId
	JOIN AnimalsCages AS ac
	  ON a.Id = ac.AnimalId
	JOIN AnimalTypes AS atp
	  ON a.AnimalTypeId = atp.Id
   WHERE atp.AnimalType = 'Mammals'
ORDER BY o.Name, a.Name DESC;


--Volunteers in Sofia

  SELECT
	     v.Name,
		 v.PhoneNumber,
		 SUBSTRING(v.Address, CHARINDEX(',', v.Address) + 2, LEN(v.Address)) AS Address
    FROM Volunteers AS v
    JOIN VolunteersDepartments AS vd
      ON v.DepartmentId = vd.Id
   WHERE vd.DepartmentName = 'Education program assistant' AND LEFT(TRIM(v.Address), 5) = 'Sofia'
ORDER BY v.Name;


--Animals for Adoption

  SELECT
		 a.Name,
		 YEAR(a.BirthDate) AS BirthYear,
		 atp.AnimalType
	FROM Animals AS a
	JOIN AnimalTypes AS atp
	  ON a.AnimalTypeId = atp.Id
   WHERE atp.AnimalType <> 'Birds' AND DATEDIFF(YEAR, a.BirthDate, '01/01/2022') < 5 AND a.OwnerId IS NULL
ORDER BY a.Name;

--Section 4. Programmability (20 pts)

--All Volunteers in a Department

GO

CREATE FUNCTION udf_GetVolunteersCountFromADepartment (@VolunteersDepartment VARCHAR(30))
RETURNS INT
	 AS
		BEGIN 
			  RETURN (
					   SELECT
							  COUNT(vd.DepartmentName)
						 FROM VolunteersDepartments AS vd
						 JOIN Volunteers AS v
						   ON vd.Id = v.DepartmentId
						WHERE vd.DepartmentName = @VolunteersDepartment
					 GROUP BY vd.DepartmentName
					);
		  END;

GO


--Animals with Owner or Not

CREATE PROCEDURE usp_AnimalsWithOwnersOrNot(@AnimalName VARCHAR(30))
	AS
	   BEGIN
			 SELECT
					a.Name,
					ISNULL(o.Name, 'For adoption') AS OwnersName
			   FROM Animals AS a
		  LEFT JOIN Owners AS o
			     ON a.OwnerId = o.Id
			  WHERE a.Name = @AnimalName
		 END;
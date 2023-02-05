--Section 1. DDL (30 pts)

CREATE DATABASE NationalTouristSitesOfBulgaria;
GO

USE NationalTouristSitesOfBulgaria;
GO

--Categories

CREATE TABLE Categories
(
	Id INT IDENTITY PRIMARY KEY,
	[Name] VARCHAR(50) NOT NULL
);

--Locations

CREATE TABLE Locations
(
	Id INT IDENTITY PRIMARY KEY,
	[Name] VARCHAR(50) NOT NULL,
	Municipality VARCHAR(50),
	Province VARCHAR(50)
);

--Sites

CREATE TABLE Sites
(
	Id INT IDENTITY PRIMARY KEY,
	[Name] VARCHAR(100) NOT NULL,
	LocationId INT NOT NULL FOREIGN KEY REFERENCES Locations(Id),
	CategoryId INT NOT NULL FOREIGN KEY REFERENCES Categories(Id),
	Establishment VARCHAR(15)
);

--Tourists

CREATE TABLE Tourists
(
	Id INT IDENTITY PRIMARY KEY,
	[Name] VARCHAR(50) NOT NULL,
	Age INT NOT NULL CHECK (Age >= 0 AND Age <= 120),
	PhoneNumber VARCHAR(20) NOT NULL,
	Nationality VARCHAR(30) NOT NULL,
	Reward VARCHAR(20)
);

--SitesTourists

CREATE TABLE SitesTourists
(
	TouristId INT NOT NULL FOREIGN KEY REFERENCES Tourists(Id),
	SiteId INT NOT NULL FOREIGN KEY REFERENCES Sites(Id),
	PRIMARY KEY (TouristId, SiteId)
);

--BonusPrizes

CREATE TABLE BonusPrizes
(
	Id INT IDENTITY PRIMARY KEY,
	[Name] VARCHAR(50) NOT NULL
);

--TouristsBonusPrizes

CREATE TABLE TouristsBonusPrizes
(
	TouristId INT NOT NULL FOREIGN KEY REFERENCES Tourists(Id),
	BonusPrizeId INT NOT NULL FOREIGN KEY REFERENCES BonusPrizes(Id),
	PRIMARY KEY (TouristId, BonusPrizeId)
);


--Section 2. DML (10 pts)

--Insert

INSERT INTO Tourists (Name, Age, PhoneNumber, Nationality, Reward)
     VALUES ('Borislava Kazakova', 52, '+359896354244', 'Bulgaria', NULL),
			('Peter Bosh' , 48, '+447911844141', 'UK', NULL),
			('Martin Smith', 29, '+353863818592', 'Ireland', 'Bronze badge'),
			('Svilen Dobrev', 49, '+359986584786', 'Bulgaria', 'Silver badge'),
			('Kremena Popova', 38, '+359893298604', 'Bulgaria', NULL);

INSERT INTO Sites (Name, LocationId, CategoryId, Establishment)
	 VALUES ('Ustra fortress', 90, 7, 'X'),
		    ('Karlanovo Pyramids', 65, 7, NULL),
			('The Tomb of Tsar Sevt', 63, 8, 'V BC'),
			('Sinite Kamani Natural Park', 17, 1, NULL),
			('St. Petka of Bulgaria – Rupite', 92, 6, '1994');

--Update

UPDATE Sites
   SET Establishment = '(not defined)'
 WHERE Establishment IS NULL;

--Delete

DELETE FROM TouristsBonusPrizes
      WHERE BonusPrizeId = 5;

DELETE FROM BonusPrizes
	  WHERE Name = 'Sleeping bag';

--Section 3. Querying (40 pts)

--Tourists

  SELECT
	     Name,
	     Age,
	     PhoneNumber,
	     Nationality
    FROM Tourists
ORDER BY Nationality, Age DESC, Name;


--Sites with Their Location and Category

SELECT
	   s.Name AS Site,
	   l.Name AS Location,
	   s.Establishment,
	   c.Name AS Category
  FROM Sites AS s
  JOIN Locations AS l
    ON s.LocationId = l.Id
  JOIN Categories AS c
    ON c.Id = s.CategoryId
ORDER BY c.Name DESC, l.Name, s.Name

--Count of Sites in Sofia Province

  SELECT 
	     l.Province,
	     l.Municipality,
	     l.Name AS Location,
		 COUNT(l.Name) AS CountOfSites
    FROM Locations AS l
    JOIN Sites AS s
      ON l.id = s.LocationId
   WHERE l.Province = 'Sofia'
GROUP BY l.Province, l.Municipality, l.Name
ORDER BY COUNT(l.Name) DESC, l.Name;


--Tourist Sites established BC

  SELECT
		 s.Name AS Site,
		 l.Name AS Location,
		 l.Municipality,
		 l.Province,
		 s.Establishment
	FROM Locations AS l
	JOIN Sites AS s
	  ON l.Id = s.LocationId
   WHERE LEFT(l.Name, 1) NOT LIKE '[B,M,D]' AND s.Establishment LIKE '%BC'
ORDER BY s.Name;


--Tourists with their Bonus Prizes

   SELECT
	 	  t.Name,
		  t.Age,
		  t.PhoneNumber,
		  t.Nationality,
		  ISNULL(bp.Name, '(no bonus prize)') AS 'BonusPrize'
     FROM Tourists AS t
LEFT JOIN TouristsBonusPrizes AS tp
	   ON t.Id = tp.TouristId
LEFT JOIN BonusPrizes AS bp
	   ON bp.Id = tp.BonusPrizeId
 ORDER BY t.Name;

 --Tourists visiting History & Archaeology sites

  SELECT
		 SUBSTRING(t.Name, CHARINDEX(' ', t.Name) + 1, LEN(t.Name)) AS LastName,
		 t.Nationality,
		 t.Age,
		 t.PhoneNumber
    FROM Tourists AS t
	JOIN SitesTourists AS st
	  ON t.Id = st.TouristId
	JOIN Sites AS s
	  ON s.Id = st.SiteId
	JOIN Categories AS c
	  ON c.Id = s.CategoryId
   WHERE c.Name = 'History and archaeology'
GROUP BY t.Name, t.Nationality, t.Age, t.PhoneNumber
ORDER BY LastName;


--Section 4. Programmability (20 pts)

--Tourists Count on a Tourist Site

GO

CREATE FUNCTION udf_GetTouristsCountOnATouristSite (@Site VARCHAR(MAX))
RETURNS INT
	 AS
		BEGIN
			  DECLARE @result INT = (
									  SELECT
											 COUNT(*)
										FROM Sites AS s
										JOIN SitesTourists AS st
										  ON s.Id = st.SiteId
									   WHERE s.Name = @Site
									);

			   RETURN @result;
		  END;

GO

--Annual Reward Lottery

CREATE PROCEDURE usp_AnnualRewardLottery @TouristName VARCHAR(50)
	AS
	   BEGIN
			 DECLARE @sitesVisited INT = (
									  SELECT
											 COUNT(*)
										FROM Tourists AS t
										JOIN SitesTourists AS st
										  ON t.Id = st.TouristId
										JOIN Sites AS s
										  ON s.Id = st.SiteId
									   WHERE t.Name = @TouristName
									);

			 IF @sitesVisited >= 100
				BEGIN
					  UPDATE Tourists
					  SET Reward = 'Gold badge'
					  WHERE Name = @TouristName
				  END
			 ELSE IF @sitesVisited >= 50
				BEGIN
					  UPDATE Tourists
					  SET Reward = 'Silver badge'
					  WHERE Name = @TouristName
				  END
			 ELSE IF @sitesVisited >= 25
				BEGIN
					  UPDATE Tourists
					  SET Reward = 'Bronze badge'
					  WHERE Name = @TouristName
				  END

			 SELECT
					Name,
					Reward
			   FROM Tourists
			  WHERE Name = @TouristName
	     END;

GO

EXEC usp_AnnualRewardLottery 'Gerhild Lutgard';

EXEC usp_AnnualRewardLottery 'Teodor Petrov';

EXEC usp_AnnualRewardLottery 'Zac Walsh';

EXEC usp_AnnualRewardLottery 'Brus Brown';
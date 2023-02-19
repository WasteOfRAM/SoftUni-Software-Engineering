--Section 1. DDL (30 pts)

CREATE DATABASE Boardgames
GO

USE Boardgames
GO


CREATE TABLE Categories
(
	Id INT IDENTITY PRIMARY KEY,
	[Name] NVARCHAR(50) NOT NULL
);


CREATE TABLE Addresses
(
	Id INT IDENTITY PRIMARY KEY,
	StreetName NVARCHAR(100) NOT NULL,
	StreetNumber INT NOT NULL,
	Town NVARCHAR(30) NOT NULL,
	Country NVARCHAR(50) NOT NULL,
	ZIP INT NOT NULL
);


CREATE TABLE Publishers
(
	Id INT IDENTITY PRIMARY KEY,
	[Name] NVARCHAR(30) NOT NULL UNIQUE,
	AddressId INT NOT NULL FOREIGN KEY REFERENCES Addresses(Id),
	Website NVARCHAR(40),
	Phone NVARCHAR(20)
);

CREATE TABLE PlayersRanges
(
	Id INT IDENTITY PRIMARY KEY,
	PlayersMin INT NOT NULL,
	PlayersMax INT NOT NULL
);

CREATE TABLE Boardgames
(
	Id INT IDENTITY PRIMARY KEY,
	[Name] NVARCHAR(30) NOT NULL,
	YearPublished INT NOT NULL,
	Rating DECIMAL(20, 2) NOT NULL,
	CategoryId INT NOT NULL FOREIGN KEY REFERENCES Categories(Id),
	PublisherId INT NOT NULL FOREIGN KEY REFERENCES Publishers(Id),
	PlayersRangeId INT NOT NULL FOREIGN KEY REFERENCES PlayersRanges(Id)
);

CREATE TABLE Creators
(
	Id INT IDENTITY PRIMARY KEY,
	FirstName NVARCHAR(30) NOT NULL,
	LastName NVARCHAR(30) NOT NULL,
	Email NVARCHAR(30) NOT NULL
);

CREATE TABLE CreatorsBoardgames
(
	CreatorId INT NOT NULL FOREIGN KEY REFERENCES Creators(Id),
	BoardgameId INT NOT NULL FOREIGN KEY REFERENCES Boardgames(Id),
	PRIMARY KEY(CreatorId, BoardgameId)
);


--Section 2. DML (10 pts)

--Insert

INSERT INTO Publishers ([Name], AddressId, Website, Phone)
	 VALUES ('Agman Games', 5, 'www.agmangames.com', '+16546135542'),
			('Amethyst Games', 7, 'www.amethystgames.com', '+15558889992'),
			('BattleBooks', 13, 'www.battlebooks.com', '+12345678907');

INSERT INTO Boardgames ([Name], YearPublished, Rating, CategoryId, PublisherId, PlayersRangeId)
	 VALUES ('Deep Blue', 2019, 5.67, 1, 15, 7),
			('Paris', 2016, 9.78, 7, 1, 5),
			('Catan: Starfarers', 2021, 9.87, 7, 13, 6),
			('Bleeding Kansas', 2020, 3.25, 3, 7, 4),
			('One Small Step', 2019, 5.75, 5, 9, 2);

--Update

UPDATE PlayersRanges
   SET PlayersMax += 1
 WHERE PlayersMin = 2 AND PlayersMax = 2;

UPDATE Boardgames
   SET [Name] = [Name] + 'V2'
 WHERE YearPublished >= 2020;

 --Delete

DELETE FROM CreatorsBoardgames
	  WHERE BoardgameId IN (SELECT Id FROM Boardgames WHERE PublisherId IN (SELECT Id FROM Publishers WHERE AddressId IN (SELECT Id FROM Addresses WHERE Country = (SELECT Country FROM Addresses WHERE Town LIKE 'L%'))))

DELETE FROM Boardgames
	  WHERE PublisherId IN (SELECT Id FROM Publishers WHERE AddressId IN (SELECT Id FROM Addresses WHERE Country = (SELECT Country FROM Addresses WHERE Town LIKE 'L%')))

DELETE FROM Publishers
		WHERE AddressId IN (SELECT Id FROM Addresses WHERE Country = (SELECT Country FROM Addresses WHERE Town LIKE 'L%'))

DELETE FROM Addresses
       WHERE Country = (SELECT Country FROM Addresses WHERE Town LIKE 'L%')


--Section 3. Querying (40 pts)

--Boardgames by Year of Publication

   SELECT
		  [Name],
		  Rating
	 FROM Boardgames
 ORDER BY YearPublished, [Name] DESC;


 --Boardgames by Category

  SELECT
		 b.Id,
		 b.[Name],
		 b.YearPublished,
		 c.[Name] AS CategoryName
    FROM Boardgames AS b
    JOIN Categories AS c
      ON b.CategoryId = c.Id
   WHERE c.[Name] IN ('Strategy Games', 'Wargames')
ORDER BY b.YearPublished DESC;


--Creators without Boardgames

   SELECT
		  c.Id,
		  CONCAT_WS(' ', c.FirstName, c.LastName) AS CreatorName,
		  c.Email
     FROM Creators AS c
LEFT JOIN CreatorsBoardgames AS cb
       ON c.Id = cb.CreatorId
	WHERE cb.BoardgameId IS NULL
 ORDER BY CreatorName;

--First 5 Boardgames

   SELECT TOP(5)
		  b.[Name],
		  b.Rating,
		  c.[Name] AS CategoryName
     FROM Boardgames AS b
	 JOIN Categories AS c
	   ON b.CategoryId = c.Id
	 JOIN PlayersRanges AS pr
	   ON b.PlayersRangeId = pr.Id
	WHERE (b.Rating > 7.00 AND b.[Name] LIKE '%a%') OR (b.Rating > 7.50 AND (pr.PlayersMin = 2 AND pr.PlayersMax = 5))
 ORDER BY b.[Name], b.Rating DESC;


-- Creators with Emails
  SELECT
	     FullName,
	     Email,
	     Rating
    FROM (
		   SELECT
				  CONCAT_WS(' ', c.FirstName, c.LastName) AS FullName,
				  c.Email,
				  DENSE_RANK() OVER (PARTITION BY c.Email ORDER BY b.Rating DESC) AS [Rank],
				  b.Rating
			 FROM Creators AS c
			 JOIN CreatorsBoardgames AS cb
			   ON c.Id = cb.CreatorId
			 JOIN Boardgames AS b
			   ON cb.BoardgameId = b.Id
			WHERE c.Email LIKE '%.com'
	       ) AS Ranked
	WHERE [Rank] = 1
 ORDER BY FullName;


--Creators by Rating

   SELECT
		  c.LastName,
		  CEILING(AVG(b.Rating)) AS AverageRating,
		  p.[Name] AS PublisherName
     FROM Creators AS c
	 JOIN CreatorsBoardgames AS cb
	   ON c.Id = cb.CreatorId
	 JOIN Boardgames AS b
	   ON b.Id = cb.BoardgameId
	 JOIN Publishers AS p
	   ON b.PublisherId = p.Id
	WHERE p.[Name] = 'Stonemaier Games'
 GROUP BY c.LastName, p.[Name]
 ORDER BY AVG(b.Rating) DESC;

--Section 4. Programmability (20 pts)

--Creator with Boardgames
GO

CREATE FUNCTION udf_CreatorWithBoardgames(@name NVARCHAR(30))
	RETURNS INT
		     AS
		  BEGIN
				 RETURN 
						(
							SELECT
								   COUNT(*)
							  FROM Creators AS c
							  JOIN CreatorsBoardgames AS cb
							    ON c.Id = cb.CreatorId
							  JOIN Boardgames AS b
							    ON cb.BoardgameId = b.Id
							 WHERE c.FirstName = @name
						)
		    END;

GO

--Search for Boardgame with Specific Category

CREATE PROCEDURE usp_SearchByCategory(@category NVARCHAR(50))
			  AS
		   BEGIN
				     SELECT
							b.[Name],
							b.YearPublished,
							b.Rating,
							c.[Name] AS CategoryName,
							p.[Name] AS PublisherName,
							CONCAT_WS(' ', pr.PlayersMin, 'people') AS MinPlayers,
							CONCAT_WS(' ', pr.PlayersMax, 'people') AS MaxPlayers
					   FROM Boardgames AS b
					   JOIN Categories AS c
					     ON b.CategoryId = c.Id
					   JOIN Publishers AS p
					     ON b.PublisherId = p.Id
					   JOIN PlayersRanges AS pr
					     ON b.PlayersRangeId = pr.Id
					  WHERE c.[Name] = @category
				   ORDER BY p.[Name], b.YearPublished DESC
		     END;
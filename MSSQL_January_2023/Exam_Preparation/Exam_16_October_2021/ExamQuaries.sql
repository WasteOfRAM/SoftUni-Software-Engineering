--Section 1. DDL (30 pts)

CREATE DATABASE CigarShop;
GO

USE CigarShop;
GO

--Sizes

CREATE TABLE Sizes
(
	Id INT IDENTITY PRIMARY KEY,
	[Length] INT NOT NULL CHECK ([Length] BETWEEN 10 AND 25),
	RingRange DECIMAL(18, 2) NOT NULL CHECK (RingRange BETWEEN 1.5 AND 7.5)
);

--Tastes

CREATE TABLE Tastes
(
	Id INT IDENTITY PRIMARY KEY,
	TasteType VARCHAR(20) NOT NULL,
	TasteStrength VARCHAR(15) NOT NULL,
	ImageURL NVARCHAR(100) NOT NULL
);

--Brands

CREATE TABLE Brands
(
	Id INT IDENTITY PRIMARY KEY,
	BrandName VARCHAR(30) NOT NULL UNIQUE,
	BrandDescription VARCHAR(MAX)
);

--Cigars

CREATE TABLE Cigars
(
	Id INT IDENTITY PRIMARY KEY,
	CigarName VARCHAR(80) NOT NULL,
	BrandId INT NOT NULL FOREIGN KEY REFERENCES Brands(Id),
	TastId INT NOT NULL FOREIGN KEY REFERENCES Tastes(Id),
	SizeId INT NOT NULL FOREIGN KEY REFERENCES Sizes(Id),
	PriceForSingleCigar MONEY NOT NULL,
	ImageURL NVARCHAR(100) NOT NULL
);

--Addresses

CREATE TABLE Addresses
(
	Id INT IDENTITY PRIMARY KEY,
	Town VARCHAR(30) NOT NULL,
	Country NVARCHAR(30) NOT NULL,
	Streat NVARCHAR(100) NOT NULL,
	ZIP VARCHAR(20) NOT NULL
);

--Clients

CREATE TABLE Clients
(
	Id INT IDENTITY PRIMARY KEY,
	FirstName NVARCHAR(30) NOT NULL,
	LastName NVARCHAR(30) NOT NULL,
	Email NVARCHAR(30) NOT NULL,
	AddressId INT NOT NULL FOREIGN KEY REFERENCES Addresses(Id)
);

--ClientsCigars

CREATE TABLE ClientsCigars
(
	ClientId INT FOREIGN KEY REFERENCES Clients(Id),
	CigarId INT FOREIGN KEY REFERENCES Cigars(Id),
	PRIMARY KEY (ClientId, CigarId)
);


--Section 2. DML (10 pts)

--Insert

INSERT INTO Cigars (CigarName, BrandId, TastId, SizeId, PriceForSingleCigar, ImageURL)
	 VALUES ('COHIBA ROBUSTO', 9, 1, 5, 15.50, 'cohiba-robusto-stick_18.jpg'),
			('COHIBA SIGLO I', 9, 1, 10, 410.00, 'cohiba-siglo-i-stick_12.jpg'),
			('HOYO DE MONTERREY LE HOYO DU MAIRE', 14, 5, 11, 7.50, 'hoyo-du-maire-stick_17.jpg'),
			('HOYO DE MONTERREY LE HOYO DE SAN JUAN', 14, 4, 15, 32.00, 'hoyo-de-san-juan-stick_20.jpg'),
			('TRINIDAD COLONIALES', 2, 3, 8, 85.21, 'trinidad-coloniales-stick_30.jpg');

INSERT INTO Addresses (Town, Country, Streat, ZIP)
	 VALUES ('Sofia', 'Bulgaria', '18 Bul. Vasil levski', 1000),
			('Athens', 'Greece', '4342 McDonald Avenue', 10435),
			('Zagreb', 'Croatia', '4333 Lauren Drive', 10000);


--Update

UPDATE Cigars
   SET PriceForSingleCigar *= 1.20
 WHERE TastId = (SELECT Id FROM Tastes WHERE TasteType = 'Spicy');

UPDATE Brands
   SET BrandDescription = 'New description'
 WHERE BrandDescription IS NULL;


--Delete

DELETE FROM Clients WHERE AddressId IN (SELECT Id FROM Addresses WHERE Country LIKE 'C%');

DELETE FROM Addresses
	  WHERE Country LIKE 'C%';


--Section 3. Querying (40 pts)

--Cigars by Price

  SELECT
		 CigarName,
		 PriceForSingleCigar,
		 ImageURL
	FROM Cigars
ORDER BY PriceForSingleCigar, CigarName DESC;


--Cigars by Taste

  SELECT
		 c.Id,
		 c.CigarName,
		 c.PriceForSingleCigar,
		 t.TasteType,
		 t.TasteStrength
	FROM Cigars AS c
	JOIN Tastes AS t
	  ON c.TastId = t.Id
   WHERE TasteType IN ('Earthy', 'Woody')
ORDER BY c.PriceForSingleCigar DESC;


--Clients without Cigars

   SELECT
		  c.Id,
		  CONCAT_WS(' ', c.FirstName, c.LastName) AS ClientName,
		  c.Email
	 FROM Clients AS c
LEFT JOIN ClientsCigars AS cc
	   ON c.Id = cc.ClientId
	WHERE cc.CigarId IS NULL
 ORDER BY ClientName;


--First 5 Cigars

  SELECT TOP(5)
		 c.CigarName,
		 c.PriceForSingleCigar,
		 c.ImageURL
    FROM Cigars AS c
	JOIN Sizes AS s
	  ON c.SizeId = s.Id
   WHERE s.[Length] >= 12 AND (c.CigarName LIKE '%ci%' OR c.PriceForSingleCigar > 50) AND s.RingRange > 2.55
ORDER BY c.CigarName, c.PriceForSingleCigar DESC;


--Clients with ZIP Codes

   SELECT
	      FullName,
	      Country,
	      ZIP,
	      CONCAT('$', PriceForSingleCigar) AS CigarPrice
     FROM
          (
			  SELECT 
					 CONCAT_WS(' ', c.FirstName, c.LastName) AS FullName,
					 a.Country,
					 a.ZIP,
					 DENSE_RANK() OVER (PARTITION BY CONCAT_WS(' ', c.FirstName, c.LastName) ORDER BY ci.PriceForSingleCigar DESC) AS PriceRank,
					 ci.PriceForSingleCigar
				FROM Clients AS c
				JOIN Addresses AS a
				  ON c.AddressId = a.Id
				JOIN ClientsCigars AS cc
				  ON c.Id = cc.ClientId
				JOIN Cigars AS ci
				  ON cc.CigarId = ci.Id
			   WHERE ISNUMERIC(a.ZIP) = 1
          ) AS RankedByPrice
    WHERE PriceRank = 1
 ORDER BY FullName;

 --Cigars by Size

  SELECT
		 c.LastName,
		 CEILING(AVG(s.[Length])) AS CiagrLength,
		 CEILING(AVG(s.RingRange)) AS CiagrRingRange
    FROM Clients AS c
	JOIN ClientsCigars AS cc
	  ON c.Id = cc.ClientId
	JOIN Cigars AS ci
	  ON cc.CigarId = ci.Id
	JOIN Sizes AS s
	  ON ci.SizeId = s.Id
GROUP BY c.LastName
ORDER BY CEILING(AVG(s.[Length])) DESC;


--Section 4. Programmability (20 pts)

--Client with Cigars

GO

CREATE FUNCTION udf_ClientWithCigars(@name NVARCHAR(30))
    RETURNS INT
			 AS
		  BEGIN
				RETURN (
							 SELECT
									COUNT(*)
							   FROM Clients AS c
						  LEFT JOIN ClientsCigars AS cc
								 ON c.Id = cc.ClientId
							  WHERE c.FirstName = @name
					   )
		    END;

GO

SELECT dbo.udf_ClientWithCigars('Betty'); -- 5

--Search for Cigar with Specific Taste

GO

CREATE PROCEDURE usp_SearchByTaste(@taste VARCHAR(20))
			  AS
		   BEGIN
				   SELECT
						  c.CigarName,
						  CONCAT('$', c.PriceForSingleCigar) AS Price,
						  t.TasteType,
						  b.BrandName,
						  CONCAT_WS(' ', s.[Length], 'cm') AS CigarLength,
						  CONCAT_WS(' ', s.RingRange, 'cm') AS CigarRingRange
				     FROM Cigars AS c
					 JOIN Tastes AS t
					   ON c.TastId = t.Id
					 JOIN Sizes AS s
					   ON c.SizeId = s.Id
					 JOIN Brands AS b
					   ON c.BrandId = b.Id
					WHERE t.TasteType = @taste
				 ORDER BY s.[Length], s.RingRange DESC;
		     END;

GO

EXEC usp_SearchByTaste 'Woody'
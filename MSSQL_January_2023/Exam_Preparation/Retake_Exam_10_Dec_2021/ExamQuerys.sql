--Section 1. DDL (30 pts)

CREATE DATABASE Airport;
GO

USE Airport;
GO

--Passengers

CREATE TABLE Passengers
(
	Id INT IDENTITY PRIMARY KEY,
	FullName VARCHAR(100) NOT NULL UNIQUE,
	Email VARCHAR(50) NOT NULL UNIQUE
);

--Pilots

CREATE TABLE Pilots
(
	Id INT IDENTITY PRIMARY KEY,
	FirstName VARCHAR(30) NOT NULL UNIQUE,
	LastName VARCHAR(30) NOT NULL UNIQUE,
	Age TINYINT NOT NULL CHECK(Age BETWEEN 21 AND 62),
	Rating FLOAT CHECK(Rating BETWEEN 0.0 AND 10.0)
);

--AircraftTypes

CREATE TABLE AircraftTypes
(
	Id INT IDENTITY PRIMARY KEY,
	TypeName VARCHAR(30) NOT NULL UNIQUE
);

--Aircraft

CREATE TABLE Aircraft
(
	Id INT IDENTITY PRIMARY KEY,
	Manufacturer VARCHAR(25) NOT NULL,
	Model VARCHAR(30) NOT NULL,
	[Year] INT NOT NULL,
	FlightHours INT,
	Condition CHAR NOT NULL,
	TypeId INT NOT NULL FOREIGN KEY REFERENCES AircraftTypes(Id)
);

--PilotsAircraft

CREATE TABLE PilotsAircraft
(
	AircraftId INT FOREIGN KEY REFERENCES Aircraft(Id),
	PilotId INT FOREIGN KEY REFERENCES Pilots(Id),
	PRIMARY KEY (AircraftId, PilotId)
);


--Airports

CREATE TABLE Airports
(
	Id INT IDENTITY PRIMARY KEY,
	AirportName VARCHAR(70) NOT NULL UNIQUE,
	Country VARCHAR(100) NOT NULL UNIQUE
);

--FlightDestinations

CREATE TABLE FlightDestinations
(
	Id INT IDENTITY PRIMARY KEY,
	AirportId INT NOT NULL FOREIGN KEY REFERENCES Airports(Id),
	[Start] DATETIME NOT NULL,
	AircraftId INT NOT NULL FOREIGN KEY REFERENCES Aircraft(Id),
	PassengerId INT NOT NULL FOREIGN KEY REFERENCES Passengers(Id),
	TicketPrice DECIMAL(18, 2) NOT NULL DEFAULT 15
);


--Section 2. DML (10 pts)

--Insert

INSERT INTO Passengers (FullName, Email)
SELECT 
	   CONCAT_WS(' ', FirstName, LastName) AS FullName,
	   CONCAT(FirstName, LastName, '@gmail.com')
  FROM Pilots
 WHERE Id BETWEEN 5 AND 15;

 --Update

UPDATE Aircraft
   SET Condition = 'A'
 WHERE Condition IN ('C', 'B') AND
	   (FlightHours IS NULL OR FlightHours <= 100) AND
	   [Year] >= 2013;


--Delete

DELETE
  FROM Passengers
 WHERE LEN(FullName) <= 10;


 --Section 3. Querying (40 pts)

 --Aircraft

  SELECT
		 Manufacturer,
		 Model,
		 FlightHours,
     	 Condition
    FROM Aircraft
ORDER BY FlightHours DESC;


--Pilots and Aircraft

  SELECT
		 p.FirstName,
		 p.LastName,
		 a.Manufacturer,
		 a.Model,
		 a.FlightHours
    FROM Pilots AS p
	JOIN PilotsAircraft AS pa
	  ON p.Id = pa.PilotId
	JOIN Aircraft AS a
	  ON pa.AircraftId = A.Id
   WHERE a.FlightHours IS NOT NULL AND a.FlightHours < 304
ORDER BY a.FlightHours DESC, p.FirstName;

--Top 20 Flight Destinations

  SELECT TOP(20)
		 fd.Id AS DestinationId,
		 fd.[Start],
		 p.FullName,
		 a.AirportName,
		 fd.TicketPrice
    FROM FlightDestinations AS fd
	JOIN Passengers AS p
	  ON fd.PassengerId = p.Id
	JOIN Airports AS a
	  ON fd.AirportId = a.Id
   WHERE DAY(fd.[Start]) % 2 = 0
ORDER BY fd.TicketPrice DESC, a.AirportName;


--Number of Flights for Each Aircraft

   SELECT
		  fd.AircraftId,
		  a.Manufacturer,
		  a.FlightHours,
		  COUNT(fd.AircraftId) AS FlightDestinationsCount,
		  ROUND(AVG(fd.TicketPrice), 2) AS AvgPrice
     FROM Aircraft AS a
LEFT JOIN FlightDestinations AS fd
	   ON a.Id = fd.AircraftId
 GROUP BY fd.AircraftId, a.Manufacturer, a.FlightHours
   HAVING COUNT(fd.AircraftId) >= 2
 ORDER BY FlightDestinationsCount DESC, fd.AircraftId;


 --Regular Passengers

   SELECT
		  p.FullName,
		  COUNT(a.Id) AS CountOfAircraft,
		  SUM(fd.TicketPrice) AS TotalPayed
     FROM Passengers AS p
	 JOIN FlightDestinations AS fd
	   ON p.Id = fd.PassengerId
	 JOIN Aircraft AS a
	   ON fd.AirportId = a.Id
	WHERE p.FullName LIKE '_a%'
 GROUP BY p.FullName
   HAVING COUNT(a.Id) >= 2
 ORDER BY p.FullName;


 --Full Info for Flight Destinations

   SELECT
		  ap.AirportName,
		  fd.[Start],
		  fd.TicketPrice,
		  p.FullName,
		  ac.Manufacturer,
		  ac.Model
     FROM FlightDestinations AS fd
	 JOIN Airports AS ap
	   ON fd.AirportId = ap.Id
	 JOIN Passengers AS p
	   ON fd.PassengerId = p.Id
	 JOIN Aircraft AS ac
	   ON fd.AircraftId = ac.Id
    WHERE fd.TicketPrice > 2500 AND (DATEPART(HOUR, fd.[Start]) >= 6 AND DATEPART(HOUR, fd.[Start]) <= 20)
 ORDER BY ac.Model;


 --Section 4. Programmability (20 pts)

 --Find all Destinations by Email Address

GO

 CREATE FUNCTION udf_FlightDestinationsByEmail (@email VARCHAR(50))
	 RETURNS INT
			  AS
		   BEGIN
				 RETURN (
						   SELECT
								  COUNT(fd.PassengerId)
							 FROM Passengers AS p
						LEFT JOIN FlightDestinations AS fd
							   ON p.Id = fd.PassengerId
						    WHERE p.Email = @email
						 GROUP BY p.FullName
						)
				   
		     END;

GO

--Full Info for Airports

CREATE PROCEDURE usp_SearchByAirportName (@airportName VARCHAR(70))
			  AS
		   BEGIN
				   SELECT
						  a.AirportName,
						  p.FullName,
						  CASE
							   WHEN fd.TicketPrice <= 400 THEN 'Low'
							   WHEN fd.TicketPrice BETWEEN 401 AND 1500 THEN 'Medium'
							   ELSE 'High'
						   END AS LevelOfTickerPrice,
						  ac.Manufacturer,
						  ac.Condition,
						  act.TypeName
				     FROM Airports AS a
					 JOIN FlightDestinations AS fd
					   ON a.Id = fd.AirportId
					 JOIN Passengers AS p
					   ON fd.PassengerId = p.Id
					 JOIN Aircraft AS ac
					   ON fd.AircraftId = ac.Id
					 JOIN AircraftTypes AS act
					   on ac.TypeId = act.Id
					WHERE a.AirportName = @airportName
				 ORDER BY ac.Manufacturer, p.FullName
		     END;
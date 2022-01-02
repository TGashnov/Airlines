CREATE TABLE [dbo].[PassInTrip]
(
	[TripNo] INT NOT NULL FOREIGN KEY REFERENCES [Trip]([TripNo]), 
    [Date] DATETIME2 NOT NULL, 
    [PassId] INT NOT NULL FOREIGN KEY REFERENCES [Passenger]([PassId]), 
    [Place] CHAR(10) NOT NULL, 
    PRIMARY KEY ([TripNo], [Date], [PassId])
)

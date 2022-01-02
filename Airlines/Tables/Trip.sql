CREATE TABLE [dbo].[Trip]
(
	[TripNo] INT NOT NULL PRIMARY KEY, 
    [CompId] INT NOT NULL FOREIGN KEY REFERENCES [Company]([CompId]), 
    [Plane] CHAR(10) NOT NULL, 
    [TownFrom] CHAR(25) NOT NULL, 
    [TownTo] CHAR(25) NOT NULL, 
    [TimeOut] DATETIME2 NOT NULL, 
    [TimeIn] DATETIME2 NOT NULL
)

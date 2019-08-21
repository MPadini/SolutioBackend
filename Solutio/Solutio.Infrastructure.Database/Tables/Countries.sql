CREATE TABLE [dbo].[Countries]
(
	[Id] bigint NOT NULL PRIMARY KEY IDENTITY,
	[Name] VARCHAR(50) NOT NULL,
	[Created] DATETIME NOT null DEFAULT GETDATE(),
	[Modified] DATETIME,
	[Deleted] DATETIME
)

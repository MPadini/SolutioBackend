CREATE TABLE [dbo].[Offices]
(
	[Id] BIGINT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] VARCHAR(50) NOT NULL, 
    [OwnerName] VARCHAR(150) NULL, 
    [OwnerDni] VARCHAR(150) NULL, 
    [OwnerCuit] VARCHAR(150) NULL,
	[Created] DATETIME NOT null DEFAULT GETDATE(),
	[Modified] DATETIME,
	[Deleted] DATETIME  
)

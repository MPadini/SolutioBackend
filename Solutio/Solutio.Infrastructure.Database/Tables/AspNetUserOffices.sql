CREATE TABLE [dbo].[AspNetUserOffices]
(
	[UserId] INT NOT NULL , 
    [OfficeId] BIGINT NOT NULL, 
	[Created] DATETIME NOT null DEFAULT GETDATE(),
	[Modified] DATETIME,
	[Deleted] DATETIME,
    PRIMARY KEY ([UserId], [OfficeId])
)

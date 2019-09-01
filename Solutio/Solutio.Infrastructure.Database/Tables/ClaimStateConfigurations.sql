CREATE TABLE [dbo].[ClaimStateConfigurations]
(
	[Id] BIGINT NOT NULL IDENTITY, 
	ParentClaimStateId BIGINT NOT NULL ,
	AllowedStateId  BIGINT NULL,
	[Created] DATETIME NOT null DEFAULT GETDATE(),
	[Modified] DATETIME,
	[Deleted] DATETIME, 
    PRIMARY KEY ([Id])
)

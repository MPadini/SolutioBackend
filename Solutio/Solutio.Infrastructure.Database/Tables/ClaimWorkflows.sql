CREATE TABLE [dbo].[ClaimWorkflows]
(
	[Id] BIGINT NOT NULL PRIMARY KEY IDENTITY, 
    [ClaimStateId] BIGINT NOT NULL, 
	[ClaimId] BIGINT NOT NULL,
    [Created] DATETIME NOT NULL DEFAULT GetDate(), 
    [Modified] DATETIME NULL, 
    [Deleted] DATETIME NULL, 
    [UserName] VARCHAR(150) NULL
)

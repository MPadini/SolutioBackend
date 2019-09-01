CREATE TABLE [ClaimStates] (
    [Id] bigint NOT NULL IDENTITY,
    [Description] nvarchar(max) NOT NULL,
	[MaximumTimeAllowed] INT NULL,
	[Created] DATETIME NOT null DEFAULT GETDATE(),
	[Modified] DATETIME,
	[Deleted] DATETIME
    CONSTRAINT [PK_ClaimStates] PRIMARY KEY ([Id]),
);
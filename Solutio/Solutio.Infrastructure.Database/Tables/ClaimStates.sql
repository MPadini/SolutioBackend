CREATE TABLE [ClaimStates] (
    [Id] bigint NOT NULL IDENTITY,
    [Description] nvarchar(max) NOT NULL,
	[MaximumTimeAllowed] INT NULL,
    [CanInDraft] BIT NOT NULL DEFAULT 0, 
    [CanAudit] BIT NOT NULL DEFAULT 0, 
    [CanPresented] BIT NOT NULL DEFAULT 0, 
    [CanWaitForAction] BIT NOT NULL DEFAULT 0, 
    [CanInMonitoring] BIT NOT NULL DEFAULT 0, 
    [CanOffered] BIT NOT NULL DEFAULT 0, 
    [CanAcepted] BIT NOT NULL DEFAULT 0, 
    [CanOutstanding] BIT NOT NULL DEFAULT 0, 
    [CanClose] BIT NOT NULL DEFAULT 0, 
	[Created] DATETIME NOT null DEFAULT GETDATE(),
	[Modified] DATETIME,
	[Deleted] DATETIME
    CONSTRAINT [PK_ClaimStates] PRIMARY KEY ([Id]),
);
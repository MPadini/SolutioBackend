CREATE TABLE [PersonTypes] (
    [Id] bigint NOT NULL IDENTITY,
    [Description] nvarchar(max) NULL,
	[Created] DATETIME NOT null DEFAULT GETDATE(),
	[Modified] DATETIME,
	[Deleted] DATETIME,
    CONSTRAINT [PK_PersonTypes] PRIMARY KEY ([Id])
);

CREATE TABLE [ClaimStates] (
    [Id] bigint NOT NULL IDENTITY,
    [Description] nvarchar(max) NULL,
    CONSTRAINT [PK_ClaimStates] PRIMARY KEY ([Id])
);
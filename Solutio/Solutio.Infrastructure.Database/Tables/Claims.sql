CREATE TABLE [Claims] (
    [Id] bigint NOT NULL IDENTITY,
    [Story] nvarchar(max) NULL,
    [Date] datetime2 NOT NULL,
    [Hour] datetime2 NOT NULL,
    [StateId] bigint NOT NULL,
    CONSTRAINT [PK_Claims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Claims_ClaimStates_StateId] FOREIGN KEY ([StateId]) REFERENCES [ClaimStates] ([Id]) ON DELETE CASCADE
);
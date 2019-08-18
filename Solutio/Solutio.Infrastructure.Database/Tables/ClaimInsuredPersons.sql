CREATE TABLE [ClaimInsuredPersons] (
    [PersonId] bigint NOT NULL,
    [ClaimId] bigint NOT NULL,
    [Created] datetime2 NOT NULL,
    [Modified] datetime2 NULL,
    [Deleted] datetime2 NULL,
    CONSTRAINT [PK_ClaimInsuredPersons] PRIMARY KEY ([PersonId], [ClaimId]),
    CONSTRAINT [FK_ClaimInsuredPersons_Claims_ClaimId] FOREIGN KEY ([ClaimId]) REFERENCES [Claims] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ClaimInsuredPersons_Persons_PersonId] FOREIGN KEY ([PersonId]) REFERENCES [Persons] ([Id]) ON DELETE CASCADE
);
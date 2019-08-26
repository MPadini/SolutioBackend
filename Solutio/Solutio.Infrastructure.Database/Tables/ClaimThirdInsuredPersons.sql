
CREATE TABLE [ClaimThirdInsuredPersons] (
    [PersonId] bigint NOT NULL,
    [ClaimId] bigint NOT NULL,
    [Created] datetime NOT NULL DEFAULT GetDate(),
    [Modified] datetime NULL,
    [Deleted] datetime NULL,
    CONSTRAINT [PK_ClaimThirdInsuredPersons] PRIMARY KEY ([PersonId], [ClaimId]),
    CONSTRAINT [FK_ClaimThirdInsuredPersons_Claims_ClaimId] FOREIGN KEY ([ClaimId]) REFERENCES [Claims] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ClaimThirdInsuredPersons_Persons_PersonId] FOREIGN KEY ([PersonId]) REFERENCES [Persons] ([Id]) ON DELETE CASCADE
);

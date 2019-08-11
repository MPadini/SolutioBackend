CREATE TABLE [ClaimPersons] (
    [PersonId] bigint NOT NULL,
    [ClaimId] bigint NOT NULL,
    [PersonResponsabilityTypeId] bigint NOT NULL,
	[Created] DATETIME NOT null DEFAULT GETDATE(),
	[Modified] DATETIME,
	[Deleted] DATETIME,
    CONSTRAINT [PK_ClaimPersons] PRIMARY KEY ([PersonId], [ClaimId]),
    CONSTRAINT [FK_ClaimPersons_Claims_ClaimId] FOREIGN KEY ([ClaimId]) REFERENCES [Claims] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ClaimPersons_Persons_PersonId] FOREIGN KEY ([PersonId]) REFERENCES [Persons] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ClaimPersons_PersonResponsabilityTypes_PersonResponsabilityTypeId] FOREIGN KEY ([PersonResponsabilityTypeId]) REFERENCES [PersonResponsabilityTypes] ([Id]) ON DELETE CASCADE
);
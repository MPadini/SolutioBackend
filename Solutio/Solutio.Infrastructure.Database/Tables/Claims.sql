CREATE TABLE [Claims] (
    [Id] bigint NOT NULL IDENTITY,
    [Story] nvarchar(max) NULL,
    [Date] datetime2 NOT NULL,
    [Hour] datetime2 NOT NULL,
    [StateId] bigint NOT NULL,
    [TotalBudgetAmount] decimal(18,2) NULL,
    [InsuranceCompany] nvarchar(max) NULL,
    [HaveFullCoverage] bit NULL DEFAULT 0,
    [Franchise] decimal(18,2) NULL,
	[Created] datetime2 NOT NULL,
    [Modified] datetime2 NULL,
    [Deleted] datetime2 NULL,
    CONSTRAINT [PK_Claims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Claims_ClaimStates_StateId] FOREIGN KEY ([StateId]) REFERENCES [ClaimStates] ([Id])
);
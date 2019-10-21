﻿CREATE TABLE [Claims] (
    [Id] bigint NOT NULL IDENTITY,
    [Story] nvarchar(max) NULL,
    [Date] DATETIME NOT NULL,
    [Hour] VARCHAR(5) NOT NULL,
    [StateId] bigint NOT NULL,
	[StateModifiedDate] DATETIME NOT NULL DEFAULT GetDate(),
	[AdressId] bigint NULL,
    [TotalBudgetAmount] decimal(18,2) NULL,
	[Created] DATETIME NOT NULL,
    [Modified] DATETIME NULL,
    [Deleted] DATETIME NULL,
	[UserName] varchar(500) NOT null,
    CONSTRAINT [PK_Claims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Claims_ClaimStates_StateId] FOREIGN KEY ([StateId]) REFERENCES [ClaimStates] ([Id])
);
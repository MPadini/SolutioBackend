CREATE TABLE [Persons] (
    [Id] bigint NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Surname] nvarchar(max) NOT NULL,
    [DocumentNumber] int NOT NULL,
    [TelephoneNumber] int NOT NULL,
    [MobileNumber] int NOT NULL,
    [Email] nvarchar(max) NULL,
    [Adress] nvarchar(max) NULL,
    [PersonTypeId] bigint NOT NULL,
	[Created] DATETIME NOT null DEFAULT GETDATE(),
	[Modified] DATETIME,
	[Deleted] DATETIME,
    CONSTRAINT [PK_Persons] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Persons_PersonTypes_PersonTypeId] FOREIGN KEY ([PersonTypeId]) REFERENCES [PersonTypes] ([Id]) ON DELETE CASCADE
);
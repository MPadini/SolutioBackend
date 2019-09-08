CREATE TABLE [Persons] (
    [Id] bigint NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [Surname] nvarchar(max) NULL,
	[LegalEntityName] nvarchar(200) NULL,
	[Cuit] nvarchar(100) NULL,
    [DocumentNumber] VARCHAR(50) NULL,
    [TelephoneNumber] VARCHAR(50) NULL,
    [MobileNumber] VARCHAR(50) NULL,
    [Email] nvarchar(max) NULL,
    [Adress] nvarchar(max) NULL,
    [PersonTypeId] bigint NULL,
	[Created] DATETIME NOT null DEFAULT GETDATE(),
	[Modified] DATETIME,
	[Deleted] DATETIME,
    CONSTRAINT [PK_Persons] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Persons_PersonTypes_PersonTypeId] FOREIGN KEY ([PersonTypeId]) REFERENCES [PersonTypes] ([Id])
);
CREATE TABLE [VehicleTypes] (
    [Id] bigint NOT NULL IDENTITY,
    [Description] nvarchar(max) NULL,
	[Created] DATETIME NOT null DEFAULT GETDATE(),
	[Modified] DATETIME,
	[Deleted] DATETIME,
    CONSTRAINT [PK_VehicleTypes] PRIMARY KEY ([Id])
);
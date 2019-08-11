CREATE TABLE [VehicleParticipationTypes] (
    [Id] bigint NOT NULL IDENTITY,
    [Description] nvarchar(max) NULL,
	[Created] DATETIME NOT null DEFAULT GETDATE(),
	[Modified] DATETIME,
	[Deleted] DATETIME,
    CONSTRAINT [PK_VehicleParticipationTypes] PRIMARY KEY ([Id])
);
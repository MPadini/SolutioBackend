CREATE TABLE [Vehicles] (
    [Id] bigint NOT NULL IDENTITY,
    [Patent] nvarchar(max) NOT NULL,
    [VehicleTypeId] bigint NOT NULL,
    [VehicleModel] nvarchar(max) NULL,
    [VehicleManufacturer] nvarchar(max) NULL,
    [DamageDetail] nvarchar(max) NULL,
	[InsuranceCompany] nvarchar(max) NULL,
    [HaveFullCoverage] bit NULL DEFAULT 0,
    [Franchise] decimal(18,2) NULL,
	[Created] DATETIME NOT null DEFAULT GETDATE(),
	[Modified] DATETIME,
	[Deleted] DATETIME,
    CONSTRAINT [PK_Vehicles] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Vehicles_VehicleTypes_VehicleTypeId] FOREIGN KEY ([VehicleTypeId]) REFERENCES [VehicleTypes] ([Id])
);

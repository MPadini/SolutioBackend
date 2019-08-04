CREATE TABLE [Vehicles] (
    [Id] bigint NOT NULL IDENTITY,
    [Patent] nvarchar(max) NOT NULL,
    [VehicleTypeId] bigint NOT NULL,
    [VehicleModelId] bigint NOT NULL,
    CONSTRAINT [PK_Vehicles] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Vehicles_VehicleModels_VehicleModelId] FOREIGN KEY ([VehicleModelId]) REFERENCES [VehicleModels] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Vehicles_VehicleTypes_VehicleTypeId] FOREIGN KEY ([VehicleTypeId]) REFERENCES [VehicleTypes] ([Id]) ON DELETE CASCADE
);

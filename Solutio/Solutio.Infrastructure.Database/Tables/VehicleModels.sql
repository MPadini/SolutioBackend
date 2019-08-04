CREATE TABLE [VehicleModels] (
    [Id] bigint NOT NULL IDENTITY,
    [Description] nvarchar(max) NULL,
    CONSTRAINT [PK_VehicleModels] PRIMARY KEY ([Id])
);
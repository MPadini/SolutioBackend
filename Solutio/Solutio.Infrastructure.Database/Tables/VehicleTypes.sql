CREATE TABLE [VehicleTypes] (
    [Id] bigint NOT NULL IDENTITY,
    [Description] nvarchar(max) NULL,
    CONSTRAINT [PK_VehicleTypes] PRIMARY KEY ([Id])
);
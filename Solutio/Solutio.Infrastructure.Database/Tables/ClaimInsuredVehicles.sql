CREATE TABLE [ClaimInsuredVehicles] (
    [VehicleId] bigint NOT NULL,
    [ClaimId] bigint NOT NULL,
    [Created] datetime2 NOT NULL,
    [Modified] datetime2 NULL,
    [Deleted] datetime2 NULL,
    CONSTRAINT [PK_ClaimInsuredVehicles] PRIMARY KEY ([VehicleId], [ClaimId]),
    CONSTRAINT [FK_ClaimInsuredVehicles_Claims_ClaimId] FOREIGN KEY ([ClaimId]) REFERENCES [Claims] ([Id]),
    CONSTRAINT [FK_ClaimInsuredVehicles_Vehicles_VehicleId] FOREIGN KEY ([VehicleId]) REFERENCES [Vehicles] ([Id])
);
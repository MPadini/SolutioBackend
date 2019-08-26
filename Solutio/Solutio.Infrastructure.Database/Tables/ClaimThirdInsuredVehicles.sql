
CREATE TABLE [ClaimThirdInsuredVehicles] (
    [VehicleId] bigint NOT NULL,
    [ClaimId] bigint NOT NULL,
    [Created] datetime NOT NULL DEFAULT GetDate(),
    [Modified] datetime NULL,
    [Deleted] datetime NULL,
    CONSTRAINT [PK_ClaimThirdInsuredVehicles] PRIMARY KEY ([VehicleId], [ClaimId]),
    CONSTRAINT [FK_ClaimThirdInsuredVehicles_Claims_ClaimId] FOREIGN KEY ([ClaimId]) REFERENCES [Claims] ([Id]),
    CONSTRAINT [FK_ClaimThirdInsuredVehicles_Vehicles_VehicleId] FOREIGN KEY ([VehicleId]) REFERENCES [Vehicles] ([Id])
);
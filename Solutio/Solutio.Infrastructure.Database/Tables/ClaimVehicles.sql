CREATE TABLE [ClaimVehicles] (
    [VehicleId] bigint NOT NULL,
    [ClaimId] bigint NOT NULL,
    [VehicleParticipationTypeId] bigint NOT NULL,
	[Created] DATETIME NOT null DEFAULT GETDATE(),
	[Modified] DATETIME,
	[Deleted] DATETIME,
    CONSTRAINT [PK_ClaimVehicles] PRIMARY KEY ([VehicleId], [ClaimId]),
    CONSTRAINT [FK_ClaimVehicles_Claims_ClaimId] FOREIGN KEY ([ClaimId]) REFERENCES [Claims] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ClaimVehicles_Vehicles_VehicleId] FOREIGN KEY ([VehicleId]) REFERENCES [Vehicles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ClaimVehicles_VehicleParticipationTypes_VehicleParticipationTypeId] FOREIGN KEY ([VehicleParticipationTypeId]) REFERENCES [VehicleParticipationTypes] ([Id]) ON DELETE CASCADE
);
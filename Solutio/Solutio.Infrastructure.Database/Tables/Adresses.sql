CREATE TABLE [Adresses] (
    [Id] bigint NOT NULL IDENTITY,
    [CityId] bigint NOT NULL,
    [ProvinceId] bigint NOT NULL,
    [Street] nvarchar(1000) NULL,
    [Number] nvarchar(100) NULL,
	[Lng] nvarchar(100) NULL,
	[Lat] nvarchar(100) NULL,
    [Intersection] nvarchar(max) NULL,
	[Created] datetime NOT NULL DEFAULT GetDate(),
    [Modified] datetime NULL,
    [Deleted] datetime NULL,
    CONSTRAINT [PK_Adresses] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Adresses_Cities_CityId] FOREIGN KEY ([CityId]) REFERENCES [Cities] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Adresses_Provinces_ProvinceId] FOREIGN KEY ([ProvinceId]) REFERENCES [Provinces] ([Id]) ON DELETE CASCADE
);
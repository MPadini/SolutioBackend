﻿CREATE TABLE [dbo].[Cities]
(
	[Id] BIGINT NOT NULL PRIMARY KEY IDENTITY,
	[Name] VARCHAR(50) NOT NULL,
	[ProvinceId] BIGINT NOT NULL,
	[Created] DATETIME NOT null DEFAULT GETDATE(),
	[Modified] DATETIME,
	[Deleted] DATETIME  
)

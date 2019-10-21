SET IDENTITY_INSERT [dbo].[AspNetRoles] ON
GO
--DEFINO LA TABLA DESTINO (TARGET)
MERGE INTO [dbo].[AspNetRoles] AS Target

USING (VALUES
--¨CARGO DATOS PARA MERGE (ORIGEN)
	(1, N'ADMIN',N'ADMIN'),
	(2, N'PRODUCTOR',N'PRODUCTOR')
) AS Source([RolesId], [Name],[NormalizedName]) -- > AGREGAR COLUMNAS 
ON Target.[Id] = [RolesId] -- > CONDICIÓN PARA SABER SI HAY MATCH

WHEN MATCHED THEN
	UPDATE SET [Name] = Source.[Name],
			[NormalizedName] = Source.[NormalizedName]

WHEN NOT MATCHED BY TARGET THEN
	INSERT ([Id],
			[Name],
			[NormalizedName])
	VALUES (Source.[RolesId],
			Source.[Name],
			Source.[NormalizedName])
  
WHEN NOT MATCHED BY Source THEN
    DELETE;
GO

SET IDENTITY_INSERT [AspNetRoles] OFF
GO
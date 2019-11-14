SET IDENTITY_INSERT [dbo].[InsuranceCompanies] ON
GO
--DEFINO LA TABLA DESTINO (TARGET)
MERGE INTO [dbo].[InsuranceCompanies] AS Target

USING (VALUES
--¨CARGO DATOS PARA MERGE (ORIGEN)
	(1, N'La Caja', N'Domicilio de prueba'),
	(2, N'Mapfre', N'Domicilio de prueba 2222 centro')
) AS Source([InsuranceCompaniesId], [Name], [Adress]) -- > AGREGAR COLUMNAS 
ON Target.[Id] = [InsuranceCompaniesId] -- > CONDICIÓN PARA SABER SI HAY MATCH

WHEN MATCHED THEN
	UPDATE SET [Name] = Source.[Name],
		[Adress] = Source.[Adress]

WHEN NOT MATCHED BY TARGET THEN
	INSERT ([Id],
			[Name],
			[Adress])
	VALUES (Source.[InsuranceCompaniesId],
			Source.[Name],
			Source.[Adress])

WHEN NOT MATCHED BY Source THEN
    DELETE;
GO

SET IDENTITY_INSERT [InsuranceCompanies] OFF
GO
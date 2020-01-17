SET IDENTITY_INSERT [dbo].[ClaimOfferStates] ON
GO
--DEFINO LA TABLA DESTINO (TARGET)
MERGE INTO [dbo].[ClaimOfferStates] AS Target

USING (VALUES
--¨CARGO DATOS PARA MERGE (ORIGEN)

	--PRIMER BUCLE: ALTA DEL RECLAMO
	(1, N'Borrador'),
	(2, N'Informado'),
	(3, N'Ofrecimiento Aceptado'),
	(4, N'Ofrecimiento Rechazado'),
	(5, N'Pendiente de pago'),
	(6, N'Firmar Convenio'),
	(7, N'Convenio Firmado')
) AS Source([ClaimOfferStateId], [Description]) -- > AGREGAR COLUMNAS 
ON Target.[Id] = [ClaimOfferStateId] -- > CONDICIÓN PARA SABER SI HAY MATCH

WHEN MATCHED THEN
	UPDATE SET [Description] = Source.[Description]

WHEN NOT MATCHED BY TARGET THEN
	INSERT ([Id],
			[Description])
	VALUES (Source.[ClaimOfferStateId],
			Source.[Description])
 
WHEN NOT MATCHED BY Source THEN
    DELETE;
GO

SET IDENTITY_INSERT [ClaimOfferStates] OFF
GO
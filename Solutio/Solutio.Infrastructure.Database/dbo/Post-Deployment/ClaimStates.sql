SET IDENTITY_INSERT [dbo].[ClaimStates] ON
GO
--DEFINO LA TABLA DESTINO (TARGET)
MERGE INTO [dbo].[ClaimStates] AS Target

USING (VALUES
--¨CARGO DATOS PARA MERGE (ORIGEN)
	(1, N'Borrador',					24),
	(2, N'En Revisión',					24),
	(3, N'Esperando Denuncia',			24),
	(4, N'Pendiente de Presentación',	24),
	(5, N'Presentado',					24),
	(6, N'Esperando Acción',			24),
	(7, N'Cerrado',						24),
	(8, N'A Juicio',					24),
	(9, N'Rechazado',					24),
	(10, N'Desistido',					24),
	(11, N'Desestimado',				24)
) AS Source([ClaimStatesId], [Description],[MaximumTimeAllowed]) -- > AGREGAR COLUMNAS 
ON Target.[Id] = [ClaimStatesId] -- > CONDICIÓN PARA SABER SI HAY MATCH

-- EL MERGE PLANTEA RESOLVER 3 CASOS BASICOS

-- 1) MATCH (ORIGEN.ID = DESTINO.ID), ACTUALIZO VALORES DE COLUMNAS EN DESTINO CON LOS DATOS DE ORIGEN
WHEN MATCHED THEN
	UPDATE SET [Description] = Source.[Description],
			[MaximumTimeAllowed] = Source.[MaximumTimeAllowed]

-- 2) NO MATCH TARGET (Existe en ORIGEN pero no en DESTINO -> INSERTO en DESTINO)
WHEN NOT MATCHED BY TARGET THEN
	INSERT ([Id],
			[Description],
			[MaximumTimeAllowed])
	VALUES (Source.[ClaimStatesId],
			Source.[Description],
			Source.[MaximumTimeAllowed])

-- 3) NO MATCH SOURCE (Existe en DESTINO pero no en ORIGEN). Hay 2 OPCIONES

-- 3.1) CONSERVAR LOS DATOS DE DESTINO (NO SE HACE NADA)
-- 3.2) BORRAR LOS DATOS DEL DESTINO QUE NO ESTAN EN EL ORIGEN
--(VALIDAR Y SI LOS DATOS DE DESTINO SE TIENEN QUE BORRAR, DESCOMENTAR EL CODIGO DE ABAJO    
WHEN NOT MATCHED BY Source THEN
    DELETE;
GO

SET IDENTITY_INSERT [ClaimStates] OFF
GO
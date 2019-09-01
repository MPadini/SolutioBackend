SET IDENTITY_INSERT [dbo].[ClaimStateConfigurations] ON
GO
--DEFINO LA TABLA DESTINO (TARGET)
MERGE INTO [dbo].[ClaimStateConfigurations] AS Target

USING (VALUES
--¨CARGO DATOS PARA MERGE (ORIGEN)
	(1, 1, 2),
	(2, 2, 1),
	(3, 2, 3),
	(4, 3, 4),
	(5, 3, 6),
	(6, 5, 3),
	(7, 6, 5),
	(8, 6, 7),
	(9, 7, 6),
	(10, 7, 8),
	(11, 8, 7),
	(12, 8, 9)
) AS Source([ClaimStatesConfigurationsId], ParentClaimStateId,AllowedStateId) -- > AGREGAR COLUMNAS 
ON Target.[Id] = [ClaimStatesConfigurationsId] -- > CONDICIÓN PARA SABER SI HAY MATCH

-- EL MERGE PLANTEA RESOLVER 3 CASOS BASICOS

-- 1) MATCH (ORIGEN.ID = DESTINO.ID), ACTUALIZO VALORES DE COLUMNAS EN DESTINO CON LOS DATOS DE ORIGEN
WHEN MATCHED THEN
	UPDATE SET ParentClaimStateId = Source.ParentClaimStateId,
			AllowedStateId = Source.AllowedStateId

-- 2) NO MATCH TARGET (Existe en ORIGEN pero no en DESTINO -> INSERTO en DESTINO)
WHEN NOT MATCHED BY TARGET THEN
	INSERT ([Id],
			ParentClaimStateId,
			AllowedStateId)
	VALUES (Source.[ClaimStatesConfigurationsId],
			Source.ParentClaimStateId,
			Source.AllowedStateId)

-- 3) NO MATCH SOURCE (Existe en DESTINO pero no en ORIGEN). Hay 2 OPCIONES

-- 3.1) CONSERVAR LOS DATOS DE DESTINO (NO SE HACE NADA)
-- 3.2) BORRAR LOS DATOS DEL DESTINO QUE NO ESTAN EN EL ORIGEN
--(VALIDAR Y SI LOS DATOS DE DESTINO SE TIENEN QUE BORRAR, DESCOMENTAR EL CODIGO DE ABAJO    
WHEN NOT MATCHED BY Source THEN
    DELETE;
GO

SET IDENTITY_INSERT [ClaimStateConfigurations] OFF
GO
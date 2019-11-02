SET IDENTITY_INSERT [dbo].[ClaimStateConfigurations] ON
GO
--DEFINO LA TABLA DESTINO (TARGET)
MERGE INTO [dbo].[ClaimStateConfigurations] AS Target

USING (VALUES
--¨CARGO DATOS PARA MERGE (ORIGEN)
	--BORRADOR
	(1, 1, 2),
	(2, 1, 1),
	--EN REVISION
	(3, 2, 2),
	(4, 2, 1),
	(5, 2, 3),
	--ESPERANDO DENUNCIA
	(6, 3, 3),
	(7, 3, 4),
	--PENDIENTE DE PRESENTACION
	(8, 4, 6),
	(9, 4, 4),
	(10, 4, 5),
	--PRESENTADO
	(11, 5, 5),
	(12, 5, 6),
	(13, 5, 7),
	--ESPERANDO ACCION
	(14, 6, 6),
	(15, 6, 4),
	--CERRADO
	(16, 7, 7),
	--A JUICIO
	(17, 8, 8),
	--RECHAZADO
	(18, 8, 8),
	--DESISTIDO
	(19, 8, 8),
	--DESESTIMADO
	(20, 8, 8)
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
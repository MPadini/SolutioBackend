﻿SET IDENTITY_INSERT [dbo].[FileTypes] ON
GO
--DEFINO LA TABLA DESTINO (TARGET)
MERGE INTO [dbo].[FileTypes] AS Target

USING (VALUES
--¨CARGO DATOS PARA MERGE (ORIGEN)
	(1, N'Archivo varios'),
	(2, N'Dni'),
	(3, N'Denuncia'),
	(4, N'Reclamo Firmado'),
	(5, N'Convenio'),
	(6, N'ConvenioFirmado')
) AS Source([FileTypesId], [Description]) -- > AGREGAR COLUMNAS 
ON Target.[Id] = [FileTypesId] -- > CONDICIÓN PARA SABER SI HAY MATCH

-- EL MERGE PLANTEA RESOLVER 3 CASOS BASICOS

-- 1) MATCH (ORIGEN.ID = DESTINO.ID), ACTUALIZO VALORES DE COLUMNAS EN DESTINO CON LOS DATOS DE ORIGEN
WHEN MATCHED THEN
	UPDATE SET [Description] = Source.[Description]

-- 2) NO MATCH TARGET (Existe en ORIGEN pero no en DESTINO -> INSERTO en DESTINO)
WHEN NOT MATCHED BY TARGET THEN
	INSERT ([Id],
			[Description])
	VALUES (Source.[FileTypesId],
			Source.[Description])

-- 3) NO MATCH SOURCE (Existe en DESTINO pero no en ORIGEN). Hay 2 OPCIONES

-- 3.1) CONSERVAR LOS DATOS DE DESTINO (NO SE HACE NADA)
-- 3.2) BORRAR LOS DATOS DEL DESTINO QUE NO ESTAN EN EL ORIGEN
--(VALIDAR Y SI LOS DATOS DE DESTINO SE TIENEN QUE BORRAR, DESCOMENTAR EL CODIGO DE ABAJO    
WHEN NOT MATCHED BY Source THEN
    DELETE;
GO

SET IDENTITY_INSERT [FileTypes] OFF
GO
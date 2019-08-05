/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

SET IDENTITY_INSERT [dbo].[ClaimStates] ON
GO
--DEFINO LA TABLA DESTINO (TARGET)
MERGE INTO [dbo].[ClaimStates] AS Target

USING (VALUES
--¨CARGO DATOS PARA MERGE (ORIGEN)
	(1, N'Borrador'),
	(2, N'En Revisión'),
	(3, N'Presentado/Contestado'),
	(4, N'Esperando Acción'),
	(5, N'En Monitoreo'),
	(6, N'Ofrecido/Reconsiderado'),
	(7, N'Aceptado'),
	(8, N'Pendiente de Pago'),
	(9, N'Cerrado')
) AS Source([ClaimStatesId], [Description]) -- > AGREGAR COLUMNAS 
ON Target.[Id] = [ClaimStatesId] -- > CONDICIÓN PARA SABER SI HAY MATCH

-- EL MERGE PLANTEA RESOLVER 3 CASOS BASICOS

-- 1) MATCH (ORIGEN.ID = DESTINO.ID), ACTUALIZO VALORES DE COLUMNAS EN DESTINO CON LOS DATOS DE ORIGEN
WHEN MATCHED THEN
	UPDATE SET [Description] = Source.[Description]

-- 2) NO MATCH TARGET (Existe en ORIGEN pero no en DESTINO -> INSERTO en DESTINO)
WHEN NOT MATCHED BY TARGET THEN
	INSERT ([Id],
			[Description])
	VALUES (Source.[ClaimStatesId],
			Source.[Description])

-- 3) NO MATCH SOURCE (Existe en DESTINO pero no en ORIGEN). Hay 2 OPCIONES

-- 3.1) CONSERVAR LOS DATOS DE DESTINO (NO SE HACE NADA)
-- 3.2) BORRAR LOS DATOS DEL DESTINO QUE NO ESTAN EN EL ORIGEN
--(VALIDAR Y SI LOS DATOS DE DESTINO SE TIENEN QUE BORRAR, DESCOMENTAR EL CODIGO DE ABAJO    
WHEN NOT MATCHED BY Source THEN
    DELETE;
GO

SET IDENTITY_INSERT [ClaimStates] OFF
GO

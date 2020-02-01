SET IDENTITY_INSERT [dbo].[ClaimStates] ON
GO
--DEFINO LA TABLA DESTINO (TARGET)
MERGE INTO [dbo].[ClaimStates] AS Target

USING (VALUES
--¨CARGO DATOS PARA MERGE (ORIGEN)

	--PRIMER BUCLE: ALTA DEL RECLAMO
	(11, N'Borrador',								24), --PRODUCTOR DE SEGUROS
	(12, N'En Revisión',								24),
	(13, N'Rechazado Mejores Datos (UI)',			24), --PRODUCTOR DE SEGUROS
	(81, N'Rechazado',								24),

	--SEGUNDO BUCLE: PRESENTACIÓN EN COMPAÑIA
	(21, N'Esperando Denuncia',						24),
	(22, N'Pendiente de Presentación',				24),
	(23, N'Presentado',								24),
	(24, N'Rechazado Mejores Datos (COMPAÑIA)',		24), --PRODUCTOR DE SEGUROS
	(82, N'Desestimado',							24),
	
	--TERCER BUCLE: NEGOCIACIÓN
	(31, N'Nuevo Ofrecimiento',						24), --PRODUCTOR DE SEGUROS
	(32, N'Ofrecimiento Rechazado',					24),
	(33, N'Esperando Ofrecimiento',					24),
	(84, N'A Juicio',								24),

	--CUARTO BUCLE: TRÁMITAR EL PAGO
	(41, N'Ofrecimiento Aceptado',					24),
	(42, N'Firmar Convenio',						24), --PRODUCTOR DE SEGUROS
	(43, N'Convenio Firmado',						24),
	(44, N'Pendiente de Pago',						24),
	(100, N'Cerrado',								24),

	--SE DA DE BAJA EL RECLAMO
	(83, N'Desistido',								24)

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
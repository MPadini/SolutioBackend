SET IDENTITY_INSERT [dbo].[ClaimStateConfigurations] ON
GO
--DEFINO LA TABLA DESTINO (TARGET)
MERGE INTO [dbo].[ClaimStateConfigurations] AS Target

USING (VALUES
--¨CARGO DATOS PARA MERGE (ORIGEN)
	--BORRADOR
	(1, 11, 11),
	(2, 11, 12),

	--EN REVISION
	(3, 12, 11),
	(4, 12, 12),
	(5, 12, 13),
	(6, 12, 21),
	(7, 12, 81),

	--RECHAZADO MEJORES DATOS (UI)
	(8, 13, 13),
	(9, 13, 12),
	(10, 13, 11),
	(11, 13, 21),
	(12, 13, 81),

	--RECHAZADO
	(13, 81, 81),
	(14, 81, 12),
	(15, 81, 11),

	--ESPERANDO DENUNCIA
	(16, 21, 21),
	(17, 21, 22),
	(18, 21, 82),
	(19, 21, 13),
	(20, 21, 12),

	--PENDIENTE PRESENTACION
	(21, 22, 22),
	(22, 22, 21),
	(23, 22, 12),
	(24, 22, 13),
	(25, 22, 82),
	(26, 22, 81),
	
	--PRESENTADO
	(27, 23, 23),
	(28, 23, 24), 
	(29, 23, 31), 
	(30, 23, 21),
	(31, 23, 12),
	(32, 23, 13),
	(33, 23, 82),
	(34, 23, 81),

	--RECHAZADO MEJORES DATOS (COMPAÑIA)
	(35, 24, 24),
	(36, 24, 22),
	(37, 24, 21),
	(38, 24, 12),
	(39, 24, 13),
	(40, 24, 82),
	(41, 24, 81),

	--DESESTIMADO
	(42, 82, 82),
	(43, 82, 21),
	(44, 82, 12),
	(45, 82, 13),
	(46, 82, 81),
		
	--NUEVO OFRECIMIENTO
	(47, 31, 31),
	(48, 31, 32),
	(49, 31, 41),
	(50, 31, 81),
	(51, 31, 82),
	(53, 31, 12),
	(55, 31, 23),
	   	
	--OFRECIMIENTO RECHAZADO
	(56, 32, 32),
	(57, 32, 33),
	(58, 32, 84),
	(59, 32, 31),
	(60, 31, 81),
	(61, 31, 82),

	--ESPERANDO OFRECIMIENTO
	(62, 33, 33),
	(63, 33, 31),
	(64, 33, 23),
	
	--A JUICIO
	(65, 84, 84),
	(66, 84, 32),
	(67, 84, 31),
	(68, 84, 41),

	--OFRECIMIENTO ACEPTADO
	(69, 41, 41),
	(70, 41, 42),
	(71, 41, 44),
	(72, 41, 31),
	(73, 41, 32),
	(74, 41, 33),

	--FIRMAR CONVENIO
	(75, 42, 42),
	(76, 42, 43),
	(77, 42, 31),
	(78, 42, 32),
	(79, 42, 33),
	(80, 42, 41),

	--CONVENIO FIRMADO
	(81, 43, 43),
	(82, 43, 44),
	(83, 43, 31),
	(84, 43, 32),
	(85, 43, 33),
	(86, 43, 41),
	(87, 43, 42),
	
	--PENDIENTE DE PAGO
	(88, 44, 44),
	(89, 44, 100),
	(90, 44, 41),
	(91, 44, 42),
	
	--CERRADO
	(92, 100, 100),
	(93, 100, 44),
	(94, 100, 43),

	--DESISTIDO
	(95, 83, 83)
	

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
﻿SET IDENTITY_INSERT [dbo].[InsuranceCompanies] ON
GO
--DEFINO LA TABLA DESTINO (TARGET)
MERGE INTO [dbo].[InsuranceCompanies] AS Target

USING (VALUES
--¨CARGO DATOS PARA MERGE (ORIGEN)
	(1,N'ACE AMERICAN INSURANCE COMPANY (SUCURSAL ARGENTINA)', '' ), 
(2,N'AFIANZADORA LATINOAMERICANA COMPAÑÍA DE SEGUROS S.A.', '' ), 
(3,N'AGROSALTA COOPERATIVA DE SEGUROS LIMITADA', '' ), 
(4,N'ALBA COMPAÑÍA ARGENTINA DE SEGUROS SOCIEDAD ANONIMA', '' ), 
(5,N'ALLIANZ ARGENTINA COMPAÑIA DE SEGUROS S.A.', '' ), 
(6,N'ALLIANZ RE ARGENTINA S.A.', '' ), 
(7,N'AMERICAN HOME ASSURANCE COMPANY (SUCURSAL ARGENTINA)', '' ), 
(8,N'ANTARTIDA COMPAÑÍA ARGENTINA DE SEGUROS SOCIEDAD ANÓNIMA', '' ), 
(9,N'ANTICIPAR COMPAÑÍA DE SEGUROS S.A.', '' ), 
(10,N'ARGOS COMPAÑÍA ARGENTINA DE SEGUROS GENERALES SOCIEDAD ANÓNIMA', '' ), 
(11,N'ARGOS MUTUAL DE SEGUROS DEL TRANSPORTE PÚBLICO DE PASAJEROS', '' ), 
(12,N'ASEGURADORA ARGENTINA DEL ATLÁNTICO S.A.', '' ), 
(13,N'ASEGURADORA DE CREDITOS Y GARANTIAS SOCIEDAD ANONIMA', '' ), 
(14,N'ASEGURADORA DEL FINISTERRE COMPAÑÍA ARGENTINA DE SEGUROS S.A.', '' ), 
(15,N'ASEGURADORA TOTAL MOTOVEHICULAR S.A.', '' ), 
(16,N'ASEGURADORES ARGENTINOS COMPAÑIA DE REASEGUROS S.A.', '' ), 
(17,N'ASEGURADORES DE CAUCIONES SOCIEDAD ANONIMA COMPAÑIA DE SEGUROS', '' ), 
(18,N'ASOCIACION MUTUAL DAN', '' ), 
(19,N'ASOCIART S.A. ASEGURADORA DE RIESGOS DEL TRABAJO', '' ), 
(20,N'ASSEKURANSA COMPAÑÍA DE SEGUROS SOCIEDAD ANÓNIMA', '' ), 
(21,N'ASSURANT ARGENTINA COMPAÑÍA DE SEGUROS SOCIEDAD ANÓNIMA', '' ), 
(22,N'ASV ARGENTINA SALUD, VIDA Y PATRIMONIALES COMPAÑIA DE SEGUROS S.A.', '' ), 
(23,N'BBVA CONSOLIDAR SEGUROS S.A.', '' ), 
(24,N'BENEFICIO S.A. COMPAÑIA DE SEGUROS', '' ), 
(25,N'BERKLEY ARGENTINA DE REASEGUROS S.A.', '' ), 
(26,N'BERKLEY INTERNATIONAL ASEGURADORA DE RIESGOS DEL TRABAJO SOCIEDAD ANONIMA', '' ), 
(27,N'BERKLEY INTERNATIONAL SEGUROS SOCIEDAD ANONIMA', '' ), 
(28,N'BHN SEGUROS GENERALES S.A.', '' ), 
(29,N'BHN VIDA S.A.', '' ), 
(30,N'BINARIA SEGUROS DE RETIRO S.A.', '' ), 
(31,N'BINARIA SEGUROS DE VIDA S.A.', '' ), 
(32,N'BONACORSI SEGUROS DE PERSONAS S.A.', '' ), 
(33,N'BOSTON COMPAÑÍA ARGENTINA DE SEGUROS SOCIEDAD ANONIMA', '' ), 
(34,N'BRADESCO ARGENTINA DE SEGUROS S.A.', '' ), 
(35,N'CAJA DE PREVISIÓN Y SEGURO MÉDICO DE LA PROVINCIA DE BS.AS', '' ), 
(36,N'CAJA DE SEGUROS S.A.', '' ), 
(37,N'CAJA POPULAR DE AHORROS DE LA PROVINCIA DE TUCUMAN', '' ), 
(38,N'CALEDONIA ARGENTINA COMPAÑIA DE SEGUROS SOCIEDAD ANONIMA', '' ), 
(39,N'CAMINOS PROTEGIDOS COMPAÑÍA DE SEGUROS S.A.', '' ), 
(40,N'CARDIF SEGUROS S.A.', '' ), 
(41,N'CARUSO COMPAÑÍA ARGENTINA DE SEGUROS SOCIEDAD ANÓNIMA', '' ), 
(42,N'CERTEZA COMPAÑIA DE SEGUROS S.A.', '' ), 
(43,N'CESCE ARGENTINA S.A. SEGUROS DE CREDITO Y GARANTÍAS', '' ), 
(44,N'CHUBB SEGUROS ARGENTINA S.A.', '' ), 
(45,N'CNP ASSURANCES COMPAÑIA DE SEGUROS S.A.', '' ), 
(46,N'COLON COMPAÑÍA DE SEGUROS SOCIEDAD ANONIMA', '' ), 
(47,N'COMARSEG COMPAÑIA ARGENTINA DE SEGUROS S.A.', '' ), 
(48,N'COMPAGNIE FRANCAISE D´ASSURANCE POUR LE COMMERCE EXTERIEUR (SUCURSAL ARGENTINA)', '' ), 
(49,N'COMPAÑIA ARGENTINA DE SEGUROS LATITUD SUR SOCIEDAD ANONIMA', '' ), 
(50,N'COMPAÑIA ARGENTINA DE SEGUROS VICTORIA SOCIEDAD ANONIMA', '' ), 
(51,N'COMPAÑÍA ASEGURADORA DEL SUR S.A.', '' ), 
(52,N'COMPAÑÍA DE SEGUROS EL NORTE SOCIEDAD ANONIMA', '' ), 
(53,N'COMPAÑIA DE SEGUROS EUROAMERICA S.A.', '' ), 
(54,N'COMPAÑIA DE SEGUROS INSUR S.A.', '' ), 
(55,N'COMPAÑIA DE SEGUROS LA MERCANTIL ANDINA SOCIEDAD ANONIMA', '' ), 
(56,N'COMPAÑÍA DE SEGUROS MAÑANA SOCIEDAD ANÓNIMA', '' ), 
(57,N'COMPAÑÍA MERCANTIL ASEGURADORA SOCIEDAD ANONIMA ARGENTINA DE SEGUROS', '' ), 
(58,N'CONFLUENCIA COMPAÑIA DE SEGUROS S.A.', '' ), 
(59,N'COOPERACION MUTUAL PATRONAL SOCIEDAD MUTUAL DE SEGUROS GENERALES', '' ), 
(60,N'COOPERATIVA DE SEGUROS LUZ Y FUERZA LIMITADA', '' ), 
(61,N'COPAN COOPERATIVA DE SEGUROS LIMITADA', '' ), 
(62,N'COSENA SEGUROS S.A.', '' ), 
(63,N'CREDICOOP COMPAÑIA DE SEGUROS DE RETIRO S.A.', '' ), 
(64,N'CREDITO Y CAUCION S.A. COMPAÑÍA DE SEGUROS', '' ), 
(65,N'CRUZ SUIZA COMPAÑÍA DE SEGUROS S.A.', '' ), 
(66,N'DIGNA SEGUROS S.A.', '' ), 
(67,N'EDIFICAR SEGUROS SOCIEDAD ANÓNIMA', '' ), 
(68,N'EL PROGRESO SEGUROS SOCIEDAD ANONIMA', '' ), 
(69,N'EL SURCO COMPAÑÍA DE SEGUROS SOCIEDAD ANONIMA', '' ), 
(70,N'ESCUDO SEGUROS S.A.', '' ), 
(71,N'EVOLUCIÓN SEGUROS S.A.', '' ), 
(72,N'Experta Aseguradora de Riesgos del Trabajo SA', '' ), 
(73,N'EXPERTA SEGUROS DE RETIRO S.A.', '' ), 
(74,N'EXPERTA SEGUROS SOCIEDAD ANÓNIMA UNIPERSONAL', '' ), 
(75,N'FEDERACION PATRONAL REASEGUROS S.A.', '' ), 
(76,N'FEDERACION PATRONAL SEGUROS DE RETIRO S.A.', '' ), 
(77,N'FEDERACION PATRONAL SEGUROS S.A.', '' ), 
(78,N'FEDERADA COMPAÑÍA DE SEGUROS S.A.', '' ), 
(79,N'FIANZAS Y CREDITO S. A. COMPAÑIA DE SEGUROS', '' ), 
(80,N'FOMS CIA. ARGENTINA DE SEGUROS S.A.', '' ), 
(81,N'GALENO ASEGURADORA DE RIESGOS DEL TRABAJO S.A.', '' ), 
(82,N'GALENO SEGUROS S.A.', '' ), 
(83,N'GALICIA RETIRO COMPAÑIA DE SEGUROS S.A.', '' ), 
(84,N'GALICIA SEGUROS S.A.', '' ), 
(85,N'GARANTÍA MUTUAL DE SEGUROS DEL TRANSPORTE PÚBLICO DE PASAJEROS', '' ), 
(86,N'GESTION COMPAÑÍA ARGENTINA DE SEGUROS S.A.', '' ), 
(87,N'HAMBURGO COMPAÑIA DE SEGUROS SOCIEDAD ANONIMA', '' ), 
(88,N'HANSEATICA COMPAÑIA DE SEGUROS SOCIEDAD ANÓNIMA', '' ), 
(89,N'HDI SEGUROS S.A.', '' ), 
(90,N'HORIZONTE COMPAÑIA ARGENTINA DE SEGUROS GENERALES SOCIEDAD ANÓNIMA', '' ), 
(91,N'HSBC SEGUROS DE RETIRO (ARGENTINA) S.A.', '' ), 
(92,N'HSBC SEGUROS DE VIDA (ARGENTINA) S.A.', '' ), 
(93,N'INSTITUTO ASEGURADOR MERCANTIL COMPAÑIA ARGENTINA DE SEGUROS SOCIEDAD ANÓNIMA IAM', '' ), 
(94,N'INSTITUTO AUTÁRQUICO PROVINCIAL DEL SEGURO', '' ), 
(95,N'INSTITUTO AUTARQUICO PROVINCIAL DEL SEGURO DE ENTRE RIOS SEGURO DE RETIRO SOCIEDAD ANONIMA', '' ), 
(96,N'INSTITUTO DE SALTA COMPAÑÍA DE SEGUROS DE VIDA SOCIEDAD ANÓNIMA', '' ), 
(97,N'INSTITUTO DE SEGUROS DE JUJUY', '' ), 
(98,N'INSTITUTO DE SEGUROS SOCIEDAD ANÓNIMA', '' ), 
(99,N'INTÉGRITY SEGUROS ARGENTINA S.A.', '' ), 
(100,N'INTERNACIONAL COMPAÑÍA DE SEGUROS DE VIDA S.A.', '' ), 
(101,N'IRB BRASIL RESSEGUROS S.A. (SUCURSAL ARGENTINA)', '' ), 
(102,N'IÚNIGO ARGENTINA COMPAÑÍA DE SEGUROS SA', '' ), 
(103,N'JUNCAL COMPAÑÍA DE SEGUROS DE AUTOS Y PATRIMONIALES S.A.', '' ), 
(104,N'LA DULCE COOPERATIVA DE SEGUROS LIMITADA', '' ), 
(105,N'LA EQUITATIVA DEL PLATA SOCIEDAD ANONIMA DE SEGUROS', '' ), 
(106,N'LA ESTRELLA S.A. COMPAñíA DE SEGUROS DE RETIRO', '' ), 
(107,N'LA HOLANDO SUDAMERICANA COMPAÑIA DE SEGUROS SOCIEDAD ANONIMA', '' ), 
(108,N'LA MERIDIONAL COMPAÑÍA ARGENTINA DE SEGUROS SOCIEDAD ANÓNIMA', '' ), 
(109,N'LA NUEVA COOPERATIVA DE SEGUROS LIMITADA', '' ), 
(110,N'LA PERSEVERANCIA SEGUROS SOCIEDAD ANONIMA', '' ), 
(111,N'LA PREVISORA S.A. SEGUROS DE SEPELIO', '' ), 
(112,N'LA SEGUNDA ASEGURADORA DE RIESGOS DEL TRABAJO SOCIEDAD ANÓNIMA', '' ), 
(113,N'LA SEGUNDA COMPAÑIA DE SEGUROS DE PERSONAS SOCIEDAD ANÓNIMA', '' ), 
(114,N'LA SEGUNDA COOPERATIVA LIMITADA DE SEGUROS GENERALES', '' ), 
(115,N'LA SEGUNDA SEGUROS DE RETIRO SOCIEDAD ANONIMA', '' ), 
(116,N'LA TERRITORIAL VIDA Y SALUD COMPAÑÍA DE SEGUROS SOCIEDAD ANÓNIMA', '' ), 
(117,N'LATIN AMERICAN SEGUROS S.A.', '' ), 
(118,N'LIBRA COMPAÑIA ARGENTINA DE SEGUROS S.A.', '' ), 
(119,N'LIDER MOTOS COMPAÑÍA DE SEGUROS S.A.', '' ), 
(120,N'LIDERAR COMPAÑIA GENERAL DE SEGUROS S.A.', '' ), 
(121,N'MAPFRE ARGENTINA SEGUROS DE VIDA S.A.', '' ), 
(122,N'MAPFRE ARGENTINA SEGUROS S.A.', '' ), 
(123,N'MAPFRE RE COMPAÑIA DE REASEGUROS S.A. (SUCURSAL ARGENTINA)', '' ), 
(124,N'METLIFE SEGUROS DE RETIRO S.A.', '' ), 
(125,N'METLIFE SEGUROS S.A.', '' ), 
(126,N'METROPOL COMPAÑÍA ARGENTINA DE SEGUROS SOCIEDAD ANONIMA', '' ), 
(127,N'METROPOL SOCIEDAD DE SEGUROS MUTUOS', '' ), 
(128,N'MUTUAL DE EMPLEADOS Y OBREROS PETROLEROS PRIVADOS ART MUTUAL', '' ), 
(129,N'MUTUAL RIVADAVIA DE SEGUROS DEL TRANSPORTE PÚBLICO DE PASAJEROS', '' ), 
(130,N'N.S.A. SEGUROS GENERALES S.A.', '' ), 
(131,N'NACION REASEGUROS S.A.', '' ), 
(132,N'NACIÓN SEGUROS DE RETIRO S.A.', '' ), 
(133,N'NACIÓN SEGUROS S.A.', '' ), 
(134,N'NATIVA COMPAÑIA ARGENTINA DE SEGUROS S.A.', '' ), 
(135,N'NIVEL SEGUROS S.A.', '' ), 
(136,N'NOBLE COMPAÑÍA DE SEGUROS SOCIEDAD ANÓNIMA', '' ), 
(137,N'NRE COMPAÑÍA DE REASEGUROS S.A.', '' ), 
(138,N'OMINT ASEGURADORA DE RIESGOS DEL TRABAJO S.A.', '' ), 
(139,N'OMINT S.A. COMPAÑÍA DE SEGUROS', '' ), 
(140,N'OPCIÓN SEGUROS S.A.', '' ), 
(141,N'ORBIS COMPAÑÍA ARGENTINA DE SEGUROS S.A.', '' ), 
(142,N'ORÍGENES SEGUROS DE RETIRO S.A.', '' ), 
(143,N'Orígenes Seguros S.A.', '' ), 
(144,N'PACÍFICO COMPAÑÍA DE SEGUROS S.A.', '' ), 
(145,N'PARANÁ SOCIEDAD ANONIMA DE SEGUROS', '' ), 
(146,N'PEUGEOT CITROËN ARGENTINA COMPAÑÍA DE SEGUROS S.A.', '' ), 
(147,N'PIEVE SEGUROS SOCIEDAD ANONIMA', '' ), 
(148,N'PLENARIA SEGUROS S.A.', '' ), 
(149,N'POR VIDA SEGUROS SOCIEDAD ANONIMA', '' ), 
(150,N'PREVENCIÓN ASEGURADORA DE RIESGOS DEL TRABAJO S.A.', '' ), 
(151,N'PREVENCIÓN SEGUROS DE RETIRO SOCIEDAD ANÓNIMA', '' ), 
(152,N'PREVINCA SEGUROS SOCIEDAD ANONIMA', '' ), 
(153,N'PRODUCTORES DE FRUTAS ARGENTINAS COOPERATIVA DE SEGUROS LIMITADA', '' ), 
(154,N'PROTECCIÓN MUTUAL DE SEGUROS DEL TRANSPORTE PÚBLICO DE PASAJEROS', '' ), 
(155,N'PROVIDENCIA COMPAÑIA ARGENTINA DE SEGUROS S.A.', '' ), 
(156,N'PROVINCIA ASEGURADORA DE RIESGOS DEL TRABAJO S.A.', '' ), 
(157,N'PROVINCIA SEGUROS DE VIDA S. A.', '' ), 
(158,N'PROVINCIA SEGUROS SOCIEDAD ANONIMA', '' ), 
(159,N'PROYECCION SEGUROS DE RETIRO S.A.', '' ), 
(160,N'PRUDENCIA COMPAÑIA ARGENTINA DE SEGUROS GENERALES SOCIEDAD ANÓNIMA', '' ), 
(161,N'PRUDENTIAL SEGUROS S.A.', '' ), 
(162,N'PUNTO SUR SOCIEDAD ARGENTINA DE REASEGUROS S.A.', '' ), 
(163,N'QUALIA COMPAÑÍA DE SEGUROS S.A.', '' ), 
(164,N'REASEGURADORES ARGENTINOS S.A.', '' ), 
(165,N'RECONQUISTA ASEGURADORA DE RIESGOS DEL TRABAJO SOCIEDAD ANONIMA', '' ), 
(166,N'REUNION RE COMPAÑIA DE REASEGUROS S.A.', '' ), 
(167,N'RÍO URUGUAY COOPERATIVA DE SEGUROS LIMITADA', '' ), 
(168,N'ROYAL & SUN ALLIANCE INSURANCE PLC (SUCURSAL ARGENTINA)', '' ), 
(169,N'SAN CRISTÓBAL SEGURO DE RETIRO SOCIEDAD ANONIMA', '' ), 
(170,N'SAN CRISTÓBAL SOCIEDAD MUTUAL DE SEGUROS GENERALES', '' ), 
(171,N'SAN GERMAN SEGUROS S.A.', '' ), 
(172,N'SAN MARINO COMPAÑÍA DE SEGUROS SOCIEDAD ANÓNIMA', '' ), 
(173,N'SAN PATRICIO SEGUROS DE VIDA Y SALUD S.A.', '' ), 
(174,N'SANCOR COOPERATIVA DE SEGUROS LIMITADA', '' ), 
(175,N'SANTA LUCÍA S.A. COMPAÑÍA DE SEGUROS', '' ), 
(176,N'SANTÍSIMA TRINIDAD SEGUROS DE VIDA SOCIEDAD ANONIMA', '' ), 
(177,N'SCOR GLOBAL P&C SE (SUCURSAL ARGENTINA)', '' ), 
(178,N'SEGURCOOP COOPERATIVA DE REASEGUROS LIMITADA', '' ), 
(179,N'SEGURCOOP COOPERATIVA DE SEGUROS LIMITADA', '' ), 
(180,N'SEGUROMETAL COOPERATIVA DE SEGUROS LIMITADA', '' ), 
(181,N'SEGUROS BERNARDINO RIVADAVIA COOPERATIVA LIMITADA', '' ), 
(182,N'SEGUROS MÉDICOS SOCIEDAD ANONIMA', '' ), 
(183,N'SEGUROS SURA S.A', '' ), 
(184,N'SENTIR SEGUROS SOCIEDAD ANONIMA', '' ), 
(185,N'SMG COMPAÑIA ARGENTINA DE SEGUROS S.A.', '' ), 
(186,N'SMG LIFE COMPAÑIA DE SEGUROS DE RETIRO S.A.', '' ), 
(187,N'SMG LIFE SEGUROS DE VIDA S.A.', '' ), 
(188,N'SMG RE ARGENTINA S.A.', '' ), 
(189,N'SMSV COMPAÑIA ARGENTINA DE SEGUROS S.A.', '' ), 
(190,N'SOL NACIENTE SEGUROS SOCIEDAD ANONIMA', '' ), 
(191,N'SOLVENCIA COMPAÑÍA DE SEGUROS DE RETIRO SOCIEDAD ANÓNIMA.', '' ), 
(192,N'STARR INDEMNITY & LIABILITY COMPANY, SUCURSAL ARGENTINA, DE SEGUROS', '' ), 
(193,N'SUMICLI ASOCIACION MUTUAL DE SEGUROS', '' ), 
(194,N'SUPERVIELLE SEGUROS S.A.', '' ), 
(195,N'SWISS MEDICAL ART S.A.', '' ), 
(196,N'TESTIMONIO COMPAÑÍA DE SEGUROS S.A.', '' ), 
(197,N'TPC COMPAÑIA DE SEGUROS S.A.', '' ), 
(198,N'TRAYECTORIA COMPAÑÍA DE SEGUROS S.A.', '' ), 
(199,N'TRES PROVINCIAS SEGUROS DE PERSONAS S.A.', '' ), 
(200,N'TRIUNFO COOPERATIVA DE SEGUROS LIMITADA', '' ), 
(201,N'TUTELAR SEGUROS SOCIEDAD ANONIMA', '' ), 
(202,N'VIRGINIA SURETY COMPAÑIA DE SEGUROS S.A.', '' ), 
(203,N'WARRANTY INSURANCE COMPANY ARGENTINA DE SEGUROS SOCIEDAD ANÓNIMA', '' ), 
(204,N'XL INSURANCE ARGENTINA S.A. COMPAÑÍA DE SEGUROS', '' ), 
(205,N'ZURICH ARGENTINA COMPAÑIA DE SEGUROS DE RETIRO SOCIEDAD ANÓNIMA', '' ), 
(206,N'ZURICH ARGENTINA COMPAÑIA DE SEGUROS SOCIEDAD ANONIMA', '' ), 
(207,N'ZURICH ARGENTINA REASEGUROS S.A.', '' ), 
(208,N'ZURICH ASEGURADORA ARGENTINA S.A.', '' ), 
(209,N'ZURICH COMPAÑIA DE REASEGUROS ARGENTINA S.A.', '' ), 
(210,N'ZURICH INTERNATIONAL LIFE LIMITED SUCURSAL ARGENTINA', '' ), 
(211,N'ZURICH SANTANDER SEGUROS ARGENTINA S.A.', '' )

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
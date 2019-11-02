SET IDENTITY_INSERT [dbo].[ClaimDocuments] ON
GO
--DEFINO LA TABLA DESTINO (TARGET)
MERGE INTO [dbo].[ClaimDocuments] AS Target

USING (VALUES
--¨CARGO DATOS PARA MERGE (ORIGEN)
	(1,N'Reconsideracion', N'<html>
<head>
	<style>
		.container{
			width: 1120;
			padding: 60px;
			font-size: 18px;
			font-family: sans-serif;
		}
		
		.caja{
			width: 546px;
			margin-left: 276px;
			padding: 17px;
			text-align: left;
			border: 1px solid #454666;
		}
		
		.barraSup{
			background-color: #37474f;
			height: 35px;
			width: 1225px;
			font-family: sans-serif;
			color: white;
			font-size: 31px;
			padding-left: 10px;
		}
	</style>
</head>
<body>
	<div class="barraSup">
		SOLUTIO
	</div>
	<div class="container">
	  <p>
		[thirdCompany]
	  </p>
	  <p>
		S__________/__________D
	  </p>
	  <p class="caja">
		Fecha de siniestro: [sinisterDate]
		<br>
		El dominio de tu asegurado es: [thirdVehicleDomain]
		<br>
		El número de siniestro en tu compañía es: [sinisterNumber]
	  </p>
	  <br>
	  <br>
	  <p>
		Hemos recibido como oferta por el siniestro de referencia la suma de: [montoOfrecimiento]
	  </p>
	  <p>
		Consideramos insuficiente la oferta realizada por vuestra aseguradora y solicitamos se reconsidere el monto ofrecido
		y proceda a abonar la suma de: [montoReclamado].
	  </p>
	  <p>
		A este monto se le suma lo que determinen los estudios médicos correspondiente a las lesiones (si hay lesiones).
	  </p>
	  <p>
		Esperamos que juntos podamos trabajar para darle una respuesta eficiente y satisfactoria a los damnificados.
	  </p>
	</div>
</body>
</html>'),
	(2,N'Reclamo', N'<html>
<head>
	<style>
		.container{
			width: 1120;
			padding: 60px;
			font-size: 16px;
			font-family: sans-serif;
		}
		
		.caja{
			width: 546px;
			margin-left: 276px;
			padding: 17px;
			text-align: left;
			border: 1px solid #454666;
		}
		
		.barraSup{
			background-color: #37474f;
			height: 35px;
			width: 1225px;
			font-family: sans-serif;
			color: white;
			font-size: 31px;
			padding-left: 10px;
		}
	</style>
</head>
<body>
	<div class="barraSup">
		SOLUTIO
	</div>
	<div class="container">
	  <p>
		[thirdCompany]
	  </p>
	  <p>
		S__________/__________D
	  </p>
	  <p class="caja">
		Fecha de siniestro: [sinisterDate]
		<br>
		El dominio de tu asegurado es: [thirdVehicleDomain]
		<br>
		El número de siniestro en tu compañía es: [sinisterNumber]
	  </p>
	  <br>
	  <br>
	  <p>
		Felicitaciones, recibiste un reclamo de SOLUTIO! Esperamos que juntos podamos trabajar para darle una respuesta eficiente y satisfactoria a quienes te detallamos a continuación:
	  </p>
	  <p>
		<b>DAMNIFICADOS:</b><br><br>
		[nombrePersona1] - [dniPersona1] - [enCaracterDePersona1] <br>
		[nombrePersona2] - [dniPersona2] - [enCaracterDePersona2] <br>
		[nombrePersona3] - [dniPersona3] - [enCaracterDePersona3] <br>
	  </p>
	  <p>
		<b>BIEN DAÑADO:</b><br><br>
		[dominioVehiculo1] - [tipoDeVehiculo1] <br>
		[dominioVehiculo2] - [tipoDeVehiculo2] <br>
		[dominioVehiculo3] - [tipoDeVehiculo3] <br>
	  </p>
	  <p>
		<b>¿CUÁL ES LA JUSTA INDEMNIZACION?</b><br><br>
		DAÑO EMERGENTE: [montoAReclamar] <br>
		LESIONES: (si hay lesiones) Monto justo que determinen los estudios médicos realizados <br>
		Todo lo arriba mencionado con más los intereses legales desde la fecha de producción del siniestro hasta su real y efectivo pago, gastos y honorarios profesionales seria lo justo.
	  </p>
	  <p>
		<b>¿QUE HECHOS FUNDAMENTAN ESTO?</b><br><br>
		Direccion del siniestro: [DireccionDelSiniestro]	 <br>		
		Entre Calles: [entreCalles] <br>
		Localidad: [Localidad] 		 <br>	
		Provincia: [Provincia]		 <br>	
		País: [País]  <br>
		Fecha de siniestro: [FechaDeSiniestro]		 <br>	
		Hora: [HoraSiniestro] <br>
		Relato del hecho Relato del hecho: [relato] <br>
	  </p>
	</div>
</body>
</html>')
) AS Source([DocumentsId],[title], [HtmlTemplate]) 
ON Target.[Id] = [DocumentsId] 

WHEN MATCHED THEN
	UPDATE SET [HtmlTemplate] = Source.[HtmlTemplate],
	[title] = Source.[title]

WHEN NOT MATCHED BY TARGET THEN
	INSERT ([Id],
			[HtmlTemplate],
			[title])
	VALUES (Source.[DocumentsId],
			Source.[HtmlTemplate],
			Source.[title])

WHEN NOT MATCHED BY Source THEN
    DELETE;
GO

SET IDENTITY_INSERT [ClaimDocuments] OFF
GO
﻿SET IDENTITY_INSERT [dbo].[ClaimDocuments] ON
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
			font-size: 16px;
			font-family: sans-serif;
		}
		
		.caja{
			width: 546px;
			margin-left: 276px;
			padding: 17px;
			text-align: left;
			border: 1px solid #ddd;
			background-color: #f2f2f2;
		}
		
		.barraSup{
			background-color: #37474f;
			height: 35px;
			width: 1225px;
			font-family: sans-serif;
			color: white;
			font-size: 21px;
			padding-left: 10px;
			padding-top: 9px;
		}
		
		 ul {
			list-style: none;
			padding:0;
			margin:0;
		}

		li { 
			padding-left: 1em; 
			text-indent: -.7em;
		}

		li:before {
			content: "• ";
			color: #4CAF50;
		}
	</style>
</head>
<body>
	<div class="barraSup">
		SOLUTIO - Reconsideración
	</div>
	<div class="container">
	  <p>
		Reclamo nro: [claimId]
	  </p>
	  <p>
		Compañía de seguro: [thirdCompany]
	  </p>
	  </p>
	  <p>
		S__________/__________D
	  </p>
	 <ul class="caja">
			<li>Fecha de siniestro: [sinisterDate]</li>
			<li>El dominio de tu asegurado es: [thirdVehicleDomain]</li>
			<li>El número de siniestro en tu compañía es: [sinisterNumber]</li>
	</ul>
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
			border: 1px solid #ddd;
			background-color: #f2f2f2;
		}
		
		.barraSup{
			background-color: #37474f;
			height: 35px;
			width: 1225px;
			font-family: sans-serif;
			color: white;
			font-size: 21px;
			padding-left: 10px;
			padding-top: 9px;
		}
		#tabla td, #reclamos th {
		  border: 1px solid #ddd;
		  //padding: 8px;
		}
		
		table {
		  width: 100%;
		}

		th {
		 //height: 50px;
		   background-color: #4CAF50;
			color: white;
		}
		
		th, td {
		 // padding: 15px;
		  text-align: left;
		}
		
		tr:nth-child(even) {background-color: #f2f2f2;}
		
		 ul {
			list-style: none;
			padding:0;
			margin:0;
		}

		li { 
			padding-left: 1em; 
			text-indent: -.7em;
		}

		li:before {
			content: "• ";
			color: #4CAF50;
		}
	</style>
</head>
<body>
	<div class="barraSup">
		SOLUTIO - Reclamo
	</div>
	<div class="container">
	<p>
		Reclamo nro: [claimId]
	  </p>
	  <p>
		Compañía de seguro: [thirdCompany]
	  </p>
	  <p>
		S__________/__________D
	  </p>
		<ul class="caja">
			<li>Fecha de siniestro: [sinisterDate]</li>
			<li>El dominio de tu asegurado es: [thirdVehicleDomain]</li>
			<li>El número de siniestro en tu compañía es: [sinisterNumber]</li>
		</ul>
	  <p>
		Felicitaciones, recibiste un reclamo de SOLUTIO! Esperamos que juntos podamos trabajar para darle una respuesta eficiente y satisfactoria a quienes te detallamos a continuación:
	  </p>
	  <p>
		<b>DAMNIFICADOS:</b><br>
		<table class="tabla">
		<tr>
		  <th>Nombre</th>
		  <th>Dni</th>
		  <th>En carácter de</th>
		</tr>
		<tr>
		  <td>[nombrePersona1]</td>
		  <td>[dniPersona1]</td>
		  <td>[enCaracterDePersona1]</td>
		</tr>
		<tr>
		  <td>[nombrePersona2]</td>
		  <td>[dniPersona2]</td>
		  <td>[enCaracterDePersona2]</td>
		</tr>
		<tr>
		  <td>[nombrePersona3]</td>
		  <td>[dniPersona3]</td>
		  <td>[enCaracterDePersona3]</td>
		</tr>
	  </table>
	  </p>
	  <p>
		<b>BIEN DAÑADO:</b><br>
		<table class="tabla">
		<tr>
		  <th>Dominio</th>
		  <th>Tipo de vehiculo</th>
		</tr>
		<tr>
		  <td>[dominioVehiculo1]</td>
		  <td>[tipoDeVehiculo1]</td>
		</tr>
		<tr>
		  <td>[dominioVehiculo2]</td>
		  <td>[tipoDeVehiculo2]</td>
		</tr>
		<tr>
		  <td>[dominioVehiculo3]</td>
		  <td>[tipoDeVehiculo3]</td>
		</tr>
	  </table>
	  </p>
	  <p>
		<b>¿CUÁL ES LA JUSTA INDEMNIZACION?</b><br>
		<ul>
			<li>Daño emergente: [montoAReclamar]]</li>
			<li>Lesiones: (si hay lesiones) Monto justo que determinen los estudios médicos realizados 
		Todo lo arriba mencionado con más los intereses legales desde la fecha de producción del siniestro hasta su real y efectivo pago, gastos y honorarios profesionales seria lo justo.</li>
		</ul>
	  </p>
	  <p>
		<b>¿QUE HECHOS FUNDAMENTAN ESTO?</b><br>
		<ul>
			<li>Direccion del siniestro: [DireccionDelSiniestro]</li>
			<li>Entre Calles: [entreCalles]</li>
			<li>Localidad: [Localidad]</li>
			<li>Provincia: [Provincia]</li>
			<li>País: [País]</li>
			<li>Fecha de siniestro: [FechaDeSiniestro]</li>
			<li>Hora: [HoraSiniestro]</li>
			<li>Relato del hecho Relato del hecho: [relato]</li>
		</ul>
	  </p>
	</div>
</body>
</html>'),
	(3,N'Caratula', N'<html>
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
			font-size: 21px;
			padding-left: 10px;
			padding-top: 9px;
		}
		
		#reclamos td, #reclamos th {
		  border: 1px solid #ddd;
		  padding: 8px;
		}
		
		table {
		  width: 100%;
		}

		th {
		  height: 50px;
		   background-color: #4CAF50;
			color: white;
		}
		
		th, td {
		  padding: 15px;
		  text-align: left;
		}
		
		tr:nth-child(even) {background-color: #f2f2f2;}
	</style>
</head>
<body>
	<div class="barraSup">
		SOLUTIO - Listado de reclamos
	</div>
	<div class="container">
	  <table id="reclamos">
		<tr>
		  <th>Id</th>
		  <th>Titular</th>
		  <th>Fecha</th>
		  <th>Hora</th>
		  <th>Estado</th>
		</tr>
		<tr>
		  <td>Id</td>
		  <td>Titular</td>
		  <td>Fecha</td>
		  <td>Hora</td>
		  <td>Estado</td>
		</tr>
		<tr>
		  <td>Id</td>
		  <td>Titular</td>
		  <td>Fecha</td>
		  <td>Hora</td>
		  <td>Estado</td>
		</tr>
	  </table>
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
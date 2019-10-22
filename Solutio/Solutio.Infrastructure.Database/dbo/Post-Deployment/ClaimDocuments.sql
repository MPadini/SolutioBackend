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
			    width: 747px;
			height: 855px;
			padding: 60px;
			font-size: 16px;
		}
		
		.caja{
			width: 400px;
			margin-left: 130px;
			padding: 17px;
			text-align: left;
			border: 1px solid black;
		}
	</style>
</head>
<body>
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
	  <br>
	  <p>
		Consideramos insuficiente la oferta realizada por vuestra aseguradora y solicitamos se reconsidere el monto ofrecido
		y
		proceda a abonar la suma de: [montoReclamado].
	  </p>
	  <p>
		A este monto se le suma lo que determinen los estudios médicos correspondiente a las lesiones (si hay lesiones).
	  </p>
	  <br>
	  <p>
		Esperamos que juntos podamos trabajar para darle una respuesta eficiente y satisfactoria a los damnificados.
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
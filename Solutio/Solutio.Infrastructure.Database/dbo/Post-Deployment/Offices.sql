SET IDENTITY_INSERT [dbo].[Offices] ON
GO

MERGE INTO [dbo].[Offices] AS Target

USING (VALUES
	(1, N'Oficina demo 1'),
	(2, N'Oficina demo 2')
) AS Source([OfficesId], [Name]) 
ON Target.[Id] = [OfficesId] 

WHEN MATCHED THEN
	UPDATE SET [Name] = Source.[Name]

WHEN NOT MATCHED BY TARGET THEN
	INSERT ([Id],
			[Name])
	VALUES (Source.[OfficesId],
			Source.[Name])
 
WHEN NOT MATCHED BY Source THEN
    DELETE;
GO

SET IDENTITY_INSERT [Offices] OFF
GO
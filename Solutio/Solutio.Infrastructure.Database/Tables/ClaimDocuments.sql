CREATE TABLE [dbo].[ClaimDocuments]
(
	[Id] bigint NOT NULL IDENTITY,    
	[Title] nvarchar(100) NULL,
    [HtmlTemplate] nvarchar(max) NOT NULL,
	[Created] DATETIME NOT null DEFAULT GETDATE(),
	[Modified] DATETIME,
	[Deleted] DATETIME
)

CREATE TABLE [dbo].[InsuranceCompanies]
(
	[Id] BIGINT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] VARCHAR(200) NULL, 
    [Created] DATETIME NOT NULL DEFAULT GetDate(), 
    [Modified] DATETIME NULL, 
    [Deleted] DATETIME NULL, 
    [Adress] NVARCHAR(MAX) NULL
)

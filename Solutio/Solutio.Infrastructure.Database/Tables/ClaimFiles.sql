CREATE TABLE [dbo].[ClaimFiles]
(
	[Id] BIGINT NOT NULL PRIMARY KEY IDENTITY, 
    [ClaimId] BIGINT NOT NULL, 
    [Base64] TEXT NOT NULL, 
	[FileName] VARCHAR(500) NOT NULL, 
    [FileExtension] VARCHAR(500) NOT NULL, 
	[FileTypeId] BIGINT NOT NULL DEFAULT 1, 
    [Created] DATETIME NULL DEFAULT Getdate(), 
    [Modified] DATETIME NULL, 
    [Deleted] DATETIME NULL,
    [Printed] BIT NOT NULL DEFAULT 0, 
    CONSTRAINT [FK_ClaimFiles_Claim_ClaimId] FOREIGN KEY ([ClaimId]) REFERENCES [Claims] ([Id])
)

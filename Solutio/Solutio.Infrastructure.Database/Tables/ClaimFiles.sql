CREATE TABLE [dbo].[ClaimFiles]
(
	[Id] BIGINT NOT NULL PRIMARY KEY IDENTITY, 
    [ClaimId] BIGINT NOT NULL, 
    [Base64] TEXT NOT NULL, 
	[FileName] VARCHAR(50) NOT NULL, 
    [FileExtension] VARCHAR(50) NOT NULL, 
	[FileTypeId] BIGINT NOT NULL DEFAULT 1, 
    [Created] DATETIME NULL DEFAULT Getdate(), 
    [Modified] DATETIME NULL, 
    [Deleted] DATETIME NULL,
    CONSTRAINT [FK_ClaimFiles_Claim_ClaimId] FOREIGN KEY ([ClaimId]) REFERENCES [Claims] ([Id])
)

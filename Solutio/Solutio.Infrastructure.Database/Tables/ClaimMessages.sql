CREATE TABLE [dbo].[ClaimMessages]
(
	[Id] INT NOT NULL IDENTITY, 
    [ClaimId] BIGINT NOT NULL, 
    [UserId] int NOT NULL, 
    [Message] VARCHAR(MAX) NOT NULL, 
    [Viewed] BIT NOT NULL, 
    [Created] DATETIME2 NOT NULL DEFAULT GetDate(), 
    [Modified] DATETIME2 NULL, 
    [Deleted] DATETIME2 NULL,
    CONSTRAINT [PK_ClaimMessages] PRIMARY KEY ([Id]),
	CONSTRAINT [FK_ClaimMessages_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE,
	CONSTRAINT [FK_ClaimMessages_Claims] FOREIGN KEY ([ClaimId]) REFERENCES [Claims] ([Id]) ON DELETE CASCADE
)

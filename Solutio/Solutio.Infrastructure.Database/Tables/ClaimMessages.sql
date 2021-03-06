﻿CREATE TABLE [dbo].[ClaimMessages]
(
	[Id] BIGINT NOT NULL IDENTITY, 
    [ClaimId] BIGINT NOT NULL, 
    [UserName] VARCHAR(100) NOT NULL, 
    [Message] VARCHAR(MAX) NOT NULL, 
    [Viewed] BIT NOT NULL, 
    [Created] DATETIME2 NOT NULL DEFAULT GetDate(), 
    [Modified] DATETIME2 NULL, 
    [Deleted] DATETIME2 NULL,
    CONSTRAINT [PK_ClaimMessages] PRIMARY KEY ([Id]),
	CONSTRAINT [FK_ClaimMessages_Claims] FOREIGN KEY ([ClaimId]) REFERENCES [Claims] ([Id]) ON DELETE CASCADE
)

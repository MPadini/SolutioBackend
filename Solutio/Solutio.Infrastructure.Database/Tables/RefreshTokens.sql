CREATE TABLE [dbo].[RefreshTokens]
(
	[Id] BIGINT NOT NULL PRIMARY KEY IDENTITY, 
	UserName varchar(200) not null,
	RefreshToken varchar(2000) not null,
    [Created] DATETIME NOT NULL DEFAULT GetDate(), 
    [Modified] DATETIME NULL, 
    [Deleted] DATETIME NULL
)

CREATE TABLE [dbo].[ClaimOfferStates]
(
	[Id] bigint NOT NULL IDENTITY,
    [Description] nvarchar(max) NOT NULL,
	[Created] DATETIME NOT null DEFAULT GETDATE(),
	[Modified] DATETIME,
	[Deleted] DATETIME
)

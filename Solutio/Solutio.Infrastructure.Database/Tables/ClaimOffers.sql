CREATE TABLE [dbo].[ClaimOffers]
(
	[Id] BIGINT NOT NULL PRIMARY KEY IDENTITY,
	[ClaimId] BIGINT NOT NULL, 
	ClaimOfferStateId bigint,
	OfferedAmount decimal(18,2),
	PayInstructions varchar(max),
	WayToPay int,
	AgreementFileId bigint,
	SignedAgreementFileId bigint,
	[Created] datetime NOT NULL DEFAULT GetDate(),
    [Modified] datetime NULL,
    [Deleted] datetime NULL,
)

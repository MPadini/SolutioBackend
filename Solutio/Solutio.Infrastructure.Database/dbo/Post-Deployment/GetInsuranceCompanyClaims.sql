CREATE PROCEDURE [dbo].[GetInsuranceCompanyClaims]
AS
BEGIN

	SELECT Id, Description
	INTO #tempStates
	FROM [dbo].[ClaimStates] where id in (21, 22, 23, 32, 33, 41, 43, 44)  

	DECLARE @Columns as VARCHAR(MAX)
	SELECT @Columns =
	COALESCE(@Columns + ', ','') + QUOTENAME('S'+CONVERT(VARCHAR,Id))
	FROM(SELECT * FROM #tempStates) AS B
	ORDER BY B.Id

	DECLARE @Columns2 as VARCHAR(MAX)
	SELECT @Columns2 =
	COALESCE(@Columns2 + ', ','') + 'ISNULL(' + QUOTENAME('S'+CONVERT(VARCHAR,Id)) + ', 0) ' + QUOTENAME('S'+CONVERT(VARCHAR,Id))
	FROM(SELECT * FROM #tempStates) AS B
	ORDER BY B.Id


	--THIS IS ONLY FOR WRITTING CLASS PURPOSES...
	--DECLARE @ClassProps as VARCHAR(MAX)
	--SELECT @ClassProps =
	--COALESCE(@ClassProps + ' ','') +  'public string ' + 'S'+CONVERT(VARCHAR,Id) + ' { get; set; } '
	--FROM(SELECT * FROM #tempStates) AS B
	--ORDER BY B.Id

	--SELECT @ClassProps
	--THIS IS ONLY FOR WRITTING CLASS PURPOSES...


	SELECT IC.Id, IC.Name, 'S'+CONVERT(VARCHAR,B.stateId) as stateId, count(B.ClaimId) as Cantidad
	INTO #tempTable
	FROM [dbo].[InsuranceCompanies] IC
	LEFT JOIN
	(SELECT V.InsuranceCompanyId, C.Id AS ClaimId, C.Deleted AS ClaimDeleted, CS.Id as stateId FROM 
	[dbo].[Vehicles] V 
	INNER JOIN [dbo].[ClaimThirdInsuredVehicles] IV ON IV.VehicleId = V.Id
	INNER JOIN [dbo].[Claims] C ON C.Id = IV.ClaimId
	INNER JOIN [dbo].[ClaimStates] CS ON CS.Id = C.StateId and CS.id in (21, 22, 23, 32, 33, 41, 43, 44)
	) AS B ON B.InsuranceCompanyId = IC.Id 
	WHERE B.ClaimDeleted is null 
	GROUP BY IC.Id, IC.Name, 'S'+CONVERT(VARCHAR,B.stateId)

	--SELECT * FROM #tempTable
	--SELECT @Columns
	--SELECT @Columns2
	--drop table #tempTable

	DECLARE @SQL as VARCHAR(MAX)
	SET @SQL = 'SELECT Id, Name as CompanyName,' + @Columns2 + ', (select SUM(Cantidad) from #tempTable B where B.Id = TablaPivot.Id) as TOTAL
	FROM #tempTable
	PIVOT(Sum(Cantidad) for
	stateId in (' + @Columns + '))
	As TablaPivot
	WHERE (select SUM(Cantidad) from #tempTable B where B.Id = TablaPivot.Id) > 0'


	EXEC(@SQL)

	DROP TABLE #tempTable
	DROP TABLE #tempStates

END
--RETURN 0
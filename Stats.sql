USE TuroPhoto1

SELECT 
	COUNT(*),
	MIN(Created) AS 'MinCreated',
	MAX(Created) AS 'MaxCreated'
FROM LibraryCatalogs


SELECT 
	ai.DirectoryPath, 
	COUNT(*) AS 'Count'
FROM 
	LibraryCatalogDirectories d JOIN 
	LibraryCatalogs ai ON d.LibraryCatalogId = ai.Id
GROUP BY
	ai.DirectoryPath

SELECT 
	ai.DirectoryPath, 
	MIN(p.DateTimeFromMetaData) AS 'MinDateTimeFromMetaData',
	MAX(p.DateTimeFromMetaData) AS 'MaxDateTimeFromMetaData',
	COUNT(*) AS 'Count'
FROM 
	Photos p JOIN 
	LibraryCatalogs ai ON p.LibraryCatalogId = ai.Id
GROUP BY
	ai.DirectoryPath
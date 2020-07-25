USE TuroPhoto1

SELECT 
	COUNT(*),
	MIN(Created) AS 'MinCreated',
	MAX(Created) AS 'MaxCreated'
FROM LibraryCatalog


SELECT 
	ai.DirectoryPath, 
	COUNT(*) AS 'Count'
FROM 
	LibraryCatalogDirectory d JOIN 
	LibraryCatalog ai ON d.LibraryCatalogId = ai.Id
GROUP BY
	ai.DirectoryPath

SELECT 
	ai.DirectoryPath, 
	MIN(p.DateTimeFromMetaData) AS 'MinDateTimeFromMetaData',
	MAX(p.DateTimeFromMetaData) AS 'MaxDateTimeFromMetaData',
	COUNT(*) AS 'Count'
FROM 
	Photo p JOIN 
	LibraryCatalog ai ON p.LibraryCatalogId = ai.Id
GROUP BY
	ai.DirectoryPath
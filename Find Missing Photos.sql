
SELECT * FROM LibraryCatalog


SELECT [FileName] AS 'Missing Photo' FROM Photo p1 WHERE p1.LibraryCatalogId = 3 EXCEPT
SELECT [FileName] FROM Photo p1 WHERE p1.LibraryCatalogId = 4

SELECT [FileName]  AS 'Missing Photo', DateTimeFromMetaData AS 'Missing DateTimeFromMetaData' FROM Photo p1 WHERE p1.LibraryCatalogId = 3 EXCEPT
SELECT [FileName], DateTimeFromMetaData FROM Photo p1 WHERE p1.LibraryCatalogId = 4


SELECT 
	c.DirectoryPath, 
	d.Path,
	p.[FileName] AS 'Missing Photo'
FROM 
	Photo p JOIN 
	LibraryCatalog c ON p.LibraryCatalogId = c.Id JOIN
	LibraryCatalogDirectory d ON p.LibraryCatalogDirectoryId = d.Id
WHERE
	p.LibraryCatalogId = 3 AND
	p.[FileName] IN (
		SELECT [FileName] FROM Photo p1 WHERE p1.LibraryCatalogId = 3 EXCEPT
		SELECT [FileName] FROM Photo p1 WHERE p1.LibraryCatalogId = 4)
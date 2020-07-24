USE AlbumIndex1

SELECT COUNT(*) FROM AlbumIndex
SELECT 
	ai.DirectoryPath, COUNT(*) 
FROM 
	Directories d JOIN 
	AlbumIndex ai ON d.AlbumIndexId = ai.Id
GROUP BY
	ai.DirectoryPath

SELECT 
	ai.DirectoryPath, 
	MIN(p.DateTimeFromMetaData) AS 'MinDateTimeFromMetaData',
	MAX(p.DateTimeFromMetaData) AS 'MaxDateTimeFromMetaData',
	COUNT(*) AS 'Count'
FROM 
	Photos p JOIN 
	AlbumIndex ai ON p.AlbumIndexId = ai.Id
GROUP BY
	ai.DirectoryPath
BEGIN TRAN

INSERT INTO WineStyle ([Name]) VALUES ('Crveno vino punog tela' )
INSERT INTO WineStyle ([Name]) VALUES ('Crveno vino srednjeg tela')
INSERT INTO WineStyle ([Name]) VALUES ('Lagano crveno vino')
INSERT INTO WineStyle ([Name]) VALUES ('Roze')
INSERT INTO WineStyle ([Name]) VALUES ('Snažno belo vino' COLLATE Serbian_Latin_100_CI_AS)
INSERT INTO WineStyle ([Name]) VALUES ('Lagano belo vino')
INSERT INTO WineStyle ([Name]) VALUES ('Slatko belo vino')
SELECT * FROM WineStyle

INSERT INTO WineSort([Name]) VALUES ('Kaberne')
INSERT INTO WineSort([Name]) VALUES ('Vranac')
INSERT INTO WineSort([Name]) VALUES ('Kratošija' COLLATE Serbian_Latin_100_CI_AS)
INSERT INTO WineSort([Name]) VALUES ('Pino noar')
INSERT INTO WineSort([Name]) VALUES ('Sandjoveze' COLLATE Serbian_Latin_100_CI_AS)
INSERT INTO WineSort([Name]) VALUES ('Šardone' COLLATE Serbian_Latin_100_CI_AS)
INSERT INTO WineSort([Name]) VALUES ('Tamjanika')
SELECT * FROM WineSort

INSERT INTO Wine
(
	[Name]
	, Price
	, IDStyle
	, IDSort
) 
SELECT 'Vino1', 1500, wst.ID, ws.ID
FROM WineStyle wst
	JOIN WineSort ws ON ws.[Name] = 'Kaberne'
WHERE wst.[Name] = 'Crveno vino punog tela'

INSERT INTO Wine
(
	[Name]
	, Price
	, IDStyle
	, IDSort
) 
SELECT 'Vino2', 1200, wst.ID, ws.ID
FROM WineStyle wst
	JOIN WineSort ws ON ws.[Name] = 'Vranac'
WHERE wst.[Name] = 'Crveno vino punog tela'

INSERT INTO Wine
(
	[Name]
	, Price
	, IDStyle
	, IDSort
) 
SELECT 'Vino3', 1350, wst.ID, ws.ID
FROM WineStyle wst
	JOIN WineSort ws ON ws.[Name] = 'Kratošija'
WHERE wst.[Name] = 'Crveno vino srednjeg tela'

INSERT INTO Wine
(
	[Name]
	, Price
	, IDStyle
	, IDSort
) 
SELECT 'Vino4', 1400, wst.ID, ws.ID
FROM WineStyle wst
	JOIN WineSort ws ON ws.[Name] = 'Pino noar'
WHERE wst.[Name] = 'Lagano crveno vino'

INSERT INTO Wine
(
	[Name]
	, Price
	, IDStyle
	, IDSort
) 
SELECT 'Vino5', 1300, wst.ID, ws.ID
FROM WineStyle wst
	JOIN WineSort ws ON ws.[Name] = 'Sandjoveze'
WHERE wst.[Name] = 'Roze'

INSERT INTO Wine
(
	[Name]
	, Price
	, IDStyle
	, IDSort
) 
SELECT 'Vino6', 1600, wst.ID, ws.ID
FROM WineStyle wst
	JOIN WineSort ws ON ws.[Name] = 'Šardone'
WHERE wst.[Name] = 'Snažno belo vino'

INSERT INTO Wine
(
	[Name]
	, Price
	, IDStyle
	, IDSort
) 
SELECT 'Vino7', 1500, wst.ID, ws.ID
FROM WineStyle wst
	JOIN WineSort ws ON ws.[Name] = 'Šardone'
WHERE wst.[Name] = 'Lagano belo vino'
SELECT * FROM Wine

INSERT INTO Wine
(
	[Name]
	, Price
	, IDStyle
	, IDSort
) 
SELECT 'Vino8', 1500, wst.ID, ws.ID
FROM WineStyle wst
	JOIN WineSort ws ON ws.[Name] = 'Tamjanika'
WHERE wst.[Name] = 'Lagano belo vino'
SELECT * FROM Wine

INSERT INTO Wine
(
	[Name]
	, Price
	, IDStyle
	, IDSort
) 
SELECT 'Vino9', 1500, wst.ID, ws.ID
FROM WineStyle wst
	JOIN WineSort ws ON ws.[Name] = 'Tamjanika'
WHERE wst.[Name] = 'Slatko belo vino'
SELECT * FROM Wine

--ROLLBACK TRAN
COMMIT TRAN


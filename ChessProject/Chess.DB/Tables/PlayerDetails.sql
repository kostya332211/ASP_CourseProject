﻿CREATE TABLE [dbo].[PlayerDetails]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [BulletRating] INT NOT NULL DEFAULT 1700, 
    [BlitzRating] INT NOT NULL DEFAULT 1700, 
    [RapidRating] INT NOT NULL DEFAULT 1700, 
    [OnlyKnightsRating] INT NOT NULL DEFAULT 1700, 
    [OnlyPawnsRating] INT NOT NULL DEFAULT 1700, 
    [OnlyQueensRating] INT NOT NULL DEFAULT 1700
)

CREATE TABLE [dbo].[GameHistory]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
	[GameTypeId] INT NOT NULL,
	[WhitePlayerId] UNIQUEIDENTIFIER NOT NULL,
	[BlackPlayerId] UNIQUEIDENTIFIER NOT NULL,
	[WinnerId] UNIQUEIDENTIFIER,
	[EndGameTypeId] INT NOT NULL,
	[GameDate] DATETIME NOT NULL,
    CONSTRAINT [FK_GameHistory_Player1] FOREIGN KEY ([WhitePlayerId]) REFERENCES [Player]([Id]), 
    CONSTRAINT [FK_GameHistory_Player2] FOREIGN KEY ([BlackPlayerId]) REFERENCES [Player]([Id]), 
    CONSTRAINT [FK_GameHistory_Winner] FOREIGN KEY ([WinnerId]) REFERENCES [Player]([Id]), 
    CONSTRAINT [FK_GameHistory_EndGameType] FOREIGN KEY ([EndGameTypeId]) REFERENCES [EndGameType]([Id]), 
    CONSTRAINT [FK_GameHistory_GameType] FOREIGN KEY ([GameTypeId]) REFERENCES [GameType]([Id])
)

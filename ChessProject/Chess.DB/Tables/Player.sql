CREATE TABLE [dbo].[Player]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [FirstName] NVARCHAR(128) NOT NULL, 
    [LastName] NVARCHAR(128) NOT NULL,
    [Nickname] NVARCHAR(128) NOT NULL,
    [Email] NVARCHAR(128) NOT NULL,
    [PasswordHash] NVARCHAR(512) NOT NULL,
    [PasswordSalt] NVARCHAR(64) NOT NULL,
    [PlayerDetailsId] UNIQUEIDENTIFIER,
    [RoleId] INT NOT NULL,
    [StatusId] INT NOT NULL, 
    CONSTRAINT [FK_Player_PlayerDetails] FOREIGN KEY ([PlayerDetailsId]) REFERENCES [PlayerDetails]([Id]), 
    CONSTRAINT [FK_Player_Status] FOREIGN KEY ([StatusId]) REFERENCES [Status]([Id]), 
    CONSTRAINT [FK_Player_Role] FOREIGN KEY ([RoleID]) REFERENCES [Role]([Id])
)

/*
Скрипт развертывания для ChessPlatform

Этот код был создан программным средством.
Изменения, внесенные в этот файл, могут привести к неверному выполнению кода и будут потеряны
в случае его повторного формирования.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "ChessPlatform"
:setvar DefaultFilePrefix "ChessPlatform"
:setvar DefaultDataPath "C:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\"
:setvar DefaultLogPath "C:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\"

GO
:on error exit
GO
/*
Проверьте режим SQLCMD и отключите выполнение скрипта, если режим SQLCMD не поддерживается.
Чтобы повторно включить скрипт после включения режима SQLCMD выполните следующую инструкцию:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'Для успешного выполнения этого скрипта должен быть включен режим SQLCMD.';
        SET NOEXEC ON;
    END


GO
USE [master];


GO

IF (DB_ID(N'$(DatabaseName)') IS NOT NULL) 
BEGIN
    ALTER DATABASE [$(DatabaseName)]
    SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE [$(DatabaseName)];
END

GO
PRINT N'Выполняется создание $(DatabaseName)...'
GO
CREATE DATABASE [$(DatabaseName)]
    ON 
    PRIMARY(NAME = [$(DatabaseName)], FILENAME = N'$(DefaultDataPath)$(DefaultFilePrefix)_Primary.mdf')
    LOG ON (NAME = [$(DatabaseName)_log], FILENAME = N'$(DefaultLogPath)$(DefaultFilePrefix)_Primary.ldf') COLLATE SQL_Latin1_General_CP1_CI_AS
GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_CLOSE OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
USE [$(DatabaseName)];


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ANSI_NULLS ON,
                ANSI_PADDING ON,
                ANSI_WARNINGS ON,
                ARITHABORT ON,
                CONCAT_NULL_YIELDS_NULL ON,
                NUMERIC_ROUNDABORT OFF,
                QUOTED_IDENTIFIER ON,
                ANSI_NULL_DEFAULT ON,
                CURSOR_DEFAULT LOCAL,
                RECOVERY FULL,
                CURSOR_CLOSE_ON_COMMIT OFF,
                AUTO_CREATE_STATISTICS ON,
                AUTO_SHRINK OFF,
                AUTO_UPDATE_STATISTICS ON,
                RECURSIVE_TRIGGERS OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ALLOW_SNAPSHOT_ISOLATION OFF;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET READ_COMMITTED_SNAPSHOT OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_UPDATE_STATISTICS_ASYNC OFF,
                PAGE_VERIFY NONE,
                DATE_CORRELATION_OPTIMIZATION OFF,
                DISABLE_BROKER,
                PARAMETERIZATION SIMPLE,
                SUPPLEMENTAL_LOGGING OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET TRUSTWORTHY OFF,
        DB_CHAINING OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'Параметры базы данных изменить нельзя. Применить эти параметры может только пользователь SysAdmin.';
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET HONOR_BROKER_PRIORITY OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'Параметры базы данных изменить нельзя. Применить эти параметры может только пользователь SysAdmin.';
    END


GO
ALTER DATABASE [$(DatabaseName)]
    SET TARGET_RECOVERY_TIME = 0 SECONDS 
    WITH ROLLBACK IMMEDIATE;


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET FILESTREAM(NON_TRANSACTED_ACCESS = OFF),
                CONTAINMENT = NONE 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF fulltextserviceproperty(N'IsFulltextInstalled') = 1
    EXECUTE sp_fulltext_database 'enable';


GO
PRINT N'Выполняется создание [dbo].[EndGameType]...';


GO
CREATE TABLE [dbo].[EndGameType] (
    [Id]   INT           NOT NULL,
    [Type] NVARCHAR (32) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Выполняется создание [dbo].[GameHistory]...';


GO
CREATE TABLE [dbo].[GameHistory] (
    [GameId]        UNIQUEIDENTIFIER NOT NULL,
    [GameType]      INT              NOT NULL,
    [WhitePlayerId] UNIQUEIDENTIFIER NOT NULL,
    [BlackPlayerId] UNIQUEIDENTIFIER NOT NULL,
    [WinnerId]      UNIQUEIDENTIFIER NULL,
    [EndGameTypeId] INT              NOT NULL,
    PRIMARY KEY CLUSTERED ([GameId] ASC)
);


GO
PRINT N'Выполняется создание [dbo].[GameType]...';


GO
CREATE TABLE [dbo].[GameType] (
    [Id]   INT           NOT NULL,
    [Type] NVARCHAR (32) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Выполняется создание [dbo].[Player]...';


GO
CREATE TABLE [dbo].[Player] (
    [Id]              UNIQUEIDENTIFIER NOT NULL,
    [FirstName]       NVARCHAR (128)   NOT NULL,
    [LastName]        NVARCHAR (128)   NOT NULL,
    [Nickname]        NVARCHAR (128)   NOT NULL,
    [Email]           NVARCHAR (128)   NOT NULL,
    [PasswordHash]    NVARCHAR (512)   NOT NULL,
    [PasswordSalt]    NVARCHAR (64)    NOT NULL,
    [PlayerDetailsId] UNIQUEIDENTIFIER NULL,
    [RoleId]          INT              NOT NULL,
    [StatusId]        INT              NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Выполняется создание [dbo].[PlayerDetails]...';


GO
CREATE TABLE [dbo].[PlayerDetails] (
    [Id]                UNIQUEIDENTIFIER NOT NULL,
    [BulletRating]      INT              NOT NULL,
    [BlitzRating]       INT              NOT NULL,
    [RapidRating]       INT              NOT NULL,
    [OnlyKnightsRating] INT              NOT NULL,
    [OnlyPawnsRating]   INT              NOT NULL,
    [OnlyQueensRating]  INT              NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Выполняется создание [dbo].[Role]...';


GO
CREATE TABLE [dbo].[Role] (
    [Id]       INT           NOT NULL,
    [RoleName] NVARCHAR (32) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Выполняется создание [dbo].[Status]...';


GO
CREATE TABLE [dbo].[Status] (
    [Id]         INT           NOT NULL,
    [StatusName] NVARCHAR (32) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Выполняется создание ограничение без названия для [dbo].[PlayerDetails]...';


GO
ALTER TABLE [dbo].[PlayerDetails]
    ADD DEFAULT 1700 FOR [BulletRating];


GO
PRINT N'Выполняется создание ограничение без названия для [dbo].[PlayerDetails]...';


GO
ALTER TABLE [dbo].[PlayerDetails]
    ADD DEFAULT 1700 FOR [BlitzRating];


GO
PRINT N'Выполняется создание ограничение без названия для [dbo].[PlayerDetails]...';


GO
ALTER TABLE [dbo].[PlayerDetails]
    ADD DEFAULT 1700 FOR [RapidRating];


GO
PRINT N'Выполняется создание ограничение без названия для [dbo].[PlayerDetails]...';


GO
ALTER TABLE [dbo].[PlayerDetails]
    ADD DEFAULT 1700 FOR [OnlyKnightsRating];


GO
PRINT N'Выполняется создание ограничение без названия для [dbo].[PlayerDetails]...';


GO
ALTER TABLE [dbo].[PlayerDetails]
    ADD DEFAULT 1700 FOR [OnlyPawnsRating];


GO
PRINT N'Выполняется создание ограничение без названия для [dbo].[PlayerDetails]...';


GO
ALTER TABLE [dbo].[PlayerDetails]
    ADD DEFAULT 1700 FOR [OnlyQueensRating];


GO


INSERT INTO [dbo].[GameType] ([Id], [Type])
VALUES
(1, N'Blitz'),
(2, N'Bullet'),
(3, N'Rapid'),
(4, N'OnlyPawns'),
(5, N'OnlyKnights'),
(6, N'OnlyQueens')
INSERT INTO [dbo].[EndGameType] ([Id], [Type])
VALUES
(1, N'Checkmate'),
(2, N'Stalemate'),
(3, N'Leave Opponent')
INSERT INTO [dbo].[Role] ([Id], [RoleName])
VALUES
(1, N'User'),
(2, N'Admin')
INSERT INTO [dbo].[Status] ([Id], [StatusName])
VALUES
(1, N'Active'),
(2, N'Blocked'),
(3, N'BlockedChat')
INSERT INTO [dbo].[PlayerDetails] ([Id]) 
VALUES 
(N'18f63087-80fa-45ba-1623-1da170c900b0')
INSERT INTO [dbo].[Player] ([Id], [Email], [FirstName], [LastName], [Nickname], [PasswordHash], [PasswordSalt])
VALUES
('18f63087-80fa-45ba-1623-1da170c900b0', N'shiriton.kostya@gmail.com', N'Kostya', N'Shiriton', N'kosya40k', N'Ṍ⏏�揢뱣㆕ɛ륄ິ㘖ꃸ漝痖錵罸ꠉﲊ፪句扐凐粅甶枠㤖휞憈ㅠ뾞磩', N'LKMAUDKASB')

GO

GO
DECLARE @VarDecimalSupported AS BIT;

SELECT @VarDecimalSupported = 0;

IF ((ServerProperty(N'EngineEdition') = 3)
    AND (((@@microsoftversion / power(2, 24) = 9)
          AND (@@microsoftversion & 0xffff >= 3024))
         OR ((@@microsoftversion / power(2, 24) = 10)
             AND (@@microsoftversion & 0xffff >= 1600))))
    SELECT @VarDecimalSupported = 1;

IF (@VarDecimalSupported > 0)
    BEGIN
        EXECUTE sp_db_vardecimal_storage_format N'$(DatabaseName)', 'ON';
    END


GO
PRINT N'Обновление завершено.';


GO

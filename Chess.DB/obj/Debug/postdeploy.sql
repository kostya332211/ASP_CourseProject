

INSERT INTO [dbo].[GameType] ([Id], [Type])
VALUES
(1, N'Bullet'),
(2, N'Blitz	'),
(3, N'Rapid'),
(4, N'OnlyPawns'),
(5, N'OnlyKnights'),
(6, N'OnlyQueens')
INSERT INTO [dbo].[EndGameType] ([Id], [Type])
VALUES
(1, N'Checkmate'),
(2, N'Stalemate'),
(3, N'Leave Opponent'),
(4, N'Time is over'),
(5, N'Draw mutual consent')


INSERT INTO [dbo].[Role] ([Id], [RoleName])
VALUES
(1, N'User'),
(2, N'Admin')
INSERT INTO [dbo].[Status] ([Id], [StatusName])
VALUES
(1, N'Active'),
(2, N'Blocked'),
(3, N'BlockedChat')
INSERT INTO [dbo].[PlayerDetails] ([Id], [BlitzRating], [BulletRating], [OnlyKnightsRating], [OnlyPawnsRating], [OnlyQueensRating], [RapidRating])
VALUES
(N'18f63087-80fa-45ba-1623-000170c900b0', 1800, 1800, 1800, 1800, 1800, 1800)

INSERT INTO [dbo].[Player] ([Id], [Email], [FirstName], [LastName], [Nickname], [PasswordHash], [PasswordSalt], [PlayerDetailsId], [RoleId], [StatusId])
VALUES
('18f63087-80fa-45ba-1623-1da170c900b0', N'shiriton.kostya@gmail.com', N'Kostya', N'Shiriton', N'kosya40k', N'Ṍ⏏�揢뱣㆕ɛ륄ິ㘖ꃸ漝痖錵罸ꠉﲊ፪句扐凐粅甶枠㤖휞憈ㅠ뾞磩', N'LKMAUDKASB', N'18f63087-80fa-45ba-1623-000170c900b0', 2, 1)

GO

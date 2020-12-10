using System;

namespace Chess.Core.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IEndGameTypeRepository EndGameTypeRepository { get; }

        IGameHistoryRepository GameHistoryRepository { get; }

        IGameTypeRepository GameTypeRepository { get; }

        IPlayerDetailsRepository PlayerDetailsRepository { get; }

        IPlayerRepository PlayerRepository { get; }

        IRoleRepository RoleRepository { get; }

        IStatusRepository StatusRepository { get; }

        void Commit();
    }
}

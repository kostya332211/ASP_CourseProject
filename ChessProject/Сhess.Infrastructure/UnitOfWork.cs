using System;
using Chess.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Сhess.Infrastructure.Repositories;

namespace Сhess.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContextOptions<ChessContext> _options;
        private ChessContext _context;
        private IEndGameTypeRepository _endGameTypeRepository;
        private IGameHistoryRepository _gameHistoryRepository;
        private IGameTypeRepository _gameTypeRepository;
        private IPlayerDetailsRepository _playerDetailsRepository;
        private IPlayerRepository _playerRepository;
        private IRoleRepository _roleRepository;
        private IStatusRepository _statusRepository;

        private ChessContext Context => _context ??= new ChessContext(_options);

        public UnitOfWork(IOptions<UnitOfWorkOptions> optionsAccessor) : this(optionsAccessor.Value)
        {

        }

        public  UnitOfWork(UnitOfWorkOptions options)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ChessContext>();
            optionsBuilder.UseSqlServer(options.ConnectionString);
            _options = optionsBuilder.Options;
        }

        public IEndGameTypeRepository EndGameTypeRepository =>
            _endGameTypeRepository ??= new EndGameTypeRepository(Context);
        public IGameTypeRepository GameTypeRepository =>
            _gameTypeRepository ??= new GameTypeRepository(Context);
        public IGameHistoryRepository GameHistoryRepository =>
            _gameHistoryRepository ??= new GameHistoryRepository(Context);
        public IRoleRepository RoleRepository =>
            _roleRepository ??= new RoleRepository(Context);
        public IStatusRepository StatusRepository =>
            _statusRepository ??= new StatusRepository(Context);
        public IPlayerRepository PlayerRepository =>
            _playerRepository ??= new PlayerRepository(Context);
        public IPlayerDetailsRepository PlayerDetailsRepository =>
            _playerDetailsRepository ??= new PlayerDetailsRepository(Context);

        public void Commit()
        {
            if (_isDisposed)
            {
                throw new ObjectDisposedException("UnitOfWork");
            }

            Context.SaveChanges();
        }

        private bool _isDisposed;

        public void Dispose()
        {
            if (_context == null)
            {
                return;
            }

            if (!_isDisposed)
            {
                Context.Dispose();
            }

            _isDisposed = true;
        }
    }
}

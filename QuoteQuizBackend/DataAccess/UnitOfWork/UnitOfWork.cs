using QuoteQuizBackend.DataAccess.Repository;
using QuoteQuizBackend.Entities;

namespace QuoteQuizBackend.DataAccess.UnitOfWork
{
    public class UnitOfWork : IDisposable
    {
        private readonly QuoteQuizDbContext _context;
        private IGenericRepository<Quote> _quoteRepository;
        private IGenericRepository<Author> _authorRepository;
        private IGenericRepository<Player> _playerRepository;

        public UnitOfWork(QuoteQuizDbContext context)
        {
            _context = context;
        }

        public IGenericRepository<Quote> QuoteRepository
        {
            get
            {
                if (_quoteRepository is null)
                {
                    _quoteRepository = new GenericRepository<Quote>(_context);
                }
                return _quoteRepository;
            }
        }

        public IGenericRepository<Author> AuthorRepository
        {
            get
            {
                if (_authorRepository is null)
                {
                    _authorRepository = new GenericRepository<Author>(_context);
                }
                return _authorRepository;
            }
        }
        public IGenericRepository<Player> PlayerRepository
        {
            get
            {
                if (_playerRepository is null)
                {
                    _playerRepository = new GenericRepository<Player>(_context);
                }
                return _playerRepository;
            }
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        private bool _disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed && disposing)
            {
                _context.Dispose();
            }
            _disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

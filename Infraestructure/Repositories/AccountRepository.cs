using Domain.Entities;
using Infraestructure.Database.PostgreSQL;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories
{
    public class AccountRepository : BaseRepository<Account>
    {
        private readonly PostgreSQLDBContext _context;
        private readonly DbSet<Account> _dbSet;

        public AccountRepository(PostgreSQLDBContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<Account>();
        }
    }
}

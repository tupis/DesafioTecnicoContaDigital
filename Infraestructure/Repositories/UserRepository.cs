using Domain.Entities;
using Infraestructure.Database.PostgreSQL;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        private readonly PostgreSQLDBContext _context;
        private readonly DbSet<User> _dbSet;
        private readonly AccountRepository _accountRepository;

        public UserRepository(AccountRepository accountRepository,PostgreSQLDBContext context) : base(context)
        {
            _accountRepository = accountRepository;
            _context = context;
            _dbSet = _context.Set<User>();
        }

        public async Task<User?> FindById(Guid id) => await _dbSet.Include(x => x.Account).SingleOrDefaultAsync(x => x.Id == id);  
        
        public async Task<User?> FindByEmail(string email) => await _dbSet.Include(x => x.Account).SingleOrDefaultAsync(x => x.Email == email);
        
        public async Task<List<User>> GetAllUsers() => await _dbSet.Include(x => x.Account).ToListAsync();

        public async Task<User> CreateAsync(User user, Account account)
        {
            await _dbSet.AddAsync(user);
            await _context.SaveChangesAsync();
            await _accountRepository.CreateAsync(account);
            user.Account = account;
            return user;
        }
    }
}

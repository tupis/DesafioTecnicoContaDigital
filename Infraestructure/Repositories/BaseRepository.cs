using Domain.Entities;
using Infraestructure.Utils;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infraestructure.Repositories
{
    public class BaseRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public BaseRepository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity?> DeleteByIdAsync(Guid id)
        {
            var entity = await FindById(id);

            if (entity == null) return null;

            if (entity.DeletedAt != null) return null;

            entity.DeletedAt = DateTime.UtcNow;
            await UpdateAsync(entity);

            return entity;
        }

        public async Task<TEntity?> FindById(Guid id)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Id == id && x.DeletedAt == null);
        }

        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null)
        {

            Expression<Func<TEntity, bool>>? defaultFilter = (x) => x.DeletedAt == null;

            Expression<Func<TEntity, bool>>? combinedFilter = CombineFilters<TEntity>.Handle(defaultFilter, filter);

            return await _dbSet.Where(combinedFilter).ToListAsync();
        }

        public async Task<TEntity?> UpdateAsync(TEntity entityUpdated)
        {
            var entity = await FindById(entityUpdated.Id);

            if (entity == null)
                return null;

            _context.Entry(entity).CurrentValues.SetValues(entityUpdated);

            await _context.SaveChangesAsync();
            return entityUpdated;
        }
    }
}
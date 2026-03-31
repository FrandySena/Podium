using Microsoft.EntityFrameworkCore;
using Podium.Persistence.Migrations;
using System.Linq.Expressions;

namespace Podium.Infrastructure.Respositories
{
    public class GenericRepository<T> where T : class
    {
        private readonly PodiumContext _context;

        public GenericRepository(PodiumContext context)
        {
            _context = context;
        }
        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async void AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public async void UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public async void DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
            }
        }

        public async void SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<List<T>> GetAllIncludingAsync(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return await query.ToListAsync();
        }
    }
}

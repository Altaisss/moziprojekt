using backend.Context;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public interface IFilmRepository : IRepository<Film>
    {
        Task<bool> ExistsAsync(int id);
        Task<Film?> GetByIdWithVetitesekAsync(int id);
    }

    public class FilmRepository : Repository<Film>, IFilmRepository
    {
        public FilmRepository(MoziDbContext context) : base(context) { }

        public async Task<bool> ExistsAsync(int id)
            => await _dbSet.AsNoTracking().AnyAsync(f => f.Id == id);

        public async Task<Film?> GetByIdWithVetitesekAsync(int id)
        {
            return await _dbSet
                .Include(f => f.Vetitesek)
                .FirstOrDefaultAsync(f => f.Id == id);
        }
    }
}

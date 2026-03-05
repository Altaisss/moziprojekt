using backend.Context;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public interface IFilmRepository : IRepository<Film>
    {
        Task<bool> ExistsAsync(int id);
    }
    public class FilmRepository : Repository<Film>, IFilmRepository
    {
        public FilmRepository(MoziDbContext context) : base(context) { }

        public async Task<bool> ExistsAsync(int id)
            => await _dbSet.AnyAsync(f => f.Id == id);
    }
}

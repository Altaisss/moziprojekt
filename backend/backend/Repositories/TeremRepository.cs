using backend.Context;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public interface ITeremRepository : IRepository<Terem>
    {
        Task<bool> ExistsAsync(int id);
    }

    public class TeremRepository : Repository<Terem>, ITeremRepository
    {
        public TeremRepository(MoziDbContext context) : base(context) { }

        public async Task<bool> ExistsAsync(int id)
            => await _dbSet.AnyAsync(t => t.Id == id);
    }
}

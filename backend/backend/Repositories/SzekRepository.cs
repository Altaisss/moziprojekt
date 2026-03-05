using backend.Context;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public interface ISzekRepository : IRepository<Szek>
    {
        Task<bool> ExistsAsync(int id);
    }

    public class SzekRepository : Repository<Szek>, ISzekRepository
    {
        public SzekRepository(MoziDbContext context) : base(context) { }

        public async Task<bool> ExistsAsync(int id)
            => await _dbSet.AnyAsync(s => s.Id == id);
    }
}

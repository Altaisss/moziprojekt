using backend.Context;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public interface IVetitesRepository : IRepository<Vetites>
    {
        Task<bool> ExistsAsync(int id);
    }

    public class VetitesRepository : Repository<Vetites>, IVetitesRepository
    {
        public VetitesRepository(MoziDbContext context) : base(context) { }

        public async Task<bool> ExistsAsync(int id)
            => await _dbSet.AnyAsync(v => v.Id == id);
    }
}

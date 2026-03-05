using backend.Context;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public interface IFoglalasRepository : IRepository<Foglalas>
    {
        Task<IEnumerable<Foglalas>> GetByFelhasznaloIdAsync(int felhasznaloId);
        Task<bool> ExistsAsync(int id);
    }

    public class FoglalasRepository : Repository<Foglalas>, IFoglalasRepository
    {
        public FoglalasRepository(MoziDbContext context) : base(context) { }

        public async Task<IEnumerable<Foglalas>> GetByFelhasznaloIdAsync(int felhasznaloId)
            => await _dbSet.Where(f => f.FelhasznaloId == felhasznaloId).ToListAsync();

        public async Task<bool> ExistsAsync(int id)
            => await _dbSet.AnyAsync(f => f.Id == id);
    }
}

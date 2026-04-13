using backend.Context;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public interface IFoglalasRepository : IRepository<Foglalas>
    {
        Task<IEnumerable<Foglalas>> GetByFelhasznaloIdAsync(int felhasznaloId);
        Task<bool> ExistsAsync(int id);
        Task<Foglalas?> GetByIdWithHelyekAsync(int id);           
        void DeleteRangeFoglaltHely(IEnumerable<Foglalthely> helyek); 
    }

    public class FoglalasRepository : Repository<Foglalas>, IFoglalasRepository
    {
        public FoglalasRepository(MoziDbContext context) : base(context) { }

        public async Task<IEnumerable<Foglalas>> GetByFelhasznaloIdAsync(int felhasznaloId)
            => await _dbSet
                .Include(f => f.Foglalthely)
                .Where(f => f.FelhasznaloId == felhasznaloId)
                .ToListAsync();

        public async Task<bool> ExistsAsync(int id)
            => await _dbSet.AnyAsync(f => f.Id == id);

       
        public async Task<Foglalas?> GetByIdWithHelyekAsync(int id)
            => await _dbSet
                .Include(f => f.Foglalthely)
                .FirstOrDefaultAsync(f => f.Id == id);

      
        public void DeleteRangeFoglaltHely(IEnumerable<Foglalthely> helyek)
        {
            _context.Foglalthelyek.RemoveRange(helyek);
        }
    }
}

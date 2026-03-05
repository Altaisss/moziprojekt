using backend.Context;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public class FelhasznaloRepository : Repository<Felhasznalo>, IFelhasznaloRepository
    {
        public FelhasznaloRepository(MoziDbContext context) : base(context) { }

        public async Task<Felhasznalo?> GetByEmailAsync(string email)
            => await _dbSet.FirstOrDefaultAsync(f => f.Email == email);

        public async Task<bool> EmailExistsAsync(string email, int? excludeId = null)
            => await _dbSet.AnyAsync(f => f.Email == email && (excludeId == null || f.Id != excludeId));
    }
}

using backend.Context;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public interface IFoglaltHelyRepository : IRepository<Foglalthely>
    {
        Task<bool> IsSeatTakenAsync(int vetitesId, int szekId, int? excludeId = null);
    }

    public class FoglaltHelyRepository : Repository<Foglalthely>, IFoglaltHelyRepository
    {
        public FoglaltHelyRepository(MoziDbContext context) : base(context) { }

        public async Task<bool> IsSeatTakenAsync(int vetitesId, int szekId, int? excludeId = null)
            => await _dbSet.AnyAsync(fh =>
                fh.VetitesId == vetitesId &&
                fh.SzekId == szekId &&
                (excludeId == null || fh.Id != excludeId));
    }
}

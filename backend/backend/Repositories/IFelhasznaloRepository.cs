using backend.Models;

namespace backend.Repositories
{
    public interface IFelhasznaloRepository : IRepository<Felhasznalo>
    {
        Task<Felhasznalo?> GetByEmailAsync(string email);
        Task<bool> EmailExistsAsync(string email, int? excludeId = null);
    }
}

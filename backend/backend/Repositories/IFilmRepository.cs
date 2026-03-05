using backend.Models;

namespace backend.Repositories
{
    public interface IFilmRepository : IRepository<Film>
    {
        Task<bool> ExistsAsync(int id);
    }
}

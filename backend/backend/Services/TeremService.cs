using backend.DTOs;
using backend.Models;
using backend.Repositories;

namespace backend.Services
{
    public interface ITeremService
    {
        Task<IEnumerable<TeremResponse>> GetAllAsync();
        Task<TeremResponse?> GetByIdAsync(int id);
        Task<TeremResponse> CreateAsync(TeremRequest dto);
        Task<bool> UpdateAsync(int id, TeremRequest dto);
        Task<bool> DeleteAsync(int id);
    }

    public class TeremService : ITeremService
    {
        private readonly ITeremRepository _repo;

        public TeremService(ITeremRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<TeremResponse>> GetAllAsync()
        {
            var termek = await _repo.GetAllAsync();
            return termek.Select(ToResponse);
        }

        public async Task<TeremResponse?> GetByIdAsync(int id)
        {
            var terem = await _repo.GetByIdAsync(id);
            return terem == null ? null : ToResponse(terem);
        }

        public async Task<TeremResponse> CreateAsync(TeremRequest dto)
        {
            var terem = new Terem { TeremNev = dto.TeremNev };
            await _repo.CreateAsync(terem);
            return ToResponse(terem);
        }

        public async Task<bool> UpdateAsync(int id, TeremRequest dto)
        {
            var terem = await _repo.GetByIdAsync(id);
            if (terem == null) return false;

            terem.TeremNev = dto.TeremNev;
            await _repo.UpdateAsync(terem);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var terem = await _repo.GetByIdAsync(id);
            if (terem == null) return false;

            await _repo.DeleteAsync(terem);
            return true;
        }

        private static TeremResponse ToResponse(Terem t) => new()
        {
            Id = t.Id,
            TeremNev = t.TeremNev
        };
    }
}

using backend.DTOs;
using backend.Models;
using backend.Repositories;

namespace backend.Services
{
    public interface ISzekService
    {
        Task<IEnumerable<SzekResponse>> GetAllAsync();
        Task<SzekResponse?> GetByIdAsync(int id);
        Task<(bool Success, string? Error, SzekResponse? Result)> CreateAsync(SzekRequest dto);
        Task<(bool Success, string? Error)> UpdateAsync(int id, SzekRequest dto);
        Task<bool> DeleteAsync(int id);
    }

    public class SzekService : ISzekService
    {
        private readonly ISzekRepository _repo;
        private readonly ITeremRepository _teremRepo;

        public SzekService(ISzekRepository repo, ITeremRepository teremRepo)
        {
            _repo = repo;
            _teremRepo = teremRepo;
        }

        public async Task<IEnumerable<SzekResponse>> GetAllAsync()
        {
            var szekek = await _repo.GetAllAsync();
            return szekek.Select(ToResponse);
        }

        public async Task<SzekResponse?> GetByIdAsync(int id)
        {
            var szek = await _repo.GetByIdAsync(id);
            return szek == null ? null : ToResponse(szek);
        }

        public async Task<(bool Success, string? Error, SzekResponse? Result)> CreateAsync(SzekRequest dto)
        {
            if (!await _teremRepo.ExistsAsync(dto.TeremId))
                return (false, "A megadott terem nem létezik.", null);

            var szek = new Szek
            {
                Sor = dto.Sor,
                Szam = dto.Szam,
                Oldal = dto.Oldal,
                TeremId = dto.TeremId
            };

            await _repo.CreateAsync(szek);
            return (true, null, ToResponse(szek));
        }

        public async Task<(bool Success, string? Error)> UpdateAsync(int id, SzekRequest dto)
        {
            var szek = await _repo.GetByIdAsync(id);
            if (szek == null) return (false, null);

            if (!await _teremRepo.ExistsAsync(dto.TeremId))
                return (false, "A megadott terem nem létezik.");

            szek.Sor = dto.Sor;
            szek.Szam = dto.Szam;
            szek.Oldal = dto.Oldal;
            szek.TeremId = dto.TeremId;

            await _repo.UpdateAsync(szek);
            return (true, null);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var szek = await _repo.GetByIdAsync(id);
            if (szek == null) return false;

            await _repo.DeleteAsync(szek);
            return true;
        }

        private static SzekResponse ToResponse(Szek s) => new()
        {
            Id = s.Id,
            Sor = s.Sor,
            Szam = s.Szam,
            Oldal = s.Oldal,
            TeremId = s.TeremId
        };
    }
}

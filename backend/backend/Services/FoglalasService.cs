using backend.DTOs;
using backend.Models;
using backend.Repositories;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public interface IFoglalasService
    {
        Task<IEnumerable<FoglalasResponse>> GetByUserAsync(int felhasznaloId);
        Task<(bool Found, bool Owned, FoglalasResponse? Result)> GetByIdAsync(int id, int felhasznaloId);
        Task<(bool Success, string? Error, FoglalasResponse? Result)> CreateAsync(FoglalasRequest dto);
        Task<(bool Found, bool Owned, string? Error)> UpdateAsync(int id, FoglalasRequest dto, int felhasznaloId);
        Task<(bool Found, bool Owned)> DeleteAsync(int id, int felhasznaloId);
    }

    public class FoglalasService : IFoglalasService
    {
        private readonly IFoglalasRepository _repo;
        private readonly IFelhasznaloRepository _felhasznaloRepo;

        public FoglalasService(IFoglalasRepository repo, IFelhasznaloRepository felhasznaloRepo)
        {
            _repo = repo;
            _felhasznaloRepo = felhasznaloRepo;
        }

        public async Task<IEnumerable<FoglalasResponse>> GetByUserAsync(int felhasznaloId)
        {
            var foglalasok = await _repo.GetByFelhasznaloIdAsync(felhasznaloId);
            return foglalasok.Select(ToResponse);
        }

        public async Task<(bool Found, bool Owned, FoglalasResponse? Result)> GetByIdAsync(int id, int felhasznaloId)
        {
            var f = await _repo.GetByIdAsync(id);
            if (f == null) return (false, false, null);
            if (f.FelhasznaloId != felhasznaloId) return (true, false, null);
            return (true, true, ToResponse(f));
        }

        public async Task<(bool Success, string? Error, FoglalasResponse? Result)> CreateAsync(FoglalasRequest dto)
        {
            if (!await _felhasznaloRepo.EmailExistsAsync("", dto.FelhasznaloId) &&
                await _felhasznaloRepo.GetByIdAsync(dto.FelhasznaloId) == null)
                return (false, "A megadott felhasználó nem létezik.", null);

            var foglalas = new Foglalas { FelhasznaloId = dto.FelhasznaloId };
            await _repo.CreateAsync(foglalas);
            return (true, null, ToResponse(foglalas));
        }

        public async Task<(bool Found, bool Owned, string? Error)> UpdateAsync(int id, FoglalasRequest dto, int felhasznaloId)
        {
            var foglalas = await _repo.GetByIdAsync(id);
            if (foglalas == null) return (false, false, null);
            if (foglalas.FelhasznaloId != felhasznaloId) return (true, false, null);

            if (await _felhasznaloRepo.GetByIdAsync(dto.FelhasznaloId) == null)
                return (true, true, "A megadott felhasználó nem létezik.");

            foglalas.FelhasznaloId = dto.FelhasznaloId;
            await _repo.UpdateAsync(foglalas);
            return (true, true, null);
        }

        public async Task<(bool Found, bool Owned)> DeleteAsync(int id, int felhasznaloId)
        {
            var foglalas = await _repo.GetByIdWithHelyekAsync(id);

            if (foglalas == null)
                return (false, false);

            if (foglalas.FelhasznaloId != felhasznaloId)
                return (true, false);

            // Delete related seats first
            if (foglalas.Foglalthely != null && foglalas.Foglalthely.Any())
            {
                _repo.DeleteRangeFoglaltHely(foglalas.Foglalthely);
            }

            // Delete the foglalas
            await _repo.DeleteAsync(foglalas);

            return (true, true);
        }

        private static FoglalasResponse ToResponse(Foglalas f) => new()
        {
            Id = f.Id,
            FelhasznaloId = f.FelhasznaloId,
            Foglalthely = f.Foglalthely.Select(h => new FoglaltHelyResponse
            {
                Id = h.Id,
                SzekId = h.SzekId,
                FoglalasId = h.FoglalasId,
                VetitesId = h.VetitesId
            }).ToList()
        };

    }
}

using backend.DTOs;
using backend.Models;
using backend.Repositories;

namespace backend.Services
{
    public interface IFoglaltHelyService
    {
        Task<IEnumerable<FoglaltHelyResponse>> GetAllAsync();
        Task<FoglaltHelyResponse?> GetByIdAsync(int id);
        Task<(bool Success, string? Error, FoglaltHelyResponse? Result)> CreateAsync(FoglaltHelyRequest dto);
        Task<(bool Success, string? Error)> UpdateAsync(int id, FoglaltHelyRequest dto);
        Task<bool> DeleteAsync(int id);
    }

    public class FoglaltHelyService : IFoglaltHelyService
    {
        private readonly IFoglaltHelyRepository _repo;
        private readonly ISzekRepository _szekRepo;
        private readonly IFoglalasRepository _foglalasRepo;
        private readonly IVetitesRepository _vetitesRepo;

        public FoglaltHelyService(
            IFoglaltHelyRepository repo,
            ISzekRepository szekRepo,
            IFoglalasRepository foglalasRepo,
            IVetitesRepository vetitesRepo)
        {
            _repo = repo;
            _szekRepo = szekRepo;
            _foglalasRepo = foglalasRepo;
            _vetitesRepo = vetitesRepo;
        }

        public async Task<IEnumerable<FoglaltHelyResponse>> GetAllAsync()
        {
            var items = await _repo.GetAllAsync();
            return items.Select(ToResponse);
        }

        public async Task<FoglaltHelyResponse?> GetByIdAsync(int id)
        {
            var fh = await _repo.GetByIdAsync(id);
            return fh == null ? null : ToResponse(fh);
        }

        public async Task<(bool Success, string? Error, FoglaltHelyResponse? Result)> CreateAsync(FoglaltHelyRequest dto)
        {
            if (!await _szekRepo.ExistsAsync(dto.SzekId) ||
                !await _foglalasRepo.ExistsAsync(dto.FoglalasId) ||
                !await _vetitesRepo.ExistsAsync(dto.VetitesId))
                return (false, "A megadott szék, foglalás vagy vetítés nem létezik.", null);

            if (await _repo.IsSeatTakenAsync(dto.VetitesId, dto.SzekId))
                return (false, "Ez a szék már foglalt erre a vetítésre.", null);

            var fh = new Foglalthely
            {
                SzekId = dto.SzekId,
                FoglalasId = dto.FoglalasId,
                VetitesId = dto.VetitesId
            };

            await _repo.CreateAsync(fh);
            return (true, null, ToResponse(fh));
        }

        public async Task<(bool Success, string? Error)> UpdateAsync(int id, FoglaltHelyRequest dto)
        {
            var fh = await _repo.GetByIdAsync(id);
            if (fh == null) return (false, null);

            if (!await _szekRepo.ExistsAsync(dto.SzekId) ||
                !await _foglalasRepo.ExistsAsync(dto.FoglalasId) ||
                !await _vetitesRepo.ExistsAsync(dto.VetitesId))
                return (false, "A megadott szék, foglalás vagy vetítés nem létezik.");

            if (await _repo.IsSeatTakenAsync(dto.VetitesId, dto.SzekId, excludeId: id))
                return (false, "Ez a szék már foglalt erre a vetítésre.");

            fh.SzekId = dto.SzekId;
            fh.FoglalasId = dto.FoglalasId;
            fh.VetitesId = dto.VetitesId;

            await _repo.UpdateAsync(fh);
            return (true, null);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var fh = await _repo.GetByIdAsync(id);
            if (fh == null) return false;

            await _repo.DeleteAsync(fh);
            return true;
        }

        private static FoglaltHelyResponse ToResponse(Foglalthely fh) => new()
        {
            Id = fh.Id,
            SzekId = fh.SzekId,
            FoglalasId = fh.FoglalasId,
            VetitesId = fh.VetitesId
        };
    }
}

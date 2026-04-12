using backend.DTOs;
using backend.Models;
using backend.Repositories;

namespace backend.Services
{
    public interface IVetitesService
    {
        Task<IEnumerable<VetitesResponse>> GetAllAsync();
        Task<VetitesResponse?> GetByIdAsync(int id);
        Task<(bool Success, string? Error, VetitesResponse? Result)> CreateAsync(VetitesRequest dto);
        Task<(bool Success, string? Error)> UpdateAsync(int id, VetitesRequest dto);
        Task<bool> DeleteAsync(int id);
    }

    public class VetitesService : IVetitesService
    {
        private readonly IVetitesRepository _repo;
        private readonly IFilmRepository _filmRepo;
        private readonly ITeremRepository _teremRepo;

        public VetitesService(IVetitesRepository repo, IFilmRepository filmRepo, ITeremRepository teremRepo)
        {
            _repo = repo;
            _filmRepo = filmRepo;
            _teremRepo = teremRepo;
        }

        public async Task<IEnumerable<VetitesResponse>> GetAllAsync()
        {
            var vetitesek = await _repo.GetAllAsync();
            return vetitesek.Select(ToResponse);
        }

        public async Task<VetitesResponse?> GetByIdAsync(int id)
        {
            var v = await _repo.GetByIdAsync(id);
            return v == null ? null : ToResponse(v);
        }

        public async Task<(bool Success, string? Error, VetitesResponse? Result)> CreateAsync(VetitesRequest dto)
        {
            if (!await _filmRepo.ExistsAsync(dto.FilmId) || !await _teremRepo.ExistsAsync(dto.TeremId))
                return (false, "A megadott film vagy terem nem létezik.", null);

            var vetites = new Vetites
            {
                Idopont = dto.Idopont,
                TeremId = dto.TeremId,
                FilmId = dto.FilmId,
                JegyAr = dto.JegyAr,
                Nyelv = dto.Nyelv,
            };

            await _repo.CreateAsync(vetites);
            return (true, null, ToResponse(vetites));
        }

        public async Task<(bool Success, string? Error)> UpdateAsync(int id, VetitesRequest dto)
        {
            var vetites = await _repo.GetByIdAsync(id);
            if (vetites == null) return (false, null);

            if (!await _filmRepo.ExistsAsync(dto.FilmId) || !await _teremRepo.ExistsAsync(dto.TeremId))
                return (false, "A megadott film vagy terem nem létezik.");

            vetites.Idopont = dto.Idopont;
            vetites.TeremId = dto.TeremId;
            vetites.FilmId = dto.FilmId;
            vetites.JegyAr = dto.JegyAr;
            vetites.Nyelv = dto.Nyelv;

            await _repo.UpdateAsync(vetites);
            return (true, null);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var vetites = await _repo.GetByIdAsync(id);
            if (vetites == null) return false;

            await _repo.DeleteAsync(vetites);
            return true;
        }

        private static VetitesResponse ToResponse(Vetites v) => new()
        {
            Id = v.Id,
            Idopont = v.Idopont,
            TeremId = v.TeremId,
            FilmId = v.FilmId,
            JegyAr = v.JegyAr,
            Nyelv = v.Nyelv
        };
    }
}

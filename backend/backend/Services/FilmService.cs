using backend.DTOs;
using backend.Models;
using backend.Repositories;

namespace backend.Services
{
    public interface IFilmService
    {
        Task<IEnumerable<FilmResponse>> GetAllAsync();
        Task<FilmResponse?> GetByIdAsync(int id);
        Task<FilmResponse> CreateAsync(FilmRequest dto);
        Task<bool> UpdateAsync(int id, FilmRequest dto);
        Task<bool> DeleteAsync(int id);
    }

    public class FilmService : IFilmService
    {
        private readonly IFilmRepository _repo;

        public FilmService(IFilmRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<FilmResponse>> GetAllAsync()
        {
            var filmek = await _repo.GetAllAsync();
            return filmek.Select(ToResponse);
        }

        public async Task<FilmResponse?> GetByIdAsync(int id)
        {
            var film = await _repo.GetByIdAsync(id);
            return film == null ? null : ToResponse(film);
        }

        public async Task<FilmResponse> CreateAsync(FilmRequest dto)
        {
            var film = new Film
            {
                Cim = dto.Cim,
                Rendezo = dto.Rendezo,
                Hossz = dto.Hossz,
                Leiras = dto.Leiras
            };

            await _repo.CreateAsync(film);
            return ToResponse(film);
        }

        public async Task<bool> UpdateAsync(int id, FilmRequest dto)
        {
            var film = await _repo.GetByIdAsync(id);
            if (film == null) return false;

            film.Cim = dto.Cim;
            film.Rendezo = dto.Rendezo;
            film.Hossz = dto.Hossz;
            film.Leiras = dto.Leiras;

            await _repo.UpdateAsync(film);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var film = await _repo.GetByIdAsync(id);
            if (film == null) return false;

            await _repo.DeleteAsync(film);
            return true;
        }

        private static FilmResponse ToResponse(Film f) => new()
        {
            Id = f.Id,
            Cim = f.Cim,
            Rendezo = f.Rendezo,
            Hossz = f.Hossz,
            Leiras = f.Leiras
        };
    }
}

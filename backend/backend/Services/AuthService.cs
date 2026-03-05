using backend.DTOs;
using backend.Models;
using backend.Repositories;

namespace backend.Services
{
    public interface IAuthService
    {
        Task<(bool Success, string Message)> RegisterAsync(FelhasznaloRequest dto);
        Task<(bool Success, string? Token, string Message)> LoginAsync(LoginRequest dto);
    }

    public class AuthService : IAuthService
    {
        private readonly IFelhasznaloRepository _felhasznaloRepo;
        private readonly IJwtService _jwtService;

        public AuthService(IFelhasznaloRepository felhasznaloRepo, IJwtService jwtService)
        {
            _felhasznaloRepo = felhasznaloRepo;
            _jwtService = jwtService;
        }

        public async Task<(bool Success, string Message)> RegisterAsync(FelhasznaloRequest dto)
        {
            if (await _felhasznaloRepo.EmailExistsAsync(dto.Email))
                return (false, "Ez az email cím már foglalt.");

            var felhasznalo = new Felhasznalo
            {
                Nev = dto.Nev,
                Email = dto.Email,
                Jelszo = BCrypt.Net.BCrypt.HashPassword(dto.Jelszo)
            };

            await _felhasznaloRepo.CreateAsync(felhasznalo);
            return (true, "Sikeres regisztráció.");
        }

        public async Task<(bool Success, string? Token, string Message)> LoginAsync(LoginRequest dto)
        {
            var felhasznalo = await _felhasznaloRepo.GetByEmailAsync(dto.Email);

            if (felhasznalo == null || !BCrypt.Net.BCrypt.Verify(dto.Jelszo, felhasznalo.Jelszo))
                return (false, null, "Hibás email vagy jelszó.");

            var token = _jwtService.GenerateToken(felhasznalo);
            return (true, token, "Sikeres bejelentkezés.");
        }
    }
}

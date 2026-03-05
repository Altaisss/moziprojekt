using backend.Context;
using backend.DTOs;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly MoziDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(MoziDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // POST api/auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] FelhasznaloRequest dto)
        {
            if (await _context.Felhasznalok.AnyAsync(f => f.Email == dto.Email))
                return Conflict("Ez az email cím már foglalt.");

            var felhasznalo = new Felhasznalo
            {
                Nev = dto.Nev,
                Email = dto.Email,
                Jelszo = BCrypt.Net.BCrypt.HashPassword(dto.Jelszo) // Fix #1: hash the password
            };

            _context.Felhasznalok.Add(felhasznalo);
            await _context.SaveChangesAsync();

            return Ok("Sikeres regisztráció.");
        }

        // POST api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest dto)
        {
            // Fix #1: look up by email only, then verify hash
            var felhasznalo = await _context.Felhasznalok
                .FirstOrDefaultAsync(f => f.Email == dto.Email);

            if (felhasznalo == null || !BCrypt.Net.BCrypt.Verify(dto.Jelszo, felhasznalo.Jelszo))
                return Unauthorized("Hibás email vagy jelszó.");

            var token = GenerateJwtToken(felhasznalo);
            return Ok(new { token });
        }

        private string GenerateJwtToken(Felhasznalo felhasznalo)
        {
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, felhasznalo.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, felhasznalo.Email),
                new Claim("nev", felhasznalo.Nev)
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(8),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

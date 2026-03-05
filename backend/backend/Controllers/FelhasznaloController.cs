using backend.Context;
using backend.DTOs;
using backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace backend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class FelhasznaloController : ControllerBase
    {
        private readonly MoziDbContext _context;

        public FelhasznaloController(MoziDbContext context)
        {
            _context = context;
        }

        // Fix #3: GET api/felhasznalo — only admins should list all users.
        // For now we restrict it to returning only the currently logged-in user's own data.
        // If you add an "admin" role later, you can gate this with [Authorize(Roles = "Admin")].
        [HttpGet]
        public async Task<ActionResult<FelhasznaloResponse>> GetMe()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)
                ?? User.FindFirstValue(JwtRegisteredClaimNames.Sub)!);

            var felhasznalo = await _context.Felhasznalok.FindAsync(userId);

            if (felhasznalo == null)
                return NotFound();

            return Ok(new FelhasznaloResponse
            {
                Id = felhasznalo.Id,
                Nev = felhasznalo.Nev,
                Email = felhasznalo.Email
                // Fix #2: Jelszo not returned
            });
        }

        // GET api/felhasznalo/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<FelhasznaloResponse>> GetById(int id)
        {
            var felhasznalo = await _context.Felhasznalok.FindAsync(id);

            if (felhasznalo == null)
                return NotFound();

            return Ok(new FelhasznaloResponse
            {
                Id = felhasznalo.Id,
                Nev = felhasznalo.Nev,
                Email = felhasznalo.Email
                // Fix #2: Jelszo not returned
            });
        }

        // Fix #3: POST (Create) removed — registration is handled by AuthController

        // PUT api/felhasznalo/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] FelhasznaloRequest dto)
        {
            var felhasznalo = await _context.Felhasznalok.FindAsync(id);

            if (felhasznalo == null)
                return NotFound();

            if (await _context.Felhasznalok.AnyAsync(f => f.Email == dto.Email && f.Id != id))
                return Conflict("Ez az email cím már foglalt.");

            felhasznalo.Nev = dto.Nev;
            felhasznalo.Email = dto.Email;
            felhasznalo.Jelszo = BCrypt.Net.BCrypt.HashPassword(dto.Jelszo); // Fix #1: hash on update too

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE api/felhasznalo/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var felhasznalo = await _context.Felhasznalok.FindAsync(id);

            if (felhasznalo == null)
                return NotFound();

            _context.Felhasznalok.Remove(felhasznalo);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

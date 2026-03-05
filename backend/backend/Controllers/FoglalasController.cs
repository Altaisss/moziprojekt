using backend.Context;
using backend.DTOs;
using backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;

namespace backend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class FoglalasController : ControllerBase
    {
        private readonly MoziDbContext _context;

        public FoglalasController(MoziDbContext context)
        {
            _context = context;
        }

        // Fix #5: GET api/foglalas — only returns bookings belonging to the logged-in user
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FoglalasResponse>>> GetAll()
        {
            var userId = GetCurrentUserId();

            var foglalasok = await _context.Foglalasok
                .Where(f => f.FelhasznaloId == userId)
                .Select(f => new FoglalasResponse
                {
                    Id = f.Id,
                    FelhasznaloId = f.FelhasznaloId
                })
                .ToListAsync();

            return Ok(foglalasok);
        }

        // GET api/foglalas/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<FoglalasResponse>> GetById(int id)
        {
            var userId = GetCurrentUserId();
            var foglalas = await _context.Foglalasok.FindAsync(id);

            if (foglalas == null)
                return NotFound();

            // Fix #5: users can only view their own bookings
            if (foglalas.FelhasznaloId != userId)
                return Forbid();

            return Ok(new FoglalasResponse
            {
                Id = foglalas.Id,
                FelhasznaloId = foglalas.FelhasznaloId
            });
        }

        // POST api/foglalas
        [HttpPost]
        public async Task<ActionResult<FoglalasResponse>> Create([FromBody] FoglalasRequest dto)
        {
            var felhasznaloExists = await _context.Felhasznalok.AnyAsync(f => f.Id == dto.FelhasznaloId);

            if (!felhasznaloExists)
                return BadRequest("A megadott felhasználó nem létezik.");

            var foglalas = new Foglalas
            {
                FelhasznaloId = dto.FelhasznaloId
            };

            _context.Foglalasok.Add(foglalas);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = foglalas.Id }, new FoglalasResponse
            {
                Id = foglalas.Id,
                FelhasznaloId = foglalas.FelhasznaloId
            });
        }

        // PUT api/foglalas/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] FoglalasRequest dto)
        {
            var userId = GetCurrentUserId();
            var foglalas = await _context.Foglalasok.FindAsync(id);

            if (foglalas == null)
                return NotFound();

            if (foglalas.FelhasznaloId != userId)
                return Forbid();

            var felhasznaloExists = await _context.Felhasznalok.AnyAsync(f => f.Id == dto.FelhasznaloId);

            if (!felhasznaloExists)
                return BadRequest("A megadott felhasználó nem létezik.");

            foglalas.FelhasznaloId = dto.FelhasznaloId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE api/foglalas/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = GetCurrentUserId();
            var foglalas = await _context.Foglalasok.FindAsync(id);

            if (foglalas == null)
                return NotFound();

            if (foglalas.FelhasznaloId != userId)
                return Forbid();

            _context.Foglalasok.Remove(foglalas);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private int GetCurrentUserId()
        {
            // JWT "sub" claim is mapped to NameIdentifier by ASP.NET Core
            var value = User.FindFirstValue(ClaimTypes.NameIdentifier)
                        ?? User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            return int.Parse(value!);
        }
    }
}

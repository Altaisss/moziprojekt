using backend.Context;
using backend.DTOs;
using backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        // GET api/felhasznalo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FelhasznaloResponse>>> GetAll()
        {
            var felhasznalok = await _context.Felhasznalok
                .Select(f => new FelhasznaloResponse
                {
                    Id = f.Id,
                    Nev = f.Nev,
                    Email = f.Email,
                    Jelszo = f.Jelszo
                })
                .ToListAsync();

            return Ok(felhasznalok);
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
                Email = felhasznalo.Email,
                Jelszo = felhasznalo.Jelszo
            });
        }

        // POST api/felhasznalo
        [HttpPost]
        public async Task<ActionResult<FelhasznaloResponse>> Create([FromBody] FelhasznaloRequest dto)
        {
            if (await _context.Felhasznalok.AnyAsync(f => f.Email == dto.Email))
                return Conflict("Ez az email cím már foglalt.");

            var felhasznalo = new Felhasznalo
            {
                Nev = dto.Nev,
                Email = dto.Email,
                Jelszo = dto.Jelszo
            };

            _context.Felhasznalok.Add(felhasznalo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = felhasznalo.Id }, new FelhasznaloResponse
            {
                Id = felhasznalo.Id,
                Nev = felhasznalo.Nev,
                Email = felhasznalo.Email,
                Jelszo = felhasznalo.Jelszo
            });
        }

        // PUT api/felhasznalo/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] FelhasznaloRequest dto)
        {
            var felhasznalo = await _context.Felhasznalok.FindAsync(id);

            if (felhasznalo == null)
                return NotFound();

            // Check email uniqueness, excluding current user
            if (await _context.Felhasznalok.AnyAsync(f => f.Email == dto.Email && f.Id != id))
                return Conflict("Ez az email cím már foglalt.");

            felhasznalo.Nev = dto.Nev;
            felhasznalo.Email = dto.Email;
            felhasznalo.Jelszo = dto.Jelszo;

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

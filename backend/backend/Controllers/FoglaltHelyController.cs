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
    public class FoglaltHelyController : ControllerBase
    {
        private readonly MoziDbContext _context;

        public FoglaltHelyController(MoziDbContext context)
        {
            _context = context;
        }

        // GET api/foglalthely
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FoglaltHelyResponse>>> GetAll()
        {
            var foglalthelyek = await _context.Foglalthelyek
                .Select(fh => new FoglaltHelyResponse
                {
                    Id = fh.Id,
                    SzekId = fh.SzekId,
                    FoglalasId = fh.FoglalasId,
                    VetitesId = fh.VetitesId
                })
                .ToListAsync();

            return Ok(foglalthelyek);
        }

        // GET api/foglalthely/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<FoglaltHelyResponse>> GetById(int id)
        {
            var foglalthely = await _context.Foglalthelyek.FindAsync(id);

            if (foglalthely == null)
                return NotFound();

            return Ok(new FoglaltHelyResponse
            {
                Id = foglalthely.Id,
                SzekId = foglalthely.SzekId,
                FoglalasId = foglalthely.FoglalasId,
                VetitesId = foglalthely.VetitesId
            });
        }

        // POST api/foglalthely
        [HttpPost]
        public async Task<ActionResult<FoglaltHelyResponse>> Create([FromBody] FoglaltHelyRequest dto)
        {
            var szekExists = await _context.Szekek.AnyAsync(s => s.Id == dto.SzekId);
            var foglalasExists = await _context.Foglalasok.AnyAsync(f => f.Id == dto.FoglalasId);
            var vetitesExists = await _context.Vetitesek.AnyAsync(v => v.Id == dto.VetitesId);

            if (!szekExists || !foglalasExists || !vetitesExists)
                return BadRequest("A megadott szék, foglalás vagy vetítés nem létezik.");

            // Check unique constraint: seat already taken for this screening
            var alreadyTaken = await _context.Foglalthelyek
                .AnyAsync(fh => fh.VetitesId == dto.VetitesId && fh.SzekId == dto.SzekId);

            if (alreadyTaken)
                return Conflict("Ez a szék már foglalt erre a vetítésre.");

            var foglalthely = new Foglalthely
            {
                SzekId = dto.SzekId,
                FoglalasId = dto.FoglalasId,
                VetitesId = dto.VetitesId
            };

            _context.Foglalthelyek.Add(foglalthely);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = foglalthely.Id }, new FoglaltHelyResponse
            {
                Id = foglalthely.Id,
                SzekId = foglalthely.SzekId,
                FoglalasId = foglalthely.FoglalasId,
                VetitesId = foglalthely.VetitesId
            });
        }

        // PUT api/foglalthely/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] FoglaltHelyRequest dto)
        {
            var foglalthely = await _context.Foglalthelyek.FindAsync(id);

            if (foglalthely == null)
                return NotFound();

            var szekExists = await _context.Szekek.AnyAsync(s => s.Id == dto.SzekId);
            var foglalasExists = await _context.Foglalasok.AnyAsync(f => f.Id == dto.FoglalasId);
            var vetitesExists = await _context.Vetitesek.AnyAsync(v => v.Id == dto.VetitesId);

            if (!szekExists || !foglalasExists || !vetitesExists)
                return BadRequest("A megadott szék, foglalás vagy vetítés nem létezik.");

            // Check unique constraint, excluding the current record
            var alreadyTaken = await _context.Foglalthelyek
                .AnyAsync(fh => fh.VetitesId == dto.VetitesId && fh.SzekId == dto.SzekId && fh.Id != id);

            if (alreadyTaken)
                return Conflict("Ez a szék már foglalt erre a vetítésre.");

            foglalthely.SzekId = dto.SzekId;
            foglalthely.FoglalasId = dto.FoglalasId;
            foglalthely.VetitesId = dto.VetitesId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE api/foglalthely/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var foglalthely = await _context.Foglalthelyek.FindAsync(id);

            if (foglalthely == null)
                return NotFound();

            _context.Foglalthelyek.Remove(foglalthely);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

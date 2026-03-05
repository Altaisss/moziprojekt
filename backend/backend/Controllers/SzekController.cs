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
    public class SzekController : ControllerBase
    {
        private readonly MoziDbContext _context;

        public SzekController(MoziDbContext context)
        {
            _context = context;
        }

        // GET api/szek
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SzekResponse>>> GetAll()
        {
            var szekek = await _context.Szekek
                .Select(s => new SzekResponse
                {
                    Id = s.Id,
                    Sor = s.Sor,
                    Szam = s.Szam,
                    Oldal = s.Oldal,
                    TeremId = s.TeremId
                })
                .ToListAsync();

            return Ok(szekek);
        }

        // GET api/szek/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<SzekResponse>> GetById(int id)
        {
            var szek = await _context.Szekek.FindAsync(id);

            if (szek == null)
                return NotFound();

            return Ok(new SzekResponse
            {
                Id = szek.Id,
                Sor = szek.Sor,
                Szam = szek.Szam,
                Oldal = szek.Oldal,
                TeremId = szek.TeremId
            });
        }

        // POST api/szek
        [HttpPost]
        public async Task<ActionResult<SzekResponse>> Create([FromBody] SzekRequest dto)
        {
            var teremExists = await _context.Termek.AnyAsync(t => t.Id == dto.TeremId);

            if (!teremExists)
                return BadRequest("A megadott terem nem létezik.");

            var szek = new Szek
            {
                Sor = dto.Sor,
                Szam = dto.Szam,
                Oldal = dto.Oldal,
                TeremId = dto.TeremId
            };

            _context.Szekek.Add(szek);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = szek.Id }, new SzekResponse
            {
                Id = szek.Id,
                Sor = szek.Sor,
                Szam = szek.Szam,
                Oldal = szek.Oldal,
                TeremId = szek.TeremId
            });
        }

        // PUT api/szek/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] SzekRequest dto)
        {
            var szek = await _context.Szekek.FindAsync(id);

            if (szek == null)
                return NotFound();

            var teremExists = await _context.Termek.AnyAsync(t => t.Id == dto.TeremId);

            if (!teremExists)
                return BadRequest("A megadott terem nem létezik.");

            szek.Sor = dto.Sor;
            szek.Szam = dto.Szam;
            szek.Oldal = dto.Oldal;
            szek.TeremId = dto.TeremId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE api/szek/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var szek = await _context.Szekek.FindAsync(id);

            if (szek == null)
                return NotFound();

            _context.Szekek.Remove(szek);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

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
    public class VetitesController : ControllerBase
    {
        private readonly MoziDbContext _context;

        public VetitesController(MoziDbContext context)
        {
            _context = context;
        }

        // Fix #7: guests can browse screenings without logging in
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VetitesResponse>>> GetAll()
        {
            var vetitesek = await _context.Vetitesek
                .Select(v => new VetitesResponse
                {
                    Id = v.Id,
                    Idopont = v.Idopont,
                    TeremId = v.TeremId,
                    FilmId = v.FilmId,
                    JegyAr = v.JegyAr,
                    Nyelv = v.Nyelv,
                    VetitesTipus = v.VetitesTipus
                })
                .ToListAsync();

            return Ok(vetitesek);
        }

        // Fix #7: guests can view a single screening too
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<VetitesResponse>> GetById(int id)
        {
            var vetites = await _context.Vetitesek.FindAsync(id);

            if (vetites == null)
                return NotFound();

            return Ok(new VetitesResponse
            {
                Id = vetites.Id,
                Idopont = vetites.Idopont,
                TeremId = vetites.TeremId,
                FilmId = vetites.FilmId,
                JegyAr = vetites.JegyAr,
                Nyelv = vetites.Nyelv,
                VetitesTipus = vetites.VetitesTipus
            });
        }

        [HttpPost]
        public async Task<ActionResult<VetitesResponse>> Create([FromBody] VetitesRequest dto)
        {
            var filmExists = await _context.Filmek.AnyAsync(f => f.Id == dto.FilmId);
            var teremExists = await _context.Termek.AnyAsync(t => t.Id == dto.TeremId);

            if (!filmExists || !teremExists)
                return BadRequest("A megadott film vagy terem nem létezik.");

            var vetites = new Vetites
            {
                Idopont = dto.Idopont,
                TeremId = dto.TeremId,
                FilmId = dto.FilmId,
                JegyAr = dto.JegyAr,
                Nyelv = dto.Nyelv,
                VetitesTipus = dto.VetitesTipus
            };

            _context.Vetitesek.Add(vetites);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = vetites.Id }, new VetitesResponse
            {
                Id = vetites.Id,
                Idopont = vetites.Idopont,
                TeremId = vetites.TeremId,
                FilmId = vetites.FilmId,
                JegyAr = vetites.JegyAr,
                Nyelv = vetites.Nyelv,
                VetitesTipus = vetites.VetitesTipus
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] VetitesRequest dto)
        {
            var vetites = await _context.Vetitesek.FindAsync(id);

            if (vetites == null)
                return NotFound();

            var filmExists = await _context.Filmek.AnyAsync(f => f.Id == dto.FilmId);
            var teremExists = await _context.Termek.AnyAsync(t => t.Id == dto.TeremId);

            if (!filmExists || !teremExists)
                return BadRequest("A megadott film vagy terem nem létezik.");

            vetites.Idopont = dto.Idopont;
            vetites.TeremId = dto.TeremId;
            vetites.FilmId = dto.FilmId;
            vetites.JegyAr = dto.JegyAr;
            vetites.Nyelv = dto.Nyelv;
            vetites.VetitesTipus = dto.VetitesTipus;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var vetites = await _context.Vetitesek.FindAsync(id);

            if (vetites == null)
                return NotFound();

            _context.Vetitesek.Remove(vetites);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

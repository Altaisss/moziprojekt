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
    public class FilmController : ControllerBase
    {
        private readonly MoziDbContext _context;

        public FilmController(MoziDbContext context)
        {
            _context = context;
        }

        // GET api/film
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FilmResponse>>> GetAll()
        {
            var filmek = await _context.Filmek
                .Select(f => new FilmResponse
                {
                    Id = f.Id,
                    Cim = f.Cim,
                    Rendezo = f.Rendezo,
                    Hossz = f.Hossz,
                    Leiras = f.Leiras
                })
                .ToListAsync();

            return Ok(filmek);
        }

        // GET api/film/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<FilmResponse>> GetById(int id)
        {
            var film = await _context.Filmek.FindAsync(id);

            if (film == null)
                return NotFound();

            return Ok(new FilmResponse
            {
                Id = film.Id,
                Cim = film.Cim,
                Rendezo = film.Rendezo,
                Hossz = film.Hossz,
                Leiras = film.Leiras
            });
        }

        // POST api/film
        [HttpPost]
        public async Task<ActionResult<FilmResponse>> Create([FromBody] FilmRequest dto)
        {
            var film = new Film
            {
                Cim = dto.Cim,
                Rendezo = dto.Rendezo,
                Hossz = dto.Hossz,
                Leiras = dto.Leiras
            };

            _context.Filmek.Add(film);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = film.Id }, new FilmResponse
            {
                Id = film.Id,
                Cim = film.Cim,
                Rendezo = film.Rendezo,
                Hossz = film.Hossz,
                Leiras = film.Leiras
            });
        }

        // PUT api/film/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] FilmRequest dto)
        {
            var film = await _context.Filmek.FindAsync(id);

            if (film == null)
                return NotFound();

            film.Cim = dto.Cim;
            film.Rendezo = dto.Rendezo;
            film.Hossz = dto.Hossz;
            film.Leiras = dto.Leiras;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE api/film/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var film = await _context.Filmek.FindAsync(id);

            if (film == null)
                return NotFound();

            _context.Filmek.Remove(film);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

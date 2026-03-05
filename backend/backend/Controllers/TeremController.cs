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
    public class TeremController : ControllerBase
    {
        private readonly MoziDbContext _context;

        public TeremController(MoziDbContext context)
        {
            _context = context;
        }

        // GET api/terem
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeremResponse>>> GetAll()
        {
            var termek = await _context.Termek
                .Select(t => new TeremResponse
                {
                    Id = t.Id,
                    TeremNev = t.TeremNev
                })
                .ToListAsync();

            return Ok(termek);
        }

        // GET api/terem/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<TeremResponse>> GetById(int id)
        {
            var terem = await _context.Termek.FindAsync(id);

            if (terem == null)
                return NotFound();

            return Ok(new TeremResponse
            {
                Id = terem.Id,
                TeremNev = terem.TeremNev
            });
        }

        // POST api/terem
        [HttpPost]
        public async Task<ActionResult<TeremResponse>> Create([FromBody] TeremRequest dto)
        {
            var terem = new Terem
            {
                TeremNev = dto.TeremNev
            };

            _context.Termek.Add(terem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = terem.Id }, new TeremResponse
            {
                Id = terem.Id,
                TeremNev = terem.TeremNev
            });
        }

        // PUT api/terem/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TeremRequest dto)
        {
            var terem = await _context.Termek.FindAsync(id);

            if (terem == null)
                return NotFound();

            terem.TeremNev = dto.TeremNev;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE api/terem/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var terem = await _context.Termek.FindAsync(id);

            if (terem == null)
                return NotFound();

            _context.Termek.Remove(terem);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

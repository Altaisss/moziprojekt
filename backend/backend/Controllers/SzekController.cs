using backend.DTOs;
using backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SzekController : ControllerBase
    {
        private readonly ISzekService _szekService;

        public SzekController(ISzekService szekService)
        {
            _szekService = szekService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _szekService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _szekService.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SzekRequest dto)
        {
            var (success, error, result) = await _szekService.CreateAsync(dto);
            if (!success) return BadRequest(error);
            return CreatedAtAction(nameof(GetById), new { id = result!.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] SzekRequest dto)
        {
            var (success, error) = await _szekService.UpdateAsync(id, dto);
            if (error != null) return BadRequest(error);
            return success ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _szekService.DeleteAsync(id);
            return success ? NoContent() : NotFound();
        }
    }
}

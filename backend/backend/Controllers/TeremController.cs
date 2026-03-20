using backend.DTOs;
using backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class TeremController : ControllerBase
    {
        private readonly ITeremService _teremService;

        public TeremController(ITeremService teremService)
        {
            _teremService = teremService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _teremService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _teremService.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TeremRequest dto)
        {
            var result = await _teremService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TeremRequest dto)
        {
            var success = await _teremService.UpdateAsync(id, dto);
            return success ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _teremService.DeleteAsync(id);
            return success ? NoContent() : NotFound();
        }
    }
}

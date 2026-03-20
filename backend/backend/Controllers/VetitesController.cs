using backend.DTOs;
using backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class VetitesController : ControllerBase
    {
        private readonly IVetitesService _vetitesService;

        public VetitesController(IVetitesService vetitesService)
        {
            _vetitesService = vetitesService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _vetitesService.GetAllAsync());

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _vetitesService.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] VetitesRequest dto)
        {
            var (success, error, result) = await _vetitesService.CreateAsync(dto);
            if (!success) return BadRequest(error);
            return CreatedAtAction(nameof(GetById), new { id = result!.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] VetitesRequest dto)
        {
            var (success, error) = await _vetitesService.UpdateAsync(id, dto);
            if (error != null) return BadRequest(error);
            return success ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _vetitesService.DeleteAsync(id);
            return success ? NoContent() : NotFound();
        }
    }
}

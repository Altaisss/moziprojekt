using backend.DTOs;
using backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class FoglaltHelyController : ControllerBase
    {
        private readonly IFoglaltHelyService _foglaltHelyService;

        public FoglaltHelyController(IFoglaltHelyService foglaltHelyService)
        {
            _foglaltHelyService = foglaltHelyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _foglaltHelyService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _foglaltHelyService.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FoglaltHelyRequest dto)
        {
            var (success, error, result) = await _foglaltHelyService.CreateAsync(dto);
            if (!success) return error!.Contains("már foglalt") ? Conflict(error) : BadRequest(error);
            return CreatedAtAction(nameof(GetById), new { id = result!.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] FoglaltHelyRequest dto)
        {
            var (success, error) = await _foglaltHelyService.UpdateAsync(id, dto);
            if (!success && error == null) return NotFound();
            if (error != null) return error.Contains("már foglalt") ? Conflict(error) : BadRequest(error);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _foglaltHelyService.DeleteAsync(id);
            return success ? NoContent() : NotFound();
        }
    }
}

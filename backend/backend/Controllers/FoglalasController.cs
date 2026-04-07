using backend.DTOs;
using backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace backend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class FoglalasController : ControllerBase
    {
        private readonly IFoglalasService _foglalasService;

        public FoglalasController(IFoglalasService foglalasService)
        {
            _foglalasService = foglalasService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _foglalasService.GetByUserAsync(GetCurrentUserId()));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var (found, owned, result) = await _foglalasService.GetByIdAsync(id, GetCurrentUserId());
            if (!found) return NotFound();
            if (!owned) return Forbid();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FoglalasRequest dto)
        {
            var (success, error, result) = await _foglalasService.CreateAsync(dto);
            if (!success) return BadRequest(error);
            return CreatedAtAction(nameof(GetById), new { id = result!.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] FoglalasRequest dto)
        {
            var (found, owned, error) = await _foglalasService.UpdateAsync(id, dto, GetCurrentUserId());
            if (!found) return NotFound();
            if (!owned) return Forbid();
            if (error != null) return BadRequest(error);
            return NoContent();
        }


        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var (found, owned) = await _foglalasService.DeleteAsync(id, userId);

            if (!found) return NotFound(new { message = "Foglalás nem található." });

            return NoContent();
        }

        private int GetCurrentUserId()
        {
            var value = User.FindFirstValue(ClaimTypes.NameIdentifier)
                        ?? User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            return int.Parse(value!);
        }
    }
}

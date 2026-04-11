using backend.DTOs;
using backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public class FilmController : ControllerBase
{
    private readonly IFilmService _filmService;

    public FilmController(IFilmService filmService)
    {
        _filmService = filmService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<FilmResponse>>> GetAll()
    {
        var films = await _filmService.GetAllAsync();
        return Ok(films);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<FilmResponse>> GetById(int id)
    {
        var film = await _filmService.GetByIdAsync(id);
        if (film == null)
            return NotFound(new { message = "Film not found" });
        return Ok(film);
    }

    [HttpPost]
    [Consumes("multipart/form-data")]
    [RequestSizeLimit(104_857_600)]
    [RequestFormLimits(MultipartBodyLengthLimit = 104_857_600)]
    public async Task<IActionResult> Create([FromForm] FilmRequest dto)
    {
        var created = await _filmService.CreateAsync(dto);
        return Ok(created);
    }

    [HttpPut("{id}")]
    [Consumes("multipart/form-data")]
    [RequestSizeLimit(104_857_600)]
    [RequestFormLimits(MultipartBodyLengthLimit = 104_857_600)]
    public async Task<IActionResult> Update(int id, [FromForm] FilmRequest dto)
    {
        var success = await _filmService.UpdateAsync(id, dto);
        if (!success)
            return NotFound(new { message = "Film not found" });
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _filmService.DeleteAsync(id);
        if (!success)
            return NotFound(new { message = "Film not found" });
        return NoContent();
    }
}
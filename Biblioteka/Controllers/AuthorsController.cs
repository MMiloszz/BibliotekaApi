using Biblioteka.Dtos;
using Biblioteka.Mappings;
using Biblioteka.Services;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteka.Controllers;

[ApiController]
[Route("authors")]
public class AuthorsController : ControllerBase
{
    private readonly AuthorsService _svc;
    public AuthorsController(AuthorsService svc) => _svc = svc;

    [HttpGet]
    public async Task<ActionResult<List<AuthorResponseDto>>> GetAll()
        => Ok((await _svc.GetAllAsync()).Select(a => a.ToDto()).ToList());

    [HttpGet("{id:int}")]
    public async Task<ActionResult<AuthorResponseDto>> GetById(int id)
    {
        var a = await _svc.GetByIdAsync(id);
        return a is null ? NotFound() : Ok(a.ToDto());
    }

    [HttpPost]
    public async Task<ActionResult<AuthorResponseDto>> Create([FromBody] AuthorCreateDto dto)
    {
        if (!ModelState.IsValid) return BadRequest();

        var a = await _svc.CreateAsync(dto.FirstName, dto.LastName);
        return Created($"/authors/{a.Id}", a.ToDto()); // 201
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] AuthorUpdateDto dto)
    {
        if (!ModelState.IsValid) return BadRequest();
        if (dto.Id != 0 && dto.Id != id) return BadRequest();

        var ok = await _svc.UpdateAsync(id, dto.FirstName, dto.LastName);
        return ok ? NoContent() : NotFound(); // 204 / 404
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var ok = await _svc.DeleteAsync(id);
        return ok ? NoContent() : NotFound(); // 204 / 404
    }
}


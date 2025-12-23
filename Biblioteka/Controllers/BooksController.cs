using Biblioteka.Dtos;
using Biblioteka.Mappings;
using Biblioteka.Services;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteka.Controllers;

[ApiController]
[Route("books")]
public class BooksController : ControllerBase
{
    private readonly BooksService _svc;
    public BooksController(BooksService svc) => _svc = svc;

    [HttpGet]
    public async Task<ActionResult<List<BookResponseDto>>> GetAll([FromQuery] int? authorId)
        => Ok((await _svc.GetAllAsync(authorId)).Select(b => b.ToDto()).ToList());

    [HttpGet("{id:int}")]
    public async Task<ActionResult<BookResponseDto>> GetById(int id)
    {
        var b = await _svc.GetByIdAsync(id);
        return b is null ? NotFound() : Ok(b.ToDto());
    }

    [HttpPost]
    public async Task<ActionResult<BookResponseDto>> Create([FromBody] BookCreateDto dto)
    {
        if (!ModelState.IsValid) return BadRequest();

        var (ok, book) = await _svc.CreateAsync(dto.Title, dto.Year, dto.AuthorId);
        if (!ok) return BadRequest(); // authorId nie istnieje

        return Created($"/books/{book!.Id}", book.ToDto()); // 201
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] BookUpdateDto dto)
    {
        if (!ModelState.IsValid) return BadRequest();
        if (dto.Id != 0 && dto.Id != id) return BadRequest();

        var (found, authorOk) = await _svc.UpdateAsync(id, dto.Title, dto.Year, dto.AuthorId);
        if (!found) return NotFound();
        if (!authorOk) return BadRequest();

        return NoContent(); // 204
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var ok = await _svc.DeleteAsync(id);
        return ok ? NoContent() : NotFound();
    }
}

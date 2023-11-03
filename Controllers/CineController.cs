using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using movies_api.Data;
using movies_api.Data.Dtos;
using movies_api.Models;

namespace movies_api.Controllers;

[ApiController]
[Route("[controller]")]
public class CineController: ControllerBase
{
    private MovieContext _context;
    private IMapper _mapper;

    public CineController(MovieContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult AddCine([FromBody] CreateCineDto cineDto)
    {
        Cine cine = _mapper.Map<Cine>(cineDto);
        _context.Cines.Add(cine);
        _context.SaveChanges();
        return CreatedAtAction(nameof(RecoverCineById), 
            new { id = cine.Id},
            cine);
    }
    
    [HttpGet]
    public IEnumerable<ReadMovieDto> RecoverCines([FromQuery] int skip, [FromQuery] int take)
    {
        return _mapper.Map<List<ReadMovieDto>>(_context.Cines.Skip(skip).Take(take));
    }

    [HttpGet("{id}")]
    public IActionResult RecoverCineById(int id)
    {
        var cine = _context.Cines.FirstOrDefault(cine => cine.Id == id);
        if (cine == null) return NotFound();
        var cineDto = _mapper.Map<ReadCineDto>(cine);
        return Ok(cineDto);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateCine(int id, [FromBody] UpdateCineDto cineDto)
    {
        var cine = _context.Cines.FirstOrDefault(cine => cine.Id == id);
        if (cine == null) return NotFound();
        _mapper.Map(cineDto, cine);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteCine(int id)
    {
        var cine = _context.Cines.FirstOrDefault(cine => cine.Id == id);
        if (cine == null) return NotFound();
        _context.Remove(cine);
        _context.SaveChanges();
        return NoContent();
    }
}
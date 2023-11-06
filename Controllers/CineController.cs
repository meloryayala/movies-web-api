using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using movies_api.Data;
using movies_api.Data.Dtos;
using movies_api.Models;

namespace movies_api.Controllers;

[ApiController]
[Route("[controller]")]
public class CineController : ControllerBase
{
    private MovieContext _context;
    private IMapper _mapper;

    public CineController(MovieContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Add cine on the database
    /// </summary>
    /// <param name="movieDto">Object needed to add data</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Once the request is completed successfully</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult AddCine([FromBody] CreateCineDto cineDto)
    {
        Cine cine = _mapper.Map<Cine>(cineDto);
        _context.Cines.Add(cine);
        _context.SaveChanges();
        return CreatedAtAction(nameof(RecoverCineById),
            new { id = cine.Id },
            cineDto);
    }

    /// <summary>
    /// Recover the list of cines with pagination
    /// </summary>
    /// <returns>IEnumarable></returns>
    /// <response code="200">Once the request is completed successfully</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IEnumerable<ReadCineDto> RecoverCines([FromQuery] int skip = 0, [FromQuery] int take = 50)
    {
        return _mapper.Map<List<ReadMovieDto>>(_context.Cines.Skip(skip).Take(take));
    }

    /// <summary>
    /// Recover a cine by Id 
    /// </summary>
    /// <param name="movieDto">Object needed to recover object data</param>
    /// <returns>IActionResult</returns>
    /// <response code="200">Once the request is completed successfully</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult RecoverCineById(int id)
    {
        var cine = _context.Cines.FirstOrDefault(cine => cine.Id == id);
        if (cine != null)
        {
            ReadCineDto cineDto = _mapper.Map<ReadCineDto>(cine);
            return Ok(cineDto);
        }

        return NotFound();
    }
    
    /// <summary>
    /// Update all data fields from a cine
    /// </summary>
    /// <param name="movieDto">Object needed to update data fields</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Once the request is completed successfully</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult UpdateCine(int id, [FromBody] UpdateCineDto cineDto)
    {
        var cine = _context.Cines.FirstOrDefault(cine => cine.Id == id);
        if (cine == null) return NotFound();
        _mapper.Map(cineDto, cine);
        _context.SaveChanges();
        return NoContent();
    }

    /// <summary>
    /// Delete a cine from the database
    /// </summary>
    /// <param name="movieDto">Object needed to delete the data</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Once the request is completed successfully</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult DeleteCine(int id)
    {
        var cine = _context.Cines.FirstOrDefault(cine => cine.Id == id);
        if (cine == null) return NotFound();
        _context.Remove(cine);
        _context.SaveChanges();
        return NoContent();
    }
}
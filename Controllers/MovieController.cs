using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using movies_api.Data;
using movies_api.Data.Dtos;
using movies_api.Models;

namespace movies_api.Controllers;

[ApiController]
[Route("[controller]")]
public class MovieController : ControllerBase
{
    private MovieContext _context;
    private IMapper _mapper;

    public MovieController(MovieContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Add movie on the database
    /// </summary>
    /// <param name="movieDto">Object needed to add data</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Once the request is completed successfully</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult AddMovie([FromBody] CreateMovieDto movieDto)
    {
        Movie movie = _mapper.Map<Movie>(movieDto);
            _context.Movies.Add(movie);
            _context.SaveChanges();
            return CreatedAtAction(nameof(ReadMovieById),
                new { id = movie.Id }
                , movie);
    }

    /// <summary>
    /// Recover the list of movies with pagination
    /// </summary>
    /// <returns>IEnumarable></returns>
    /// <response code="200">Once the request is completed successfully</response>
    [HttpGet]
    public IEnumerable<ReadMovieDto> ReadMovies([FromQuery] int skip = 0, [FromQuery] int take = 50)
    {
        return _mapper.Map<List<ReadMovieDto>>(_context.Movies.Skip(skip).Take(take));
    }
    
    /// <summary>
    /// Recover a movie by Id 
    /// </summary>
    /// <param name="movieDto">Object needed to recover object data</param>
    /// <returns>IActionResult</returns>
    /// <response code="200">Once the request is completed successfully</response>
    [HttpGet("{id}")]
    public IActionResult ReadMovieById(int id)
    {
        var movie = _context.Movies.FirstOrDefault(movie => movie.Id == id);
        if (movie == null) return NotFound();
        var movieDto = _mapper.Map<ReadMovieDto>(movie);
        return Ok(movieDto);
    }
    
    /// <summary>
    /// Update all data fields from a movie
    /// </summary>
    /// <param name="movieDto">Object needed to update data fields</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Once the request is completed successfully</response>
    [HttpPut("{id}")]
    public IActionResult UpdateMovie(int id, [FromBody] UpdateMovieDto movieDto)
    {
        var movie = _context.Movies.FirstOrDefault(movie => movie.Id == id);
        if (movie == null) return NotFound();
        _mapper.Map(movieDto, movie);
        _context.SaveChanges();
        return NoContent();
    }
    
    /// <summary>
    /// Update data fields partially from movie
    /// </summary>
    /// <param name="movieDto">Object needed to update data fields</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Once the request is completed successfully</response>
    [HttpPatch("{id}")]
    public IActionResult UpdateMovieParcial(int id, JsonPatchDocument<UpdateMovieDto> patch)
    {
        var movie = _context.Movies.FirstOrDefault(movie => movie.Id == id);
        if (movie == null) return NotFound();

        var movieToUpdate = _mapper.Map<UpdateMovieDto>(movie);
        patch.ApplyTo(movieToUpdate, ModelState);

        if (!TryValidateModel(movieToUpdate))
        {
            return ValidationProblem(ModelState);
        }
        _mapper.Map(movieToUpdate, movie);
        _context.SaveChanges();
        return NoContent();
    }

    /// <summary>
    /// Delete a movie from the database
    /// </summary>
    /// <param name="movieDto">Object needed to delete the data</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Once the request is completed successfully</response>
    [HttpDelete("{id}")]
    public IActionResult DeleteMovie(int id)
    {
        var movie = _context.Movies.FirstOrDefault(movie => movie.Id == id);
        if (movie == null) return NotFound();
        _context.Remove(movie);
        _context.SaveChanges();
        return NoContent();
    }
}
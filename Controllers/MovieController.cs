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
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IEnumerable<ReadMovieDto> ReadMovies(
        [FromQuery] int skip = 0, 
        [FromQuery] int take = 50,
        [FromQuery] string? cineName = null)
    {
        if (cineName == null)
        {
            var movieList = _context.Movies
                .Skip(skip)
                .Take(take)
                .ToList();
            return _mapper.Map<List<ReadMovieDto>>(movieList);
        }

        var cineQuery = _context.Movies
            .Skip(skip)
            .Take(take)
            .Where(movie => movie.Sessions.Any(session => session.Cine.Name == cineName))
            .ToList();
        return _mapper.Map<List<ReadMovieDto>>(cineQuery);
    }
    
    /// <summary>
    /// Recover a movie by Id 
    /// </summary>
    /// <param name="movieDto">Object needed to recover object data</param>
    /// <returns>IActionResult</returns>
    /// <response code="200">Once the request is completed successfully</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
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
    [ProducesResponseType(StatusCodes.Status204NoContent)]
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
    [ProducesResponseType(StatusCodes.Status204NoContent)]
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
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult DeleteMovie(int id)
    {
        var movie = _context.Movies.FirstOrDefault(movie => movie.Id == id);
        if (movie == null) return NotFound();
        _context.Remove(movie);
        _context.SaveChanges();
        return NoContent();
    }
}
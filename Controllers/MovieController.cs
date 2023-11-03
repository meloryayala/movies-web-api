using Microsoft.AspNetCore.Mvc;
using movies_api.Data;
using movies_api.Models;

namespace movies_api.Controllers;

[ApiController]
[Route("[controller]")]
public class MovieController : ControllerBase
{
    private MovieContext _context;

    public MovieController(MovieContext context)
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult AddMovie([FromBody] Movie movie)
    {
        {
            _context.Movies.Add(movie);
            _context.SaveChanges();
            return CreatedAtAction(nameof(ReadMovieById),
                new { id = movie.Id }
                , movie);
        }
    }

    [HttpGet]
    public IEnumerable<Movie> ReadMovies([FromQuery] int skip = 0, [FromQuery] int take = 50)
    {
        return _context.Movies.Skip(skip).Take(take);
    }

    [HttpGet("{id}")]
    public IActionResult ReadMovieById(int id)
    {
        var movie = _context.Movies.FirstOrDefault(movie => movie.Id == id);
        if (movie == null) return NotFound();
        return Ok(movie);
    }
}
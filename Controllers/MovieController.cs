using Microsoft.AspNetCore.Mvc;
using movies_api.Models;

namespace movies_api.Controllers;

[ApiController]
[Route("[controller]")]
public class MovieController : ControllerBase
{
    private static List<Movie> movies = new List<Movie>();

    [HttpPost]
    public void AddMovie([FromBody] Movie movie)
    {
        {
            movies.Add(movie);
            Console.WriteLine(movie.Title);
            Console.WriteLine(movie.Duration);
        }
    }
}
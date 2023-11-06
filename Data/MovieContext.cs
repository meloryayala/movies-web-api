using Microsoft.EntityFrameworkCore;
using movies_api.Models;
namespace movies_api.Data;

public class MovieContext : DbContext
{
    public MovieContext(DbContextOptions<MovieContext> opts)
        : base(opts)
    {
        
    }
    
    public DbSet<Movie> Movies { get; set; } = null!;

    public DbSet<Cine> Cines { get; set; } = null!;

    public DbSet<Adress> Adresses { get; set; } = null!;

    public DbSet<Session> Sessions { get; set; } = null!;

}
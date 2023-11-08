using Microsoft.EntityFrameworkCore;
using movies_api.Models;
namespace movies_api.Data;

public class MovieContext : DbContext
{
    public MovieContext(DbContextOptions<MovieContext> opts)
        : base(opts)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Session>()
            .HasKey(session => new { session.MovieId, session.CineId });

        builder.Entity<Session>()
            .HasOne(session => session.Cine)
            .WithMany(cine => cine.Sessions)
            .HasForeignKey(session => session.CineId);
        
        builder.Entity<Session>()
            .HasOne(session => session.Movie)
            .WithMany(movie => movie.Sessions)
            .HasForeignKey(session => session.MovieId);

        builder.Entity<Adress>()
            .HasOne(adress => adress.Cine)
            .WithOne(cine => cine.Adress)
            .OnDelete(DeleteBehavior.Restrict);
    }
    
    public DbSet<Movie> Movies { get; set; } = null!;
    public DbSet<Cine> Cines { get; set; } = null!;
    public DbSet<Adress> Adresses { get; set; } = null!;
    public DbSet<Session> Sessions { get; set; } = null!;

}
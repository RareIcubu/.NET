using Microsoft.EntityFrameworkCore;

namespace Lab2_api;

internal class MovieContext : DbContext
{
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Rating> Ratings { get; set; }

    public MovieContext()
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite(@"Data Source=movies.db");
    }
}
using Microsoft.EntityFrameworkCore;
using Project1RevSQL.Models;

namespace Project1RevSQL.Data;

public class TcgDbContext : DbContext
{

    //Fields
    public DbSet<Player> Players { get; set; }
    public DbSet<Tournament> Tournaments { get; set; }

    // Methods

    public TcgDbContext(DbContextOptions options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
using Microsoft.EntityFrameworkCore;
using Project1Rev.Models;

namespace Project1Rev.Data;

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
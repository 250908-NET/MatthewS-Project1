using System.Net;
using System.Text;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Text.Json;
using Project1RevSQL.Models;
using Project1RevSQL.Data;

namespace Project1Rev.Test
{

    public class TestBase : IDisposable
    {
        protected TcgDbContext Context { get; private set; }

        public TestBase()
        {
            var options = new DbContextOptionsBuilder<TcgDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            Context = new TcgDbContext(options);
            Context.Database.EnsureCreated();
        }

        protected async Task SeedDataAsync()
        {
            var players = new List<Player>
            {
                new Player { Id = 1, UserName = "Terrance", WinLoss = 0.75, TotalRounds = 20, City = "Loudon" },
                new Player { Id = 2, UserName = "AgumonSuperFan", WinLoss = 0.60, TotalRounds = 15, City = "Lenoir City" },
                new Player { Id = 3, UserName = "JohnCounterspell", WinLoss = 0.90, TotalRounds = 30, City = "Tellico" }
            };

            var tournaments = new List<Tournament>
            {
                new Tournament { Id = 1, SizeLimit = 16, Storename = "Store1", Address = "123 Main St", City = "Loudon", RuleType = "Standard", RoundType = "Swiss", TcgName = "MTG" },
                new Tournament { Id = 2, SizeLimit = 32, Storename = "Store2", Address = "456 Elm St", City = "Lenoir City", RuleType = "Regulation", RoundType = "Elimination", TcgName = "Digimon" }
            };

            Context.Players.AddRange(players);
            Context.Tournaments.AddRange(tournaments);
            await Context.SaveChangesAsync();
        }


        public void Dispose()
        {
            Context.Dispose();
        }


    }
}

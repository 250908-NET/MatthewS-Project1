using FluentAssertions;
using Project1RevSQL.Models;
using Project1RevSQL.Repositories;
using Microsoft.EntityFrameworkCore;
using Project1RevSQL.Repositories.implementation;
using System.Diagnostics.CodeAnalysis;

namespace Project1Rev.Test
{
    public class PlayerTest : TestBase
    {
        private readonly PlayerRepo _repository;
        public PlayerTest()
        {
            _repository = new PlayerRepo(Context);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllPlayers()
        {
            await SeedDataAsync();
            var players = await _repository.GetAllAsync();

            players.Should().NotBeNull();
            players.Count.Should().Be(3);

            string[] UserNames = { "Terrance", "AgumonSuperFan", "JohnCounterspell" };
            double[] Winlosses = { 0.75, 0.60, 0.90 };
            string[] Cities = { "Loudon", "Lenoir City", "Tellico" };
            int[] TotalRounds = { 20, 15, 30 };

            for (int i = 0; i < players.Count; i++)
            {
                players[i].UserName.Should().Be(UserNames[i]);
                players[i].WinLoss.Should().Be(Winlosses[i]);
                players[i].City.Should().Be(Cities[i]);
                players[i].TotalRounds.Should().Be(TotalRounds[i]);
            }
        }
        [Fact]
        public async Task GetByIdAsync_ShouldReturnCorrectPlayer()
        {
            await SeedDataAsync();
            var player = await _repository.GetByIdAsync(2);

            player.Should().NotBeNull();
            player.UserName.Should().Be("AgumonSuperFan");
            player.WinLoss.Should().Be(0.60);
            player.City.Should().Be("Lenoir City");
            player.TotalRounds.Should().Be(15);
        }
        [Fact]
        public async Task GetByIdAsync_ShouldReturnPlayerNotFound()
        {
            await SeedDataAsync();
            var player = await _repository.GetByIdAsync(99);

            player.Should().BeNull();
        }
        [Fact]
        public async Task AddAsync_ShouldAddNewPlayer()
        {
            await SeedDataAsync();

            var newPlayer = new Player
            {
                UserName = "RocksEnjoyer",
                WinLoss = 0.50,
                TotalRounds = 10,
                City = "Knoxville",
            };

            await _repository.AddAsync(newPlayer);
            await _repository.SaveChangesAsync();

            var player = await _repository.GetByIdAsync(newPlayer.Id);

            player.Should().NotBeNull();
            player.UserName.Should().Be(newPlayer.UserName);
            player.WinLoss.Should().Be(newPlayer.WinLoss);
            player.City.Should().Be(newPlayer.City);
            player.TotalRounds.Should().Be(newPlayer.TotalRounds);
        }
    }
}
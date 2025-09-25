using FluentAssertions;
using Project1RevSQL.Models;
using Project1RevSQL.Repositories;
using Microsoft.EntityFrameworkCore;
using Project1RevSQL.Repositories.implementation;
using System.Diagnostics.CodeAnalysis;

namespace Project1Rev.Test
{
    public class TournamentTest : TestBase
    {
        private readonly TournamentRepo _repository;
        public TournamentTest()
        {
            _repository = new TournamentRepo(Context);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllPlayers()
        {
            await SeedDataAsync();
            var tournaments = await _repository.GetAllAsync();

            tournaments.Should().NotBeNull();
            tournaments.Count.Should().Be(2);

            int[] SizeLimits = { 16, 32 };
            string[] StoreNames = { "Store1", "Store2" };
            string[] Addresses = { "123 Main St", "456 Elm St" };
            string[] Cities = { "Loudon", "Lenoir City" };
            string[] RuleTypes = { "Standard", "Regulation" };
            string[] RoundTypes = { "Swiss", "Elimination" };
            string[] TcgNames = { "MTG", "Digimon" };

            for (int i = 0; i < tournaments.Count; i++)
            {
                tournaments[i].SizeLimit.Should().Be(SizeLimits[i]);
                tournaments[i].Storename.Should().Be(StoreNames[i]);
                tournaments[i].Address.Should().Be(Addresses[i]);
                tournaments[i].City.Should().Be(Cities[i]);
                tournaments[i].RuleType.Should().Be(RuleTypes[i]);
                tournaments[i].RoundType.Should().Be(RoundTypes[i]);
                tournaments[i].TcgName.Should().Be(TcgNames[i]);
            }
        }
        [Fact]
        public async Task GetByIdAsync_ShouldReturnCorrectPlayer()
        {
            await SeedDataAsync();
            var tournament = await _repository.GetByIdAsync(1);

            tournament.Should().NotBeNull();
            tournament.SizeLimit.Should().Be(16);
            tournament.Storename.Should().Be("Store1");
            tournament.Address.Should().Be("123 Main St");
            tournament.City.Should().Be("Loudon");
            tournament.RuleType.Should().Be("Standard");
            tournament.RoundType.Should().Be("Swiss");
            tournament.TcgName.Should().Be("MTG");

        }
        [Fact]
        public async Task GetByIdAsync_ShouldReturnTournamentNotFound()
        {
            await SeedDataAsync();
            var player = await _repository.GetByIdAsync(99);

            player.Should().BeNull();
        } 
        [Fact]
        public async Task AddAsync_ShouldAddNewTournament()
        {
            await SeedDataAsync();

            var newTournament = new Tournament
            {
                SizeLimit = 64,
                Storename = "Store3",
                Address = "789 Oak St",
                City = "Knoxville",
                RuleType = "Standard",
                RoundType = "Swiss",
                TcgName = "MTG"
            };

            await _repository.AddAsync(newTournament);
            await _repository.SaveChangesAsync();

            var tournament = await _repository.GetByIdAsync(newTournament.Id);

            tournament.Should().NotBeNull();
            tournament.SizeLimit.Should().Be(newTournament.SizeLimit);
            tournament.Storename.Should().Be(newTournament.Storename);
            tournament.Address.Should().Be(newTournament.Address);
            tournament.City.Should().Be(newTournament.City);
            tournament.RuleType.Should().Be(newTournament.RuleType);
            tournament.RoundType.Should().Be(newTournament.RoundType);
            tournament.TcgName.Should().Be(newTournament.TcgName);
        }
        

        public async Task<List<Player>> GetAllPlayersTestAsync(int tournamentId)
        {
            return (await _repository.GetAllPlayersAsync(tournamentId)).ToList();
        }
    }
}
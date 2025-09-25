using FluentAssertions;
using Moq;
using Project1RevSQL.Repositories;
using Microsoft.EntityFrameworkCore;
using Project1RevSQL.Repositories.implementation;
using System.Diagnostics.CodeAnalysis;
using Project1RevSQL.Services;
using Project1RevSQL.Models;
using Project1RevSQL.Repositories.interfaces;
using Project1RevSQL.Services.implementation;

namespace Project1Rev.Test.ServiceTest
{
    public class TournamentTestS
    {
        private readonly Mock<ITournamentRepo> _mockRepo;
        private readonly TournamentService _service;
        public TournamentTestS()
        {
            _mockRepo = new Mock<ITournamentRepo>();
            _service = new TournamentService(_mockRepo.Object);
        }

        [Fact]

        public async Task UpdateAsync_ShouldUpdateExistingPlayer1()
        {

            // Arrange
            const int tournamentId1 = 1;
            const int tournamentId2 = 2;
            var tournamentUpdate1 = new Tournament
            {
                Id = 1,
                SizeLimit = 20,
                Storename = "Store1Updated",
                Address = "123 Main St Updated",
                City = "Loudon",
                RuleType = "Standard",
                RoundType = "Swiss",
                TcgName = "MTG",
            };
            var tournamentUpdate2 = new Tournament
            {
                Id = 2,
                SizeLimit = 30,
                Storename = "Store2Updated",
                Address = "456 Main St Updated",
                City = "Loudon",
                RuleType = "Standard",
                RoundType = "Swiss",
                TcgName = "MTG",
            };
            //Act
            _mockRepo.Setup(repo => repo.UpdateAsync(tournamentUpdate1)).Returns(Task.CompletedTask);
            _mockRepo.Setup(repo => repo.UpdateAsync(tournamentUpdate2)).Returns(Task.CompletedTask);

            await _service.UpdateAsync(tournamentId1, tournamentUpdate1);
            await _service.UpdateAsync(tournamentId2, tournamentUpdate2);

            // Assert

            _mockRepo.Verify(repo => repo.UpdateAsync(tournamentUpdate1), Times.Once);
            _mockRepo.Verify(repo => repo.UpdateAsync(tournamentUpdate2), Times.Once);
        }
        [Fact]
        public async Task DeleteAsync_ShouldCallRepositoryDelete()
        {
            // Arrange
            const int tournamentId = 1;

            // Act
            _mockRepo.Setup(repo => repo.DeleteAsync(tournamentId)).Returns(Task.CompletedTask);

            await _service.DeleteAsync(tournamentId);

            // Assert
            _mockRepo.Verify(repo => repo.DeleteAsync(tournamentId), Times.Once);
        }
        [Fact]
        public async Task GetCountPlayersInTournament_ShouldReturnCorrectCount()
        {
            // Arrange
            const int tournamentId = 1;
            const int expectedCount = 5;

            // Act
            _mockRepo.Setup(repo => repo.GetCountPlayersInTournament(tournamentId)).ReturnsAsync(expectedCount);

            var count = await _service.GetCountPlayersInTournament(tournamentId);

            // Assert
            count.Should().Be(expectedCount);
            _mockRepo.Verify(repo => repo.GetCountPlayersInTournament(tournamentId), Times.Once);
        }
        [Fact]
        public async Task GetAllPlayers_ShouldReturnPlayersInTournament()
        {
            // Arrange
            const int tournamentId = 1;
            var expectedPlayers = new List<Player>
            {
                new Player { Id = 1, UserName = "Player1", WinLoss = 0.5, TotalRounds = 10, City = "City1" },
                new Player { Id = 2, UserName = "Player2", WinLoss = 0.6, TotalRounds = 15, City = "City2" }
            };

            // Act
            _mockRepo.Setup(repo => repo.GetAllPlayersAsync(tournamentId)).ReturnsAsync(expectedPlayers);

            var players = await _service.GetAllPlayers(tournamentId);

            // Assert
            players.Should().BeEquivalentTo(expectedPlayers);
            _mockRepo.Verify(repo => repo.GetAllPlayersAsync(tournamentId), Times.Once);
        }
    }
}
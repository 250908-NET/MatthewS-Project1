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
using Microsoft.Identity.Client;

namespace Project1Rev.Test
{
    public class PlayerTestS
    {
        private readonly Mock<IPlayerRepo> _mockRepo;
        private readonly PlayerService _service;

        public PlayerTestS()
        {
            _mockRepo = new Mock<IPlayerRepo>();
            _service = new PlayerService(_mockRepo.Object);
        }
        [Fact]

        public async Task UpdateAsync_ShouldUpdateExistingPlayer1()
        {

            // Arrange
            const int playerId1 = 1;
            const int playerId2 = 2;
            var playerUpdate1 = new Player
            {
                Id = 1,
                UserName = "TerranceTheGreat",
                WinLoss = 0.80,
                TotalRounds = 25,
                City = "Loudon",
            };
            var playerUpdate2 = new Player
            {
                Id = 2,
                UserName = "AgumonSuperFan",
                WinLoss = 0.65,
                TotalRounds = 18,
                City = "Knoxville",
            };
            //Act
            _mockRepo.Setup(repo => repo.UpdateAsync(playerUpdate1)).Returns(Task.CompletedTask);
            _mockRepo.Setup(repo => repo.UpdateAsync(playerUpdate2)).Returns(Task.CompletedTask);

            await _service.UpdateAsync(playerUpdate1, playerId1);
            await _service.UpdateAsync(playerUpdate2, playerId2);

            // Assert

            _mockRepo.Verify(repo => repo.UpdateAsync(playerUpdate1), Times.Once);
            _mockRepo.Verify(repo => repo.UpdateAsync(playerUpdate2), Times.Once);
        }
        [Fact]
        public async Task DeleteAsync_ShouldCallRepositoryDelete()
        {
            // Arrange
            const int playerId = 1;

            // Act
            _mockRepo.Setup(repo => repo.DeleteAsync(playerId)).Returns(Task.CompletedTask);

            await _service.DeleteAsync(playerId);

            // Assert
            _mockRepo.Verify(repo => repo.DeleteAsync(playerId), Times.Once);
        }

        [Fact]
        public async Task RegisterPlayerAsync_ShouldCallRepositoryAddPlayer()
        {
            // Arrange
            const int playerId = 1;
            const int tournamentId = 1;

            // Act
            _mockRepo.Setup(repo => repo.AddPlayerAsync(playerId, tournamentId)).Returns(Task.CompletedTask);

            await _service.RegisterPlayerAsync(playerId, tournamentId);

            // Assert
            _mockRepo.Verify(repo => repo.AddPlayerAsync(playerId, tournamentId), Times.Once);
        }
        [Fact]
        public async Task RegisterPlayerAsync_ShouldNotRegisterIfPlayerAlreadyRegistered()
        {
            //Arrange
            const int playerId = 1;
            const int tournamentId = 1;
            //Act
            _mockRepo.SetupSequence(repo => repo.AddPlayerAsync(playerId, tournamentId))
                .Returns(Task.CompletedTask)
                .Throws(new Exception("Player is already registered for this tournament"));
            _mockRepo.Setup(repo => repo.SaveChangesAsync()).Returns(Task.CompletedTask);

            await _service.RegisterPlayerAsync(playerId, tournamentId);
            await Assert.ThrowsAsync<Exception>(() => _service.RegisterPlayerAsync(playerId, tournamentId));
            //Assert
            _mockRepo.Verify(repo => repo.AddPlayerAsync(playerId, tournamentId), Times.Exactly(2));
            _mockRepo.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }
        [Fact]
        public async Task RemovePlayerAsync_ShouldCallRepositoryRemovePlayer()
        {
            // Arrange
            const int playerId = 1;
            const int tournamentId = 1;

            // Act
            _mockRepo.Setup(repo => repo.RemovePlayerAsync(playerId, tournamentId)).Returns(Task.CompletedTask);
            _mockRepo.Setup(repo => repo.SaveChangesAsync()).Returns(Task.CompletedTask);

            await _service.RemovePlayerAsync(playerId, tournamentId);

            // Assert
            _mockRepo.Verify(repo => repo.RemovePlayerAsync(playerId, tournamentId), Times.Once);
            _mockRepo.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }
        
        // Test code here
    }
}
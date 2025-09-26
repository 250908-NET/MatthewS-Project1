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
    public class APItest : IClassFixture<WebApplicationFactory<Program>>
    {

        private readonly HttpClient _client;

        public APItest(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }
        [Fact]
        public async Task GetAllPlayers_ReturnsOkResponse()
        {
            // Arrange
            var request = "/players";

            // Act
            var response = await _client.GetAsync(request);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task GetPlayerById_ReturnsOkResponse()
        {
            // Arrange
            var request = "/players/1";

            // Act
            var response = await _client.GetAsync(request);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task GetPlayerById_ReturnsNotFoundResponse()
        {
            // Arrange
            var request = "/players/99";

            // Act
            var response = await _client.GetAsync(request);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
        [Fact]
        public async Task updatePlayer_ReturnOk()
        {
            var request = "/players/1";
            var playerUpdate = new Player
            {
                Id = 1,
                UserName = "TerranceTheGreat",
                WinLoss = 0.80,
                TotalRounds = 25,
                City = "Loudon",
            };

            var response = await _client.PutAsJsonAsync(request, playerUpdate);

            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        public async Task updatePlayer_ReturnNotFound()
        {
            var request = "/players/99";
            var playerUpdate = new Player
            {
                Id = 99,
                UserName = "TerranceTheGreat",
                WinLoss = 0.80,
                TotalRounds = 25,
                City = "Loudon",
            };

            var response = await _client.PutAsJsonAsync(request, playerUpdate);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
        public async Task DeletePlayer_ReturnOk()
        {
            var request = "/players/1";

            var response = await _client.DeleteAsync(request);

            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        public async Task DeletePlayer_ReturnNotFound()
        {
            var request = "/players/99";

            var response = await _client.DeleteAsync(request);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

    }
}
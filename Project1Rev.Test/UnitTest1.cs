using System.Net;
using System.Text;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Text.Json;
using Project1RevSQL.Models;

namespace Project1Rev.Test;

public class APItest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    
    public APItest(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }
    [Fact]
    public async Task GetTaskSuccess()
    {
        
        var response = await _client.GetAsync("/players");
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}

/*
    Program.cs
    This is the main entry point for the application.
    It sets up the web application, configures services, and defines endpoints.
*/

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Project1RevSQL.Data;
using Project1RevSQL.Models;
using Project1RevSQL.Repositories.implementation;
using Project1RevSQL.Repositories.interfaces;
using Project1RevSQL.Services.implementation;
using Project1RevSQL.Services.interfaces;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
var ServerString = File.ReadAllText("../../ServerString.env");
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TcgDbContext>(options => options.UseSqlServer(ServerString));


builder.Services.AddScoped<IPlayerRepo, PlayerRepo>();
builder.Services.AddScoped<ITournamentRepo, TournamentRepo>();

builder.Services.AddScoped<IPlayerService, PlayerService>();
builder.Services.AddScoped<ITournamentService, TournamentService>();

Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger();

builder.Host.UseSerilog();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/", () =>
{
    return "Hello world";
});
// Simple Endpoints for Players
app.MapGet("/players", async (IPlayerService service) =>
{
    return Results.Ok(new { Status = "Success", Data = await service.GetAllAsync(), Message = "Players Retrieved" });
});
app.MapPost("/players", async (IPlayerService service,[FromBody] Player player) =>
{
    await service.CreateAsync(player);
    return Results.Ok(new { Status = "Success", Data = player, Message = "Player created" });
});
app.MapGet("/players/{id}", async (IPlayerService service, int id) =>
{
    var player = await service.GetByIdAsync(id);
    if (player == null)
    {
        return Results.NotFound(new { Status = "Error", Message = "Player not found" });
    }
    return Results.Ok(new { Status = "Success", Data = player, Message = "Player Retrieved" });
});
app.MapPut("/players/{id}", async (IPlayerService service, int id, [FromBody] PlayerUpdateDto? player) =>
{
    var existingPlayer = await service.GetByIdAsync(id);
    if (existingPlayer == null)
    {
        return Results.NotFound(new { Status = "Error", Message = "Player not found" });
    }
    existingPlayer.UserName = player.UserName ?? existingPlayer.UserName;
    existingPlayer.WinLoss = player.WinLoss ?? existingPlayer.WinLoss;
    existingPlayer.TotalRounds = player.TotalRounds ?? existingPlayer.TotalRounds;
    existingPlayer.City = player.City ?? existingPlayer.City;
    
    try
    {
        return Results.Ok(new { Status = "Success", Data = await service.UpdateAsync(existingPlayer, id), Message = "Username Updated" });
    }
    catch (Exception ex)
    {
        return Results.Ok(new { Status = "Error", Message = ex.Message });
    }
});
// Delete player
app.MapDelete("/players/{id}", async (IPlayerService service, int id) =>
{
    
    // Deletion logic would go here
    try
    {
        await service.DeleteAsync(id);
        return Results.Ok(new { Status = "Success", Message = "Player deleted" });
    }
    catch (Exception ex) // catch any errors that occure, this should never happen
    {
        return Results.Ok(new { Status = "Error", Message = ex.Message });
    }
});
// Simple Endpoints for Tournaments
app.MapGet("/tournaments", async (ITournamentService service) =>
{
    return Results.Ok(new { Status = "Success", Data = await service.GetAllAsync(), Message = "Tournaments Retrieved" });
});
app.MapGet("/tournaments/{id}", async (ITournamentService service, int id) =>
{
    var tournament = await service.GetByIdAsync(id);
    if (tournament == null)
    {
        return Results.NotFound(new { Status = "Error", Message = "Tournament not found" });
    }
    return Results.Ok(new { Status = "Success", Data = tournament, Message = "Tournament Retrieved" });
});
app.MapPost("/tournaments", async (ITournamentService service, [FromBody] Tournament tournament) =>
{
    await service.CreateAsync(tournament);
    return Results.Ok(new { Status = "Success", Data = tournament, Message = "Tournament created" });
});
app.MapPut("/tournaments/{id}", async (ITournamentService service, int id, [FromBody] TournamentUpdateDto? tournament) =>
{
    try
    {
        var existingTournament = await service.GetByIdAsync(id);
        if (existingTournament == null)
        {
            return Results.NotFound(new { Status = "Error", Message = "Tournament not found" });
        }
        existingTournament.SizeLimit = tournament.SizeLimit ?? existingTournament.SizeLimit;
        existingTournament.Storename = tournament.Storename ?? existingTournament.Storename;
        existingTournament.Address = tournament.Address ?? existingTournament.Address;
        existingTournament.City = tournament.City ?? existingTournament.City;
        existingTournament.RuleType = tournament.RuleType ?? existingTournament.RuleType;
        existingTournament.RoundType = tournament.RoundType ?? existingTournament.RoundType;
        existingTournament.TcgName = tournament.TcgName ?? existingTournament.TcgName;
        return Results.Ok(new { Status = "Success", Data = await service.UpdateAsync(id, existingTournament), Message = "Tournament Updated" });
    }
    catch (Exception ex)
    {
        return Results.Ok(new { Status = "Error", Message = ex.Message });
    }


});
// delete tournament
app.MapDelete("/tournaments/{id}", async (ITournamentService service, int id) =>
{
    var tournament = await service.GetByIdAsync(id);
    if (tournament == null)
    {
        return Results.NotFound(new { Status = "Error", Message = "Tournament not found" });
    }
    // Deletion logic would go here
    try
    {
        await service.DeleteAsync(id);
        return Results.Ok(new { Status = "Success", Message = "Tournament deleted" });
    }
    catch (Exception ex) // catch any errors that occure, this should never happen
    {
        return Results.Ok(new { Status = "Error", Message = ex.Message });
    }
});
// Endpoints using both player and tournament

/*
    Get all players in a tournament
    This should return a list of player usernames
*/
app.MapGet("/players/tournaments/{tournamentId}", async (ITournamentService service, int tournamentId) =>
{
var tournament = await service.GetByIdAsync(tournamentId);
if (tournament == null) // tournament does not exist
{
    return Results.NotFound(new { Status = "Error", Message = "Tournament not found" });
}
    try // try to get all players in the tournament
    {
        var players = await service.GetAllPlayers(tournamentId);
        string playerNames = string.Join(", ", players.Select(p => p.UserName));
        return Results.Ok(new { Status = "Success", Data = playerNames, Message = "Players Retrieved" });
    }
    catch (Exception ex) // catch error(this exist to catch if the tournament has no players)
    {
        return Results.Ok(new { Status = "Error", Message = ex.Message });
    }
    {
        
    }

});
/*
    Register a player for a tournament
    This should add an entry to the join table
*/
app.MapPost("/players/tournaments/{playerId}/{tournamentId}", async (IPlayerService playerService, ITournamentService tournamentService, int playerId, int tournamentId) =>
{
    var player = await playerService.GetByIdAsync(playerId);
    if (player == null) // player does not exist
    {
        return Results.NotFound(new { Status = "Error", Message = "Player not found" });
    }
    var tournament = await tournamentService.GetByIdAsync(tournamentId);
    if (tournament == null)
    {
        return Results.NotFound(new { Status = "Error", Message = "Tournament not found" });
    }
    try // try to register player for tournament
    {
        int numberOfPlayers = await tournamentService.GetCountPlayersInTournament(tournamentId);
        if (numberOfPlayers >= tournament.SizeLimit)
        {
            return Results.Ok(new { Status = "Error", Message = "Tournament is full" });
        }
        await playerService.RegisterPlayerAsync(playerId, tournamentId);
        return Results.Ok(new { Status = "Success", Message = "Player Registered for Tournament" });
    }
    catch (Exception ex) // catch exceptions(this will catch if the player is already registered)
    {
        return Results.Ok(new { Status = "Error", Message = ex.Message });
    }
});
/*
    Remove a player from a tournament
    This should remove an entry from the join table
*/
app.MapDelete("/players/tournaments/{playerId}/{tournamentId}", async (IPlayerService playerService, ITournamentService tournamentService, int playerId, int tournamentId) =>
{
    var player = await playerService.GetByIdAsync(playerId);
    if (player == null) // player does not exist
    {
        return Results.NotFound(new { Status = "Error", Message = "Player not found" });
    }
    var tournament = await tournamentService.GetByIdAsync(tournamentId);
    if (tournament == null) // tournament does not exist
    {
        return Results.NotFound(new { Status = "Error", Message = "Tournament not found" });
    }
    try // try to remove player from tournament
    {
        await playerService.RemovePlayerAsync(playerId, tournamentId);
        return Results.Ok(new { Status = "Success", Message = "Player Removed from Tournament" });
    } // catch any exceptions that occur while trying to remove player from tournament
    catch (Exception ex)
    {
        return Results.Ok(new { Status = "Error", Message = ex.Message });
    }

});

app.Run();

public record TournamentUpdateDto(
    int? SizeLimit,
    string? Storename,
    string? Address,
    string? City,
    string? RuleType,
    string? RoundType,
    string? TcgName
);
public record PlayerUpdateDto(
    string? UserName,
    double? WinLoss,
    int? TotalRounds,
    string? City
);
public partial class Program { };
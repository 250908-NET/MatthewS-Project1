/*
    FileName: PlayerRepo.cs
    Description: This class implements the IPlayerRepo interface, providing methods for managing player data and their registrations for tournaments in the database.
    Parents: IPlayerRepo.cs
*/
using Microsoft.EntityFrameworkCore;
using Project1RevSQL.Models;
using Project1RevSQL.Data;
using Project1RevSQL.Repositories.interfaces;
using Serilog;


namespace Project1RevSQL.Repositories.implementation
{

    public class PlayerRepo : IPlayerRepo
    {
        private readonly TcgDbContext _context; // database context
        public PlayerRepo(TcgDbContext context) // constructor to initialize the context
        {
            _context = context;
        }
        // method to get all players
        public async Task<List<Player>> GetAllAsync()
        {
            return await _context.Players.ToListAsync();
        }
        // method to get a player by id
        public async Task<Player> GetByIdAsync(int id)
        {
            return await _context.Players.FindAsync(id);
        }
        // method to add a new player
        public async Task AddAsync(Player player)
        {
            await _context.Players.AddAsync(player);
        }
        // method to update an existing player
        public async Task UpdateAsync(Player player)
        {
            _context.Players.Update(player);
        }
        // method to save changes to the database
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        // method to register a player for a tournament
        public async Task AddPlayerAsync(int playerId, int tournamentId)
        {
            var player = await _context.Players
            .Include(p => p.Tournaments)
            .FirstOrDefaultAsync(p => p.Id == playerId);
            var tournament = await _context.Tournaments.FindAsync(tournamentId);
            if (player.Tournaments.Any(t => t.Id == tournamentId))
            {
                throw new Exception("Player already registered for this tournament");
            }
            player.Tournaments.Add(tournament);
        }
        // method to remove a player from a tournament
        public async Task RemovePlayerAsync(int playerId, int tournamentId)
        {
            var player = await _context.Players
            .Include(p => p.Tournaments)
            .FirstOrDefaultAsync(p => p.Id == playerId);
            var tournament = await _context.Tournaments.FindAsync(tournamentId);
            if (!player.Tournaments.Any(t => t.Id == tournamentId))
            {
                throw new Exception("Player not registered for this tournament");
            }
            player.Tournaments.Remove(tournament);
        }
        // method to delete a player
        public async Task DeleteAsync(int id)
        {
            var player = await _context.Players.FindAsync(id);
            if (player == null)
            {
                throw new Exception("Player not found");
            }
            _context.Players.Remove(player);
            await _context.SaveChangesAsync();
        }

    }
    
}
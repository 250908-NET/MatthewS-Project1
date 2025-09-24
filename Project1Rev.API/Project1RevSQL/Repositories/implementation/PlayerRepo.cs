using Microsoft.EntityFrameworkCore;
using Project1RevSQL.Models;
using Project1RevSQL.Data;
using Project1RevSQL.Repositories.interfaces;
using Serilog;


namespace Project1RevSQL.Repositories.implementation
{

    public class PlayerRepo : IPlayerRepo
    {
        private readonly TcgDbContext _context;
        public PlayerRepo(TcgDbContext context)
        {
            _context = context;
        }
        public async Task<List<Player>> GetAllAsync()
        {
            return await _context.Players.ToListAsync();
        }
        public async Task<Player> GetByIdAsync(int id)
        {
            return await _context.Players.FindAsync(id);
        }
        public async Task AddAsync(Player player)
        {
            await _context.Players.AddAsync(player);
        }
        public async Task UpdateAsync(Player player)
        {
            _context.Players.Update(player);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
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
            await _context.SaveChangesAsync();
        }
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
            await _context.SaveChangesAsync();
        }

    }
    
}
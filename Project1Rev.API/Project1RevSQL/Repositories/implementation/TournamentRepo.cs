using Microsoft.EntityFrameworkCore;
using Project1RevSQL.Models;
using Project1RevSQL.Data;
using Project1RevSQL.Repositories.interfaces;

namespace Project1RevSQL.Repositories.implementation
{
    public class TournamentRepo : ITournamentRepo
    {
        private readonly TcgDbContext _context;

        public TournamentRepo(TcgDbContext context)
        {
            _context = context;
        }

        public async Task<List<Tournament>> GetAllAsync()
        {
            return await _context.Tournaments.ToListAsync();
        }

        public async Task<Tournament> GetByIdAsync(int id)
        {
            return await _context.Tournaments.FindAsync(id);
        }

        public async Task AddAsync(Tournament tournament)
        {
            await _context.Tournaments.AddAsync(tournament);
        }
        public async Task UpdateAsync(Tournament tournament)
        {
            _context.Tournaments.Update(tournament);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<int> GetCountPlayersInTournament(int tournamentId)
        {
            var tournament = await _context.Tournaments
            .Include(t => t.Players)
            .FirstOrDefaultAsync(t => t.Id == tournamentId);
            if (tournament == null)
            {
                throw new Exception("Tournament not found");
            }
            return tournament.Players.Count;
        }
        public async Task<ICollection<Player>> GetAllPlayersAsync(int tournamentId)
        {
            string sqlString = "SELECT p.* FROM Players AS p JOIN PlayerTournament AS pt ON p.id = pt.PlayersId WHERE pt.TournamentsId = {0}";
            return await _context.Players.FromSqlRaw(sqlString, tournamentId).ToListAsync();
        }
    }
}
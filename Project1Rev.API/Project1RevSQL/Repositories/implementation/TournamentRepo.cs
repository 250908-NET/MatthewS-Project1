/*
    FileName: TournamentRepo.cs
    Description: This class implements the ITournamentRepo interface, providing methods for managing tournament data and their associated players in the database.
    Parents: ITournamentRepo.cs
*/
using Microsoft.EntityFrameworkCore;
using Project1RevSQL.Models;
using Project1RevSQL.Data;
using Project1RevSQL.Repositories.interfaces;

namespace Project1RevSQL.Repositories.implementation
{
    public class TournamentRepo : ITournamentRepo 
    {
        private readonly TcgDbContext _context; // database context

        // constructor to initialize the context
        public TournamentRepo(TcgDbContext context)
        {
            _context = context;
        }
        // method to get all tournaments
        public async Task<List<Tournament>> GetAllAsync()
        {
            return await _context.Tournaments.ToListAsync();
        }

        // method to get a tournament by id 
        public async Task<Tournament> GetByIdAsync(int id)
        {
            return await _context.Tournaments.FindAsync(id);
        }
        // method to add a new tournament
        public async Task AddAsync(Tournament tournament)
        {
            await _context.Tournaments.AddAsync(tournament);
        }
        // method to update an existing tournament
        public async Task UpdateAsync(Tournament tournament)
        {
            _context.Tournaments.Update(tournament);
        }
        // method to save changes to the database
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        // method to get the count of players in a tournament
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
        // method to get all players in a tournament
        public async Task<ICollection<Player>> GetAllPlayersAsync(int tournamentId)
        {
            string sqlString = "SELECT p.* FROM Players AS p RIGHT OUTER JOIN PlayerTournament AS pt ON p.PlayerId = pt.PlayersId WHERE pt.TournamentsId = {0}";
            return await _context.Players.FromSqlRaw(sqlString, tournamentId).ToListAsync();
        }
        public async Task DeleteAsync(int id)
        {
          var tournament = await _context.Tournaments.FindAsync(id);
            if (tournament == null)
            {
                throw new Exception("Tournament not found");
            }
            _context.Tournaments.Remove(tournament);
            await _context.SaveChangesAsync();
        }
    }
}
using Project1RevSQL.Models;

namespace Project1RevSQL.Repositories.interfaces
{
    public interface ITournamentRepo
    {
        Task<List<Tournament>> GetAllAsync();
        Task<Tournament> GetByIdAsync(int id);
        Task AddAsync(Tournament tournament);
        Task UpdateAsync(Tournament tournament);
        Task SaveChangesAsync();
        Task<int> GetCountPlayersInTournament(int tournamentId);
        Task<ICollection<Player>> GetAllPlayersAsync(int tournamentId);
    }
}
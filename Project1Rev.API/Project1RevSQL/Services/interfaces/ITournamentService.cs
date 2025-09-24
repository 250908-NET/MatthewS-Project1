using Project1RevSQL.Models;

namespace Project1RevSQL.Services.interfaces
{
    public interface ITournamentService
    {
        Task<List<Tournament>> GetAllAsync();
        Task<Tournament> GetByIdAsync(int id);
        Task<Tournament> CreateAsync(Tournament tournament);
        Task<Tournament> UpdateAsync(int id, Tournament tournament);
        Task<int> GetCountPlayersInTournament(int tournamentId);
        Task<ICollection<Player>> GetAllPlayers(int tournamentId);
    }
}
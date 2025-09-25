/*
    FileName: ITournamentService.cs
    Description: This interface defines the contract for the TournamentService class, outlining the methods for managing tournaments and their associated players.
    Children: TournamentService.cs
*/
using Project1RevSQL.Models;

namespace Project1RevSQL.Services.interfaces
{
    public interface ITournamentService
    {
        Task<List<Tournament>> GetAllAsync(); // get all tournaments
        Task<Tournament> GetByIdAsync(int id); // get tournament by id
        Task<Tournament> CreateAsync(Tournament tournament); // create a new tournament
        Task<Tournament> UpdateAsync(int id, Tournament tournament); // update an existing tournament
        Task<int> GetCountPlayersInTournament(int tournamentId); // get the count of players in a tournament
        Task<ICollection<Player>> GetAllPlayers(int tournamentId); // get all players in a tournament
        Task DeleteAsync(int id); // delete a tournament
    }
}
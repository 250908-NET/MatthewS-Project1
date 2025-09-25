/*
    FileName: ITournamentRepo.cs
    Description: This interface defines the contract for tournament repository operations, including methods for retrieving, adding, updating, and deleting tournaments, as well as managing players within tournaments.
    Children: TournamentRepo.cs
*/
using Project1RevSQL.Models;

namespace Project1RevSQL.Repositories.interfaces
{
    public interface ITournamentRepo
    {
        Task<List<Tournament>> GetAllAsync(); // get all tournaments
        Task<Tournament> GetByIdAsync(int id); // get a tournament by id
        Task AddAsync(Tournament tournament); // add a new tournament
        Task UpdateAsync(Tournament tournament); // update an existing tournament
        Task SaveChangesAsync(); // save changes to the database
        Task<int> GetCountPlayersInTournament(int tournamentId); // get the count of players in a tournament
        Task<ICollection<Player>> GetAllPlayersAsync(int tournamentId); // get all players in a tournament
        Task DeleteAsync(int id); // delete a tournament
    }
}
/*
    FileName: IPlayerRepo.cs
    Description: This interface defines the contract for player repository operations, including methods for retrieving, adding, updating, and deleting players, as well as managing player registrations for tournaments.
    Children: PlayerRepo.cs
*/
using Project1RevSQL.Models;

namespace Project1RevSQL.Repositories.interfaces
{
    public interface IPlayerRepo
    {
        Task<List<Player>> GetAllAsync(); // get all players
        Task<Player> GetByIdAsync(int id); // get a player by id
        Task AddAsync(Player player); // add a new player
        Task UpdateAsync(Player player); // update an existing player
        Task SaveChangesAsync(); // save changes to the database
        Task AddPlayerAsync(int playerId, int tournamentId); // register a player for a tournament
        Task RemovePlayerAsync(int playerId, int tournamentId); // remove a player from a tournament
        Task DeleteAsync(int id); // delete a player
    }
}
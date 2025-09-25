/*
    FileName: IPlayerService.cs
    Description: This interface defines the contract for the PlayerService class, outlining the methods for managing players and their registration in tournaments.
    Children: PlayerService.cs
*/
using Project1RevSQL.Models;

namespace Project1RevSQL.Services.interfaces
{
    public interface IPlayerService
    {
        Task<List<Player>> GetAllAsync(); // get all players
        Task<Player> GetByIdAsync(int id); // get player by id
        Task<Player> CreateAsync(Player player); // create a new player
        Task<Player> UpdateAsync(Player player, int id); // update player's information
        Task RegisterPlayerAsync(int playerId, int tournamentId); // register player to a tournament
        Task RemovePlayerAsync(int playerId, int tournamentId); // remove player from a tournament
        Task DeleteAsync(int id); // delete a player
    }
}
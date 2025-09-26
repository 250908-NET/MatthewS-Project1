/*
    FileName: PlayerService.cs
    Description: This class implements the IPlayerService interface, providing methods to manage players and their registration in tournaments.
    Parents: IPlayerService.cs
*/
using Project1RevSQL.Models;
using Project1RevSQL.Services.interfaces;
using Project1RevSQL.Repositories.interfaces;


namespace Project1RevSQL.Services.implementation
{
    public class PlayerService : IPlayerService
    {
        // declaring player repository
        private readonly IPlayerRepo _repo;
        // constructor to initialize the repository
        public PlayerService(IPlayerRepo repo)
        {
            _repo = repo;
        }
        // method to get all players
        public async Task<List<Player>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }
        // method to get a player by id
        public async Task<Player> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }
        // method to create a new player
        public async Task<Player> CreateAsync(Player player)
        {
            await _repo.AddAsync(player);
            await _repo.SaveChangesAsync();
            return player;
        }
        // method to update a player's information
        public async Task<Player> UpdateAsync(Player player,int id)
        {
            
            await _repo.UpdateAsync(player);
            await _repo.SaveChangesAsync();
            return player;
        }
        // method to register a player for a tournament
        public async Task RegisterPlayerAsync(int playerId, int tournamentId)
        {

            try
            {
                await _repo.AddPlayerAsync(playerId, tournamentId);
                await _repo.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error registering player for tournament: " + ex.Message);
            }

        }
        // method to remove a player from a tournament
        public async Task RemovePlayerAsync(int playerId, int tournamentId)
        {
            try
            {
                await _repo.RemovePlayerAsync(playerId, tournamentId);
                await _repo.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error removing player from tournament: " + ex.Message);
            }

        }
        /*
            handles delete for the program.cs endpoint
            this finds the player by id, then deletes the player
        */
        public async Task DeleteAsync(int id)
        {
            await _repo.DeleteAsync(id);
            await _repo.SaveChangesAsync();
        }
    }
}
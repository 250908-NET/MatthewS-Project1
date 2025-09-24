using Project1RevSQL.Models;
using Project1RevSQL.Services.interfaces;
using Project1RevSQL.Repositories.interfaces;


namespace Project1RevSQL.Services.implementation
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepo _repo;
        public PlayerService(IPlayerRepo repo)
        {
            _repo = repo;
        }
        public async Task<List<Player>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }
        public async Task<Player> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }
        public async Task<Player> CreateAsync(Player player)
        {
            await _repo.AddAsync(player);
            await _repo.SaveChangesAsync();
            return player;
        }
        public async Task<Player> UpdateUsernameAsync(int id, string newUsername)
        {
            var existingPlayer = await _repo.GetByIdAsync(id);
            if (existingPlayer == null)
            {
                throw new Exception("Player not found");
            }
            existingPlayer.UserName = newUsername;
            await _repo.UpdateAsync(existingPlayer);
            await _repo.SaveChangesAsync();
            return existingPlayer;
        }
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
    }
}
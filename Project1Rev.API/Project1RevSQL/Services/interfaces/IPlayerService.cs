using Project1RevSQL.Models;

namespace Project1RevSQL.Services.interfaces
{
    public interface IPlayerService
    {
        Task<List<Player>> GetAllAsync();
        Task<Player> GetByIdAsync(int id);
        Task<Player> CreateAsync(Player player);
        Task<Player> UpdateUsernameAsync(int id, string newUsername);
        Task RegisterPlayerAsync(int playerId, int tournamentId);
        Task RemovePlayerAsync(int playerId, int tournamentId);
    }
}
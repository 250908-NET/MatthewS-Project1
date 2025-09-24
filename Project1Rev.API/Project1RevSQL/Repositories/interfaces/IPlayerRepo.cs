using Project1RevSQL.Models;

namespace Project1RevSQL.Repositories.interfaces
{
    public interface IPlayerRepo
    {
        Task<List<Player>> GetAllAsync();
        Task<Player> GetByIdAsync(int id);
        Task AddAsync(Player player);
        Task UpdateAsync(Player player);
        Task SaveChangesAsync();
        Task AddPlayerAsync(int playerId, int tournamentId);
        Task RemovePlayerAsync(int playerId, int tournamentId);
        
    }
}
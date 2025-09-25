using Project1RevSQL.Models;
using Project1RevSQL.Services.interfaces;
using Project1RevSQL.Repositories.interfaces;

namespace Project1RevSQL.Services.implementation
{
    public class TournamentService : ITournamentService
    {
        private readonly ITournamentRepo _repo;
        public TournamentService(ITournamentRepo repo)
        {
            _repo = repo;
        }
        public async Task<List<Tournament>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }
        public async Task<Tournament> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }
        public async Task<Tournament> CreateAsync(Tournament tournament)
        {
            await _repo.AddAsync(tournament);
            await _repo.SaveChangesAsync();
            return tournament;
        }
        public async Task<Tournament> UpdateAsync(int id, Tournament tournament)
        {
            await _repo.UpdateAsync(tournament);
            await _repo.SaveChangesAsync();
            return tournament;

        }
        public async Task<int> GetCountPlayersInTournament(int tournamentId)
        {
            try
            {
                return await _repo.GetCountPlayersInTournament(tournamentId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching player count", ex);
            }
        }
        public async Task<ICollection<Player>> GetAllPlayers(int tournamentId)
        {
            try
            {
                var Players = await _repo.GetAllPlayersAsync(tournamentId);
                return Players;
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching players", ex);
            }
        }
        /*
            handles delete for the program.cs endpoint
            this finds the tournament by id, then deletes the tournament
        */
        public async Task DeleteAsync(int id)
        {

            await _repo.DeleteAsync(id);
            await _repo.SaveChangesAsync();
        }
    }
}
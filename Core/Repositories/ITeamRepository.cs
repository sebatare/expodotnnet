public interface ITeamRepository
{
    // Task<IEnumerable<Equipo>> GetAllTeamsAsync();
    // Task<Equipo> GetTeamByIdAsync(string id);
    Task<Team> AddAsync(Team team);
    Task<int> SaveChangesAsync();
    // Task UpdateTeamAsync(string id, Equipo updatedTeam);
    // Task DeleteTeamAsync(string id);

}
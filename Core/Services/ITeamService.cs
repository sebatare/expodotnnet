public interface ITeamService
{
    // Task<IEnumerable<EquipoDto>> GetAllTeamsAsync();
    // Task<EquipoDto> GetTeamByIdAsync(string id);
    Task<RegisterTeamDto> RegisterTeamAsync(RegisterTeamDto newTeam);
    // Task UpdateTeamAsync(string id, UpdateTeamDto updatedTeam);
    // Task DeleteTeamAsync(string id);
}
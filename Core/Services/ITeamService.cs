public interface ITeamService
{
    // Task<IEnumerable<EquipoDto>> GetAllTeamsAsync();
    // Task<EquipoDto> GetTeamByIdAsync(string id);
    Task<TeamRegisterDto> RegisterTeamAsync(TeamRegisterDto newTeam);
    // Task UpdateTeamAsync(string id, UpdateTeamDto updatedTeam);
    // Task DeleteTeamAsync(string id);
}
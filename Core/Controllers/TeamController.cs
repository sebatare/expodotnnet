using Microsoft.AspNetCore.Mvc;

public class TeamController : ControllerBase
{
    private readonly ITeamService _teamService;

    public TeamController(ITeamService teamService)
    {
        _teamService = teamService;
    }

    [HttpPost("add-team")]
    public async Task<IActionResult> RegisterTeam([FromBody] TeamRegisterDto equipoDto)
    {
        var equipo = await _teamService.RegisterTeamAsync(equipoDto);

        return Ok(new
        {
            Message = "Equipo creado exitosamente.",
            Team = new TeamRegisterDto
            {
                Id = equipo.Id,
                Nombre = equipo.Nombre,
                FechaCreacion = equipo.FechaCreacion,
                CapitanId = equipo.CapitanId,
                ClubId = equipo.ClubId
            }
        });
    }
}
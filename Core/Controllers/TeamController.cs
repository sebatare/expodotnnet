using Microsoft.AspNetCore.Mvc;

public class TeamController : ControllerBase
{
    private readonly ITeamService _teamService;

    public TeamController(ITeamService teamService)
    {
        _teamService = teamService;
    }

    [HttpPost("add-team")]
    public async Task<IActionResult> RegisterTeam([FromBody] RegisterTeamDto equipo)
    {
        await _teamService.RegisterTeamAsync(equipo);
        return Ok(new { Message = "Equipo creado exitosamente." });
    }
} 
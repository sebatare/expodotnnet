using Microsoft.AspNetCore.Mvc;

public class SedeController : ControllerBase
{
    private readonly ISedeService _sedeService;

    public SedeController(ISedeService sedeService)
    {
        _sedeService = sedeService;
    }

    [HttpGet("get-all-sedes")]
    public async Task<ActionResult<IEnumerable<SedeDto>>> GetSedes()
    {
        var sedes = await _sedeService.GetAllSedesAsync();
        return Ok(sedes); 
    }


    [HttpGet("get-sedes-cercanas")]
    public async Task<ActionResult<IEnumerable<SedesCercanasDto>>> GetSedesCercanas()
    {
        var sedes = await _sedeService.GetAllSedesAsync();
        return Ok(sedes); 
    }

    [HttpGet("get-sede/{id}")]
    public async Task<ActionResult<SedeDto>> GetSedeById(int id)
    {
        var sede = await _sedeService.GetSedeByIdAsync(id);
        if (sede == null) return NotFound();
        return Ok(sede);
    }

    [HttpPost("add-sede")]
    public async Task<IActionResult> AddSede([FromBody] CreateSedeDto newSede)
    {
        await _sedeService.AddSedeAsync(newSede);
        return Ok(new { Message = "Sede creada exitosamente." });

    }

    [HttpPut("update-sede/{id}")]
    public async Task<IActionResult> UpdateSede(int id, [FromBody] UpdateSedeDto updatedSede)
    {
        var result = await _sedeService.UpdateSedeAsync(id, updatedSede);
        if (result == null) return NotFound();
        return Ok(new { Message = "Sede actualizada exitosamente." });
    }
}
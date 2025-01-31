using Microsoft.AspNetCore.Mvc;

public class CanchaController : ControllerBase
{
    private readonly ICanchaService _canchaService;

    public CanchaController(ICanchaService canchaService)
    {
        _canchaService = canchaService;
    }

    [HttpPost("add-cancha")]
    public async Task<IActionResult> AddCancha([FromBody] CreateCanchaDto cancha)
    {

        await _canchaService.AddCanchaAsync(cancha);
        return Ok(new { Message = "Cancha creada exitosamente." });

    }

    [HttpGet("get-all-canchas")]
    public async Task<ActionResult<IEnumerable<Cancha>>> GetCanchas()
    {
        var canchas = await _canchaService.GetAllCanchasAsync();
        return Ok(canchas);

    }


    [HttpPut("update-cancha/{id}")]
    public async Task<IActionResult> UpdateCancha(int id, [FromBody] UpdateCanchaDto cancha)
    {
        try
        {
            await _canchaService.UpdateCanchaAsync(id, cancha);
            return Ok(new { Message = "Cancha actualizada exitosamente." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "Ha ocurrido un error interno del servidor. Por favor, inténtelo más tarde." });
        }
    }



    [HttpDelete("delete-cancha/{id}")]
    public async Task<IActionResult> DeleteCancha(int id)
    {
        try
        {
            await _canchaService.DeleteCanchaAsync(id);
            return Ok(new { Message = "Cancha eliminada exitosamente." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "Ha ocurrido un error interno del servidor. Por favor, inténtelo más tarde." });
        }
    }

    [HttpGet("get-cancha/{id}")]
    public async Task<ActionResult<Cancha>> GetCanchaById(int id)
    {
        var cancha = await _canchaService.GetCanchaByIdAsync(id);
        if (cancha == null) return NotFound();
        return Ok(cancha);
    }

    [HttpGet("get-canchas-by-sede/{id}")]
    public async Task<ActionResult<IEnumerable<CanchaDto>>> GetCanchasBySede(int id)
    {
        var canchas = await _canchaService.GetCanchasBySedeAsync(id);
        return Ok(canchas);
    }
}
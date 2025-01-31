using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

public class ReservaController : ControllerBase
{
    private readonly IReservaService _reservaService;

    public ReservaController(IReservaService reservaService)
    {
        _reservaService = reservaService;
    }



    [Authorize(AuthenticationSchemes = "Bearer")]
    [HttpPost("create-reserva")]
    public async Task<ActionResult<CreateReservaDto>> CreateReserva([FromBody] CreateReservaDto newReserva)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var reserva = await _reservaService.CreateReservaAsync(newReserva, userId);
        return Ok(reserva);
    }

}
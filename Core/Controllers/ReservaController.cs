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



    // [Authorize(AuthenticationSchemes = "Bearer")]
    [HttpPost("create-reserva")]
    public async Task<ActionResult<CreateReservaDto>> CreateReserva([FromBody] CreateReservaDto newReserva)
    {
        try
        {
            Console.WriteLine("Iniciando el proceso de controlador de reserva...");
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Console.WriteLine("Reserva CanchaId:");
            Console.WriteLine(userId);

            var reserva = await _reservaService.CreateReservaAsync(newReserva, newReserva.UsuarioId);
            return Ok(reserva);
        }
        catch (ReservaNoDisponibleException)
        {
            return BadRequest(new { message = "La cancha no está disponible para ese horario." });
        }
        catch (Exception ex)
        {
            Console.WriteLine(" Error al crear la reserva:");
            Console.WriteLine(ex.Message);
            return StatusCode(500, new { message = "Ocurrió un error al crear la reserva.", details = ex.Message });
        }
    }


}
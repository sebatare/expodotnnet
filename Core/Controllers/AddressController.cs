using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

public class AddressController : ControllerBase
{
    private readonly IAddressService _addressService;
    private readonly IAddressRepository _addressRepository;

    public AddressController(IAddressService addressService, IAddressRepository addressRepository)
    {
        _addressRepository = addressRepository;
        _addressService = addressService;
    }

    [HttpGet("GetAddressByEmail/{email}")]
    public async Task<IActionResult> GetAddressesByEmailAsync(string email)
    {
        var addresses = await _addressService.GetAddressesByEmailAsync(email); // Usa await para esperar la tarea

        if (addresses == null || !addresses.Any()) // Comprueba si no hay direcciones
        {
            return NotFound(); // Retorna un 404 si no se encuentran direcciones
        }

        return Ok(addresses); // Devuelve las direcciones en formato JSON
    }

    [HttpGet("get-all-addresses")]
    public async Task<IActionResult> GetAllAddresses()
    {
        var addresses = await _addressService.GetAllAddressesAsync();
        return Ok(addresses);
    }

    [HttpPost("add-address")]
    [Authorize(AuthenticationSchemes = "Bearer")] // Asegura que solo los usuarios autenticados pueden acceder a este endpoint
    public async Task<IActionResult> AddAddress([FromBody] CreateAddressDto dto)
    {
        try
        {
            // Obtener el UserId desde el token JWT
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new { Message = "Usuario no autenticado." });
            }

            // Llamar al servicio para crear la dirección y asociarla con el usuario
            await _addressService.AddAddressAsync(dto, userId);

            return Ok(new { Message = "Dirección creada exitosamente." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "Ha ocurrido un error interno del servidor. Por favor, inténtelo más tarde." });
        }
    }


    [HttpPut("update-address/{id}")]
    public async Task<IActionResult> UpdateAddress(int id, [FromBody] UpdateAddressDto dto)
    {

        try
        {
            // Llamamos al servicio para actualizar la dirección
            await _addressService.UpdateAddressAsync(id, dto);
            return Ok(new { Message = "Address updated successfully" });
        }
        catch (Exception)
        {
            return StatusCode(500, new { Message = "Ha ocurrido un error al actualizar dirección. Por favor, inténtelo más tarde." });
        }


    }

    [HttpDelete("delete-address/{id}")]
    public async Task<IActionResult> DeleteAddress(int id)
    {
        try
        {
            // Llamamos al servicio para eliminar la dirección
            await _addressService.DeleteAddressAsync(id);
            return Ok(new { Message = "Address deleted successfully" });
        }
        catch (Exception)
        {
            return StatusCode(500, new { Message = "Ha ocurrido un error al eliminar dirección. Por favor, inténtelo más tarde." });
        }
    }


    [HttpGet("get-address/{id}")]
    public async Task<IActionResult> GetAddressById(int id)
    {
        var address = await _addressRepository.GetAddressByIdAsync(id);
        if (address == null) return NotFound();
        return Ok(address);
    }
}


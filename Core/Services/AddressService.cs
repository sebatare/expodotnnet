using proyectodotnet.Common;
using proyectodotnet.Core.Models;
namespace proyectodotnet.Core.Services;
public class AddressService : IAddressService
{
    private readonly IAddressRepository _addressRepository;

    public AddressService(IAddressRepository addressRepository)
    {
        _addressRepository = addressRepository;
    }

    public async Task AddAddressAsync(CreateAddressDto dto, string userId)
    {
        var address = new Address
        {
            Calle = dto.Calle,
            Numero = dto.Numero,
            Otro = dto.Otro,    //NULL OPTIONAL
            Comuna = dto.Comuna,
            Ciudad = dto.Ciudad,
            Pais = dto.Pais,
            UserId = userId, //NULL OPTIONAL
        };

        await _addressRepository.AddAddressAsync(address);

    }


    public async Task<IEnumerable<AddressDto>> GetAddressesByEmailAsync(string email)
    {
        var addresses = await _addressRepository.GetAddressesByEmailAsync(email);

        // Aquí, aseguramos que solo se devuelven los datos de AddressDto, sin tareas asincrónicas.
        var addressDtos = addresses.Select(a => new AddressDto
        {
            Calle = a.Calle,
            Numero = a.Numero,
            Otro = a.Otro,
            Comuna = a.Comuna,
            Ciudad = a.Ciudad,
            Pais = a.Pais
        }).ToList();

        return addressDtos;
    }



    public async Task<IEnumerable<AddressDto>> GetAllAddressesAsync()
    {
        // Recupera las entidades desde el repositorio
        var addresses = await _addressRepository.GetAllAddressesAsync();

        // Mapea las entidades a DTOs
        return addresses.Select(a => new AddressDto
        {
            Calle = a.Calle,
            Numero = a.Numero,
            Comuna = a.Comuna,
            Ciudad = a.Ciudad,
            Pais = a.Pais
        });
    }




    public async Task UpdateAddressAsync(int id, UpdateAddressDto dto)
    {
        // Buscamos la dirección por el id
        var address = await _addressRepository.GetAddressByIdAsync(id) ?? throw new KeyNotFoundException("Address not found");

        // Actualizamos los campos de la dirección
        address.Calle = dto.Calle;
        address.Numero = dto.Numero;
        address.Otro = dto.Otro;
        address.Comuna = dto.Comuna;
        address.Ciudad = dto.Ciudad;
        address.Pais = dto.Pais;


        // Guardamos los cambios en la base de datos
        await _addressRepository.UpdateAddressAsync(address);
    }

    public async Task DeleteAddressAsync(int id)
    {
        await _addressRepository.DeleteAddressAsync(id);
    }

    public async Task<Response<AddressDto>> GetAddressById(int id)
{
    var address = await _addressRepository.GetAddressByIdAsync(id);

    if (address == null)
    {
        return Response<AddressDto>.Fail("Address not found");
    }

    var dto = new AddressDto
    {
        Id = address.Id,
        Calle = address.Calle,
        Numero = address.Numero,
        Otro = address.Otro,
        Comuna = address.Comuna,
        Ciudad = address.Ciudad,
        Pais = address.Pais
    };

    return Response<AddressDto>.Ok(dto);
}

}

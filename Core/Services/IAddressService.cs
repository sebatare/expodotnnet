using proyectodotnet.Common;

public interface IAddressService
{

    Task<IEnumerable<AddressDto>> GetAllAddressesAsync();
    Task AddAddressAsync(CreateAddressDto dto, string userId);
    Task UpdateAddressAsync(int id, UpdateAddressDto dto);
    Task DeleteAddressAsync(int id);

    Task<IEnumerable<AddressDto>> GetAddressesByEmailAsync(string email);

    Task<Response<AddressDto>> GetAddressById(int id);
}

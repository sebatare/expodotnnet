using proyectodotnet.Core.Models;

public interface IAddressRepository
{
    Task AddAddressAsync(Address newAddress);
    Task<Address?> GetAddressByIdAsync(int id);
    Task<IEnumerable<Address>> GetAddressesByEmailAsync(string email);
    Task<IEnumerable<Address>> GetAllAddressesAsync();
    Task UpdateAddressAsync(Address address);
    Task DeleteAddressAsync(int id);
    Task<AddressDto?> GetAddressByDetailsAsync(string calle, int numero, string comuna);
}
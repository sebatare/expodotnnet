public interface IAddressService
{
    Task<AddressDto> GetAddressByIdAsync(int id);
    Task<IEnumerable<AddressDto>> GetAllAddressesAsync();
    Task AddAddressAsync(CreateAddressDto dto, string userId);
    Task UpdateAddressAsync(int id, UpdateAddressDto dto);
    Task DeleteAddressAsync(int id);

    Task<IEnumerable<AddressDto>> GetAddressesByEmailAsync(string email);
}

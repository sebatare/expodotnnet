public interface IAddressRepository
{
    Task<IEnumerable<Address>> GetAllAddressesAsync();
    Task<Address> GetAddressByIdAsync(int id);
    Task AddAddressAsync(Address address);
    Task UpdateAddressAsync(Address address);
    Task DeleteAddressAsync(int id);
    Task<IEnumerable<Address>> GetAddressesByEmailAsync(string email);
}

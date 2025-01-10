using Microsoft.EntityFrameworkCore;

public class AddressRepository : IAddressRepository
{
    private readonly ApplicationDbContext _context;

    public AddressRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAddressAsync(Address newAddress)
    {
        try
        {
            await _context.Addresses.AddAsync(newAddress);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public async Task<Address> GetAddressByIdAsync(int id)
    {
        try
        {
            return await _context.Addresses.FirstOrDefaultAsync(a => a.Id == id);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<IEnumerable<Address>> GetAddressesByEmailAsync(string email)
    {

        return await _context.Addresses
            .Include(a => a.User)
            .Where(a => a.User.Email == email) 
            .ToListAsync();
    }

    public async Task<IEnumerable<Address>> GetAllAddressesAsync()
    {
        try
        {
            return await _context.Addresses.ToListAsync();
        }
        catch (Exception ex)
        {
            // Log the error
            Console.WriteLine(ex.Message);
            throw;
        }

    }


    public async Task UpdateAddressAsync(Address address)
    {
        _context.Addresses.Update(address);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAddressAsync(int id)
    {
        var address = await GetAddressByIdAsync(id);
        if (address != null)
        {
            _context.Addresses.Remove(address);
            await _context.SaveChangesAsync();
        }
    }
}

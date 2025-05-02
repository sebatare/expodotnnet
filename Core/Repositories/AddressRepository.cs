using Microsoft.EntityFrameworkCore;
using proyectodotnet.Core.Models;
using proyectodotnet.Data;

namespace proyectodotnet.Core.Repositories;



public class AddressRepository : IAddressRepository
{
    private readonly ApplicationDbContext _context;

    public AddressRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task AddAddressAsync(Address newAddress)
    {
        await _context.Addresses.AddAsync(newAddress);
        await _context.SaveChangesAsync();
    }

    public async Task<Address?> GetAddressByIdAsync(int id)
    {
        return await _context.Addresses
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<IEnumerable<Address>> GetAddressesByEmailAsync(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email no puede estar vacío", nameof(email));

        return await _context.Addresses
            .Include(a => a.User)
            .Where(a => a.User.Email == email)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<Address>> GetAllAddressesAsync()
    {
        return await _context.Addresses
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task UpdateAddressAsync(Address address)
    {
        _context.Addresses.Update(address);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAddressAsync(int id)
    {
        var address = await _context.Addresses.FindAsync(id);
        if (address != null)
        {
            _context.Addresses.Remove(address);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<AddressDto?> GetAddressByDetailsAsync(string calle, int numero, string comuna)
    {
        if (string.IsNullOrWhiteSpace(calle))
            throw new ArgumentException("La calle no puede estar vacía.", nameof(calle));

        if (string.IsNullOrWhiteSpace(comuna))
            throw new ArgumentException("La comuna no puede estar vacía.", nameof(comuna));

        return await _context.Addresses
            .Where(a => a.Calle == calle && a.Numero == numero && a.Comuna == comuna)
            .Select(a => new AddressDto
            {
                Calle = a.Calle,
                Numero = a.Numero,
                Comuna = a.Comuna,
                Ciudad = a.Ciudad,
                Pais = a.Pais
            })
            .AsNoTracking()
            .FirstOrDefaultAsync();
    }
}
using Microsoft.EntityFrameworkCore;
using proyectodotnet.Core.Models;
using proyectodotnet.Data;
namespace proyectodotnet.Core.Repositories;

public class SedeRepository : ISedeRepository
{
    private readonly ApplicationDbContext _context;

    public SedeRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Sede>> GetAllSedesAsync()
    {
        try
        {
            var sedes = await _context.Sede.ToListAsync();
            return sedes;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<Sede> GetSedeByIdAsync(int id)
    {
        try
        {
            return await _context.Sede.FirstOrDefaultAsync(s => s.Id == id);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<Sede> GetSedeByEmailAsync(string userEmail)
    {
        try
        {
            return await _context.Sede.FirstOrDefaultAsync(s => s.Email == userEmail);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<Sede> AddSedeAsync(Sede newSede)
    {
        try
        {
            await _context.Sede.AddAsync(newSede);
            await _context.SaveChangesAsync();
            return newSede;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<Sede> UpdateSedeAsync(Sede sede)
    {
        _context.Sede.Update(sede);
        await _context.SaveChangesAsync();
        return sede;
    }
    public async Task DeleteSedeAsync(Sede sede)
    {
        _context.Sede.Remove(sede);
        await _context.SaveChangesAsync();
    }

}
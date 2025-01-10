using Microsoft.EntityFrameworkCore;

public class CanchaRepository : ICanchaRepository
{
    private readonly ApplicationDbContext _context;
    public CanchaRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddCanchaAsync(Cancha cancha)
    {
        await _context.Cancha.AddAsync(cancha);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Cancha>> GetAllCanchasAsync()
    {
        var canchas = await _context.Cancha.ToListAsync();
        return canchas;
    }

    public async Task<Cancha> GetCanchaByIdAsync(int id)
    {
        try
        {
            return await _context.Cancha.FirstOrDefaultAsync(c => c.Id == id);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task UpdateCanchaAsync(Cancha cancha)
    {
        _context.Cancha.Update(cancha);
        await _context.SaveChangesAsync();
    }
    public async Task DeleteCanchaAsync(int id)
    {
        var cancha = await _context.Cancha.FindAsync(id);
        if (cancha != null)
        {
            _context.Cancha.Remove(cancha);
            await _context.SaveChangesAsync();
        }
    }


    //LISTA DE CANCHAS OBTENIDOS POR IDs
    public async Task<List<Cancha>> GetCanchasByIdsAsync(List<int> ids)
    {
        return await _context.Cancha
                             .Where(c => ids.Contains(c.Id))
                             .ToListAsync();
    }
}
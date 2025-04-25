using Microsoft.EntityFrameworkCore;

public class ReservaRepository : IReservaRepository
{
    private readonly ApplicationDbContext _context;

    public ReservaRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> CanchaDisponible(int CanchaId,DateTime Fecha, DateTime HoraInicio){
        return !await _context.Reservas.AnyAsync(x => x.CanchaId == CanchaId && x.Fecha == Fecha && x.HoraInicio == HoraInicio);
    }

    public async Task<Reserva> CreateReservaAsync(Reserva newReserva)
    {
        try
        {
            await _context.Reservas.AddAsync(newReserva);
            await _context.SaveChangesAsync();
            return newReserva;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<Reserva> ObtenerReservaConflictiva(int canchaId, DateTime fecha, DateTime horaInicio)
{
    return await _context.Reservas
        .Where(r => r.CanchaId == canchaId && r.Fecha == fecha && r.HoraInicio <= horaInicio && r.HoraTermino > horaInicio)
        .FirstOrDefaultAsync();
}
}
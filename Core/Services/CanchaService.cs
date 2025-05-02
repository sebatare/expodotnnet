using proyectodotnet.Core.Models;

public class CanchaService : ICanchaService
{
    private readonly ICanchaRepository _canchaRepository;

    public CanchaService(ICanchaRepository canchaRepository)
    {
        _canchaRepository = canchaRepository;
    }

    public async Task AddCanchaAsync(CreateCanchaDto dto)
    {
        var cancha = new Cancha
        {
            Nombre = dto.Nombre,
            Capacidad = dto.Capacidad,
            Largo = dto.Largo,
            Ancho = dto.Ancho,
            Observacion = dto.Observacion,
            ImageUrl = dto.ImageUrl,
            SedeId = dto.SedeId
        };
        await _canchaRepository.AddCanchaAsync(cancha);
    }

    public async Task<CanchaDto> GetCanchaByIdAsync(int id)
    {
        var cancha = await _canchaRepository.GetCanchaByIdAsync(id);
        if (cancha == null) return null;
        return new CanchaDto
        {
            Id = cancha.Id,
            Capacidad = cancha.Capacidad,
            Largo = cancha.Largo,
            Ancho = cancha.Ancho,
            Observacion = cancha.Observacion,
            ImageUrl = cancha.ImageUrl
        };
    }

    public async Task<IEnumerable<CanchaDto>> GetAllCanchasAsync()
    {
        var canchas = await _canchaRepository.GetAllCanchasAsync();
        return canchas.Select(c => new CanchaDto
        {
            Id = c.Id,
            Capacidad = c.Capacidad,
            Largo = c.Largo,
            Ancho = c.Ancho,
            Observacion = c.Observacion,
            ImageUrl = c.ImageUrl
        });
    }

    public async Task UpdateCanchaAsync(int id, UpdateCanchaDto dto)
    {
        var cancha = await _canchaRepository.GetCanchaByIdAsync(id);
        if (cancha == null) return;
        cancha.Nombre = dto.Nombre;
        // cancha.Capacidad = dto.Capacidad;
        // cancha.Largo = dto.Largo;
        // cancha.Ancho = dto.Ancho;
        // cancha.Observacion = dto.Observacion;
        // cancha.ImageUrl = dto.ImageUrl;
        // cancha.SedeId = dto.SedeId;
        await _canchaRepository.UpdateCanchaAsync(cancha);
    }

    public async Task DeleteCanchaAsync(int id)
    {
        var cancha = await _canchaRepository.GetCanchaByIdAsync(id);
        if (cancha == null) return;
        await _canchaRepository.DeleteCanchaAsync(cancha.Id);
    }

    public async Task<IEnumerable<CanchaDto>> GetCanchasBySedeAsync(int sedeId)
    {
        var canchas = await _canchaRepository.GetCanchasBySedeAsync(sedeId);
        return canchas.Select(c => new CanchaDto
        {
            Id = c.Id,
            Nombre = c.Nombre,
            Capacidad = c.Capacidad,
            Largo = c.Largo,
            Ancho = c.Ancho,
            Observacion = c.Observacion,
            ImageUrl = c.ImageUrl,
            SedeId = c.SedeId
        });
    }
}
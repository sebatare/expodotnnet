public class SedeService : ISedeService
{
    private readonly ISedeRepository _sedeRepository;
    private readonly ICanchaRepository _canchaRepository;
    public SedeService(ISedeRepository sedeRepository, ICanchaRepository canchaRepository)
    {
        _sedeRepository = sedeRepository;
        _canchaRepository = canchaRepository;
    }

    public async Task<IEnumerable<SedeDto>> GetAllSedesAsync()
    {
        var sedes = await _sedeRepository.GetAllSedesAsync();

        return sedes.Select(static s => new SedeDto
        {
            Id = s.Id,
            Nombre = s.Nombre,
            Descripcion = s.Descripcion,
            ImageUrl = s.ImageUrl,
            idsCanchas = s.Canchas.Select(c => c.Id).ToList(),

            // Aquí mapear Address directamente
            Address = s.Address != null ? new AddressToSedeDto
            {
                Calle = s.Address.Calle,
                Numero = s.Address.Numero,
                Comuna = s.Address.Comuna
            } : null // Si no hay dirección, se asigna null
        });
    }



    public async Task<SedeDto> GetSedeByIdAsync(int id)
    {
        var sede = await _sedeRepository.GetSedeByIdAsync(id);
        if (sede == null) return null;
        return new SedeDto
        {
            Id = sede.Id,
            Nombre = sede.Nombre,
            Descripcion = sede.Descripcion,
            idsCanchas = sede.Canchas.Select(c => c.Id).ToList()
        };
    }

    public async Task<SedeDto> GetSedeByEmailAsync(string userEmail)
    {
        var sede = await _sedeRepository.GetSedeByEmailAsync(userEmail);
        if (sede == null) return null;
        return new SedeDto
        {
            Id = sede.Id,
            Nombre = sede.Nombre,
            Descripcion = sede.Descripcion,
            idsCanchas = sede.Canchas.Select(c => c.Id).ToList()
        };
    }
    public async Task AddSedeAsync(CreateSedeDto dto)
    {
        // Crear una lista vacía para almacenar las canchas (por defecto está vacía)
        List<Cancha> canchas = new List<Cancha>();

        if (dto.IdCanchas != null && dto.IdCanchas.Any())
        {
            // Obtener las canchas existentes por sus IDs
            canchas = await _canchaRepository.GetCanchasByIdsAsync(dto.IdCanchas);

            // Verificar si no se encontraron todas las IDs proporcionadas
            var canchasEncontradasIds = canchas.Select(c => c.Id).ToList();
            var idsNoEncontrados = dto.IdCanchas.Except(canchasEncontradasIds).ToList();

            if (idsNoEncontrados.Any())
            {
                // Lanzar una excepción indicando las IDs que no existen
                throw new Exception($"Las siguientes IDs de canchas no existen: {string.Join(", ", idsNoEncontrados)}");
            }
        }

        // Crear la nueva entidad Sede
        var sede = new Sede
        {
            Nombre = dto.Nombre,
            Descripcion = dto.Descripcion,
            ImageUrl = dto.ImageUrl,
            Canchas = canchas // Asignar la lista de canchas (vacía si no se proporcionaron IDs)
        };

        try
        {
            await _sedeRepository.AddSedeAsync(sede);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.InnerException?.Message ?? ex.Message);
            throw;
        }
    }



    public async Task<Sede> UpdateSedeAsync(int id, UpdateSedeDto dto)
    {
        // Buscar la sede a actualizar
        var sede = await _sedeRepository.GetSedeByIdAsync(id)
                    ?? throw new Exception("No se encontró la sede a actualizar");

        // Actualizar los datos principales
        sede.Nombre = dto.Nombre;
        sede.Descripcion = dto.Descripcion;
        sede.ImageUrl = dto.ImageUrl;

        // Manejar las canchas
        if (dto.idsCancha != null && dto.idsCancha.Any())
        {
            // Obtener las canchas desde la base de datos
            var canchas = await _canchaRepository.GetCanchasByIdsAsync(dto.idsCancha);

            // Verificar que todas las IDs sean válidas
            if (canchas.Count != dto.idsCancha.Count)
            {
                throw new Exception("Algunas de las canchas proporcionadas no existen.");
            }

            // Asignar las canchas a la sede
            sede.Canchas = canchas;
        }
        else
        {
            // Si no se proporcionaron IDs, limpiar la lista de canchas
            sede.Canchas = new List<Cancha>();
        }

        // Actualizar la sede en el repositorio
        await _sedeRepository.UpdateSedeAsync(sede);

        // Retornar la sede actualizada
        return sede;
    }


    public async Task DeleteSedeAsync(int id)
    {
        var sede = await _sedeRepository.GetSedeByIdAsync(id);
        if (sede == null) throw new Exception("Sede not found");
        await _sedeRepository.DeleteSedeAsync(sede);
    }
}
using proyectodotnet.Core.Models;

public interface ISedeService
{
    Task<IEnumerable<SedeDto>> GetAllSedesAsync();
    Task<SedeDto> GetSedeByIdAsync(int id);
    Task AddSedeAsync(CreateSedeDto newSede);
    Task<Sede> UpdateSedeAsync(int id, UpdateSedeDto updatedSede);
    Task DeleteSedeAsync(int id);
}
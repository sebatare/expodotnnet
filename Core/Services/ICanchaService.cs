public interface ICanchaService
{
    Task AddCanchaAsync(CreateCanchaDto dto);
    Task<IEnumerable<CanchaDto>> GetAllCanchasAsync();
    
    Task<CanchaDto> GetCanchaByIdAsync(int id);
    Task UpdateCanchaAsync(int id, UpdateCanchaDto dto);
    Task DeleteCanchaAsync(int id);
}
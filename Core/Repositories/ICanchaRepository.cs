public interface ICanchaRepository
{
    Task<IEnumerable<Cancha>> GetAllCanchasAsync();

    Task<Cancha> GetCanchaByIdAsync(int id);
    Task AddCanchaAsync(Cancha cancha);
    Task UpdateCanchaAsync(Cancha cancha);
    Task DeleteCanchaAsync(int id);

    Task<List<Cancha>> GetCanchasByIdsAsync(List<int> ids);


}
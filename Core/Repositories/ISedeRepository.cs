public interface ISedeRepository{
    Task<IEnumerable<Sede>> GetAllSedesAsync();
    Task<Sede> GetSedeByIdAsync(int id);
    Task<Sede> AddSedeAsync(Sede newSede);
    Task<Sede> UpdateSedeAsync(Sede sede);
    Task DeleteSedeAsync(Sede sede);

}
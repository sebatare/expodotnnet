public interface IReservaService
{
    Task<CreateReservaDto> CreateReservaAsync(CreateReservaDto newReserva, string userId);
}
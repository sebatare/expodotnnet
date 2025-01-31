public interface IReservaRepository{
    Task<Reserva> CreateReservaAsync(Reserva newReserva);
    Task<bool> CanchaDisponible(int CanchaId, DateTime Fecha, DateTime HoraInicio);
}
public interface IReservaRepository{
    Task<Reserva> CreateReservaAsync(Reserva newReserva);
    Task<bool> CanchaDisponible(int CanchaId, DateTime Fecha, DateTime HoraInicio);
    Task<Reserva> ObtenerReservaConflictiva(int canchaId, DateTime fecha, DateTime horaInicio);
}
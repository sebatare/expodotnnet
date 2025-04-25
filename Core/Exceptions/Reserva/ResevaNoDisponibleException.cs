public class ReservaNoDisponibleException : Exception
{
    public int CanchaId { get; }
    public DateTime Fecha { get; }
    public DateTime HoraInicio { get; }
    public DateTime HoraTermino { get; }

    public ReservaNoDisponibleException(int canchaId, DateTime fecha, DateTime horaInicio, DateTime horaTermino)
        : base($"La cancha con ID {canchaId} ya está reservada el {fecha:yyyy-MM-dd} desde las {horaInicio:HH:mm} hasta las {horaTermino:HH:mm}.")
    {
        CanchaId = canchaId;
        Fecha = fecha;
        HoraInicio = horaInicio;
        HoraTermino = horaTermino;
    }
}

public class Reserva
{
    public int Id { get; set; }
    public DateTime FechaHoraInicio { get; set; }
    public DateTime FechaHoraFin { get; set; }

    // Relación con Usuario
    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; }

    // Relación con Cancha
    public int CanchaId { get; set; }
    public Cancha Cancha { get; set; }

    // Relación con Equipo
    public int? EquipoId { get; set; } // Opcional si la reserva no es para un equipo
    public Equipo Equipo { get; set; }

    public int? PuntuacionEquipo { get; set; }
}
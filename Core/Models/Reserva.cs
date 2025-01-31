public class Reserva
{
    public int Id { get; set; }
    public DateTime  FechaCreacion { get; set; }

    public DateTime Fecha { get; set; }
    public DateTime  HoraInicio { get; set; }
    public DateTime  HoraTermino { get; set; }

    // Relación con Usuario
    public string? UsuarioId { get; set; }
    public virtual User Usuario { get; set; }

    // Relación con Cancha
    public int? CanchaId { get; set; }
    public virtual Cancha Cancha { get; set; }

    // Relación con Equipo
    public int? EquipoId { get; set; } // Opcional si la reserva no es para un equipo
    public virtual Equipo Equipo { get; set; }

    public int? PuntuacionEquipo { get; set; }
}
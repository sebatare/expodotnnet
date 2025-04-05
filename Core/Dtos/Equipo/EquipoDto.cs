public class EquipoDto
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public DateTime FechaCreacion { get; set; }

    // Relación con Club
    public int? ClubId { get; set; } // Opcional si el equipo no pertenece a un club

    // Relación con Reservas
    public virtual ICollection<ReservaDto> Reservas { get; set; }

    // Relación con Usuarios a través de la entidad intermedia
    public virtual ICollection<UsuarioEquipo> UsuarioEquipos { get; set; }
}
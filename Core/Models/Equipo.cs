using proyectodotnet.Migrations;

public class Equipo
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public DateTime FechaCreacion { get; set; }

    // Relación con Club
    public int? ClubId { get; set; } // Opcional si el equipo no pertenece a un club
    public virtual Club? Club { get; set; }

    // Relación con Usuarios
    public virtual ICollection<User> Usuarios { get; set; }

    // Relación con Reservas
    public virtual ICollection<Reserva> Reservas { get; set; }

    // Relación con Usuarios a través de la entidad intermedia
    public virtual ICollection<UsuarioEquipo> UsuarioEquipos { get; set; }
}

namespace proyectodotnet.Core.Models;
public class Team
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public DateTime FechaCreacion { get; set; }

    public int? ClubId { get; set; }
    public virtual Club? Club { get; set; }

    public virtual ICollection<Reserva> Reservas { get; set; }

    public virtual ICollection<UserTeam> UserTeams { get; set; }

    public string CapitanId { get; set; }

    public virtual User Capitan { get; set; } // Relación con el capitán del equipo
}

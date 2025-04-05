public class RegisterTeamDto
{
    public string Id { get; set; }
    public int? ClubId { get; set; } // Opcional si el equipo no pertenece a un club
    public List<string> UserIds { get; set; } = new List<string>();

    public string CapitanId { get; set; }

    public virtual User Capitan { get; set; } // Usuario que es el capitán del equipo
}
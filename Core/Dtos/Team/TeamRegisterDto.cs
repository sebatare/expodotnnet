public class TeamRegisterDto
{
    public int Id { get; set; }

    public string Nombre { get; set; }
    public int? ClubId { get; set; }

    public List<TeamMemberDto> Miembros { get; set; }

    public string CapitanId { get; set; }

    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

}
public class RegisterTeamDto
{

    public string Nombre { get; set; }
    public int? ClubId { get; set; }

    public List<string> MiembrosIds { get; set; }

    public string CapitanId { get; set; }
   
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

}
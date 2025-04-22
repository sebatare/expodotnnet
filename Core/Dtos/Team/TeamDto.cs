public class TeamDto
{
    public string Id { get; set; }
    public string Nombre { get; set; }
    public DateTime FechaPartido { get; set; }
    public List<string> MiembrosIds { get; set; }
    public string CapitanId { get; set; }
}
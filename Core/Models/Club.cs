public class Club
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public DateTime FechaCreacion { get; set; }

    // Relación con Fundador
    public string CapitanId { get; set; }
    public virtual User Capitan { get; set; }

    // Relación con Equipo
    public virtual ICollection<Equipo>? Equipos { get; set; }
}

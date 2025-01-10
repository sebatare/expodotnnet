public class Cancha
{
    public int Id { get; set; }

    public int? Capacidad { get; set; }

    public float? Largo { get; set; }
    public float? Ancho { get; set; }

    public string? Observacion { get; set; }

    public string? ImageUrl { get; set; }

    public virtual Sede? Sede { get; set; }

    public int? SedeId { get; set; }
}
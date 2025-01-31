public class Sede
{
    public int Id { get; set; }

    public string Nombre { get; set; }

    public string? Telefono { get; set; }

    public string? Descripcion { get; set; }

    public string? Email { get; set; }

    public string? ImageUrl { get; set; }

    public virtual List<Cancha>? Canchas { get; set; }

    public virtual Address? Address { get; set; }

    public int? AddressId { get; set; }
}
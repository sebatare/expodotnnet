public class SedeDto
{
    public int Id { get; set; }

    public string Nombre { get; set; }

    public string? Descripcion { get; set; }

    public string? ImageUrl { get; set; }

    public virtual List<int>? idsCanchas { get; set; }
}
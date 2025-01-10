public class UpdateSedeDto
{
    public string Nombre { get; set; }
    public string? Descripcion { get; set; }
    public string? ImageUrl { get; set; }
    public List<int>? idsCancha { get; set; }
}
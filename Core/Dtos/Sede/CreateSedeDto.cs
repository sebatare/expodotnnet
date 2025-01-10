public class CreateSedeDto
{
    public string Nombre { get; set; }
    public string Email { get; set; }
    public string? Descripcion { get; set; }
    public string? ImageUrl { get; set; }


    public virtual List<int>? IdCanchas { get; set; }
}
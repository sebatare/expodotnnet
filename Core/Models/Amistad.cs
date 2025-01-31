public class Amistad
{
    public int Id { get; set; }
    public string UsuarioId1 { get; set; }
    public virtual User Usuario1 { get; set; }
    public string UsuarioId2 { get; set; }
    public virtual User Usuario2 { get; set; }
    public string? Estado { get; set; } // pendiente, aceptada, bloqueada
}

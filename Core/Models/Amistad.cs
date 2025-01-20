public class Amistad
{
    public int Id { get; set; }
    public int UsuarioId1 { get; set; }
    public User Usuario1 { get; set; }
    public int UsuarioId2 { get; set; }
    public User Usuario2 { get; set; }
    public string Estado { get; set; } // pendiente, aceptada, bloqueada
}

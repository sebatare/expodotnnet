public class UsuarioEquipo
{
    // Claves compuestas
    public string UserId { get; set; }
    public int EquipoId { get; set; }

    // Propiedad adicional para la confirmación
    public bool Confirmado { get; set; }

    // Navegación
    public virtual User User { get; set; }
    public virtual Equipo Equipo { get; set; }
}

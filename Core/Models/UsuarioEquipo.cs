public class UsuarioEquipo
{
    public int Id { get; set; }

    // Relación con Usuario
    public string UsuarioId { get; set; }
    public virtual User Usuario { get; set; }

    // Relación con Equipo
    public int EquipoId { get; set; }
    public virtual Equipo Equipo { get; set; }

    // Información adicional
    public string Posicion { get; set; } // delantero, portero, etc.
    public int Goles { get; set; }
    public double Puntuacion { get; set; }
}

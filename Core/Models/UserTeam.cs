public class UserTeam
{
    // Claves compuestas
    public string UserId { get; set; }

    // Propiedad adicional para la confirmación
    public bool Confirmado { get; set; }

    // Navegación   
    public virtual User User { get; set; }
    public int TeamId { get; set; } // Asegúrate de que esta propiedad esté aquí
    public virtual Team Team { get; set; }
}

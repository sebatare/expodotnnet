namespace proyectodotnet.Core.Models;
public class ChatMessage
{
    public int Id { get; set; }
    public string Message { get; set; }
    public string FromUserId { get; set; }
    public string ToUserId { get; set; }
    public DateTime Timestamp { get; set; }

    // Relaciones con la clase User
    public virtual User FromUser { get; set; } // Usuario que envía el mensaje
    public virtual User ToUser { get; set; }   // Usuario que recibe el mensaje
}

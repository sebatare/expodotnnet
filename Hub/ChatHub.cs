using Microsoft.AspNetCore.SignalR;

public class ChatHub : Hub
{

    public override Task OnConnectedAsync()
    {
        Console.WriteLine("00000000000000000000");
        var userId = Context.User?.Identity?.Name; // Obtiene el nombre del usuario autenticado
        if (userId == null)
        {
            Console.WriteLine("Usuario no autenticado");
        }
        else
        {
            Console.WriteLine($"Usuario autenticado: {userId}");
        }
        return base.OnConnectedAsync();
    }

}

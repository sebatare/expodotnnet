public class ReservaNoDisponibleException : Exception
{
    public ReservaNoDisponibleException() : base("La cancha ya está reservada en ese horario.") { }
}

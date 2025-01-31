public class ReservaDto{
    public int Id {get; set;}

    public DateOnly Fecha { get; set; }
    public TimeOnly HoraInicio { get; set; }
    public TimeOnly HoraTermino { get; set; }

    public string UsuarioId { get; set; }

    public int CanchaId { get; set; }
}
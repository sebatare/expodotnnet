public class CreateReservaDto
{
    public string UsuarioId { get; set; }
    public DateTime Fecha { get; set; }
    public DateTime HoraInicio { get; set; }
    public DateTime HoraTermino { get; set; }
    public int CanchaId { get; set; }
    public int? EquipoId { get; set; }
}
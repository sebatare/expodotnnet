public class CreateAddressDto
{
    public string Calle { get; set; }
    public int Numero { get; set; }
    public string? Otro { get; set; }
    public string Comuna { get; set; }
    public string Ciudad { get; set; }
    public string Pais { get; set; }

    public string? UserId { get; set; }
    public int? CanchaId { get; set; }
}

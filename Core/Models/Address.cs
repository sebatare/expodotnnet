public class Address
{
    public int Id { get; set; }
    public string Calle { get; set; }
    public int Numero { get; set; }

    public string? Otro { get; set; }
    public string? Comuna { get; set; }
    public string Ciudad { get; set; }
    public string? Pais { get; set; }


    // Clave foránea a User
    public string? UserId { get; set; }
    public virtual User? User { get; set; }

    public virtual Sede Sede { get; set; }

}
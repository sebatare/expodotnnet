using Microsoft.AspNetCore.Identity;

public class User : IdentityUser
{
    public string FirstName { get; set; }
    public string? SecondName { get; set; }
    public string LastName { get; set; }
    public string? SecondLastName { get; set; }

    public int Age { get; set; }
    public DateTime? DateOfBirth { get; set; }

    public string? Rut { get; set; }

    public string? Address { get; set; }

    public int? NumberAddress { get; set; }

    // Permitir que el campo Username sea nulo o vacío
    public override string? UserName { get; set; }


}

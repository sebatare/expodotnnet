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

    public bool Invitado { get; set; } = false;


    // Permitir que el campo Username sea nulo o vacío
    public override string? UserName { get; set; }

    public virtual ICollection<Address> Addresses { get; set; }
    public virtual ICollection<Amistad> Amistades { get; set; }
    public virtual ICollection<Reserva> Reservas { get; set; }

    public virtual ICollection<UserTeam> UserTeams { get; set; }





}

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _configuration;
    private readonly RoleManager<IdentityRole> _roleManager;


    private readonly IEmailService _emailService;

    public UserService(UserManager<User> userManager, IEmailService emailService, IConfiguration configuration, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _emailService = emailService;
        _configuration = configuration;
        _roleManager = roleManager;
    }


    //REGISTRO DE USUARIO
    public async Task<IdentityResult> RegisterUserAsync(RegisterDto model)
    {
        // Verificar si el correo electrónico ya existe
        var existingUser = await _userManager.FindByEmailAsync(model.Email);
        if (existingUser != null)
        {
            // Si el usuario ya existe, devolvemos un resultado de fallo
            return IdentityResult.Failed(new IdentityError { Code = "Correo existente", Description = "El correo electrónico ya está registrado." });
        }

        var user = new User
        {
            Email = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName,
            UserName = model.Email,
            PhoneNumber = model.PhoneNumber
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            //ASIGNACION DE ROL AL CREAR USUARIO
            await _userManager.AddToRoleAsync(user, "Administrador");
            // Si la creación del usuario fue exitosa, devolvemos el resultado con éxito
            return IdentityResult.Success;
        }
        else
        {
            // Si hay errores, los devolvemos
            return result;
        }
    }


    //INICIO DE SESIÓN
    public async Task<(bool Success, string Token)> LoginUserAsync(LoginDto model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
        {
            return (false, null);
        }

        // Genera el token JWT
        var token = await GenerateJwtTokenAsync(user);
        return (true, token);
    }

    //TOKEN DE REINICIO CONTRASEÑA
    public async Task<bool> SendPasswordResetTokenAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
            return false;

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var resetLink = $"https://localhost:5014/reset-password?email={email}&token={Uri.EscapeDataString(token)}";

        var subject = "Restablecimiento de Contraseña";
        var body = $"<p>Haz clic en el enlace para restablecer tu contraseña:</p><a href='{resetLink}'>Restablecer Contraseña</a>";

        await _emailService.SendEmailAsync(email, subject, body);
        return true;
    }


    //REINICIAR CONTRASEÑA
    public async Task<IdentityResult> ResetPasswordAsync(string email, string token, string newPassword)
    {
        var user = await _userManager.FindByEmailAsync(email);

        var decodedToken = Uri.UnescapeDataString(token);
        if (user == null)
            return IdentityResult.Failed(new IdentityError { Description = "Usuario no encontrado." });

        return await _userManager.ResetPasswordAsync(user, decodedToken, newPassword);
    }

    //OBTENER DETALLES DE USUARIO
    public async Task<UserDetailsDto> GetUserDetailsAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId); // Usa UserManager para buscar el usuario
        if (user == null) return null;

        // Obtener los roles del usuario
        var roles = await _userManager.GetRolesAsync(user);

        // Mapea los datos a un DTO
        return new UserDetailsDto
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Roles = roles.ToList() // Asignar los roles al DTO
        };
    }

public async Task<UserDetailsDto> GetUserDetailsByEmail(string userEmail)


    {
        var user = await _userManager.FindByEmailAsync(userEmail); // Usa UserManager para buscar el usuario
        if (user == null) return null;

        // Obtener los roles del usuario
        var roles = await _userManager.GetRolesAsync(user);

        // Mapea los datos a un DTO
        return new UserDetailsDto
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Roles = roles.ToList() // Asignar los roles al DTO
        };
    }




    public async Task<List<UserDetailsDto>> GetAllUsersAsync()
    {
        var users = await _userManager.Users.ToListAsync();
        var userDetails = new List<UserDetailsDto>();

        foreach (var user in users)
        {
            // Obtener los roles del usuario
            var roles = await _userManager.GetRolesAsync(user);

            // Crear el DTO con los detalles del usuario
            var userDetail = new UserDetailsDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Roles = roles.ToList() // Asignar los roles del usuario
            };

            // Agregar el DTO a la lista
            userDetails.Add(userDetail);
        }

        return userDetails;
    }



    //GENRADOR DE TOKEN
    public async Task<string> GenerateJwtTokenAsync(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        // Obtén la clave secreta desde la configuración o variables de entorno
        // o, si usas configuración:
        var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);  // Preferiblemente desde appsettings.json

        var claims = new List<Claim>
    {

        //DEFINO LA INFORMACIO QUE VA A LLEVAR MI JWT
        new Claim(ClaimTypes.Name, user.UserName),
        new Claim(ClaimTypes.Email, user.Email),
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        // SE PUEDEN AGREGAR MAS CLAIMS
    };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(24), // Configura la expiración
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = "your_issuer",  // Asegúrate de que estos valores sean consistentes
            Audience = "your_audience"
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }





}

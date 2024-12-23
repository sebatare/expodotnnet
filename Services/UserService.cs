using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _configuration;


    private readonly IEmailService _emailService;

    public UserService(UserManager<User> userManager, IEmailService emailService, IConfiguration configuration)
    {
        _userManager = userManager;
        _emailService = emailService;
        _configuration = configuration;
    }

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
            UserName = model.Email // Usar el correo como el nombre de usuario
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            // Si la creación del usuario fue exitosa, devolvemos el resultado con éxito
            return IdentityResult.Success;
        }
        else
        {
            // Si hay errores, los devolvemos
            return result;
        }
    }

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

    public async Task<IdentityResult> ResetPasswordAsync(string email, string token, string newPassword)
    {
        var user = await _userManager.FindByEmailAsync(email);

        var decodedToken = Uri.UnescapeDataString(token);
        if (user == null)
            return IdentityResult.Failed(new IdentityError { Description = "Usuario no encontrado." });

        return await _userManager.ResetPasswordAsync(user, decodedToken, newPassword);
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
        new Claim(ClaimTypes.Name, user.UserName),
        new Claim(ClaimTypes.Email, user.Email),
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) // Asegúrate de que user.Id sea string
        // Agrega más claims si es necesario
    };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(1), // Configura la expiración
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = "tu_issuer",  // Asegúrate de que estos valores sean consistentes
            Audience = "tu_audience"
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }



}

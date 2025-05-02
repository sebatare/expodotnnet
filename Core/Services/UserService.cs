using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using proyectodotnet.Core.Models;

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
    public async Task<IdentityResult> RegisterUserAsync(UserRegisterDto dto)
    {
        // 1. Validación de email existente
        if (string.IsNullOrEmpty(dto.Email))
        {
            return IdentityResult.Failed(new IdentityError
            {
                Code = "EmailRequired",
                Description = "El correo electrónico es obligatorio."
            });
        }

        var existingUser = await _userManager.FindByEmailAsync(dto.Email);
        if (existingUser != null)
        {
            return IdentityResult.Failed(new IdentityError
            {
                Code = "DuplicateEmail",
                Description = "El correo electrónico ya está registrado."
            });
        }

        // 2. Creación del usuario
        var user = new User
        {
            Email = dto.Email,
            UserName = dto.Email,  // Asegurar que UserName no sea null
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            PhoneNumber = dto.PhoneNumber,
            EmailConfirmed = true  // Opcional: si no requieres confirmación
        };

        // 3. Creación del usuario (con/sin password)
        IdentityResult result;
        if (!string.IsNullOrWhiteSpace(dto.Password))
        {
            result = await _userManager.CreateAsync(user, dto.Password);
        }
        else
        {
            result = await _userManager.CreateAsync(user);
        }

        // 4. Si falla la creación, retornar error inmediatamente
        if (!result.Succeeded)
        {
            return result;
        }

        // 5. Asignación de rol (solo si el usuario se creó correctamente)
        var role = !string.IsNullOrWhiteSpace(dto.Password) ? "Player" : "Guest";

        // Verificar que el rol exista
        if (!await _roleManager.RoleExistsAsync(role))
        {
            await _roleManager.CreateAsync(new IdentityRole(role));
        }

        var roleResult = await _userManager.AddToRoleAsync(user, role);

        // 6. Combinar resultados
        if (!roleResult.Succeeded)
        {
            // Opcional: revertir creación si falla la asignación de rol
            await _userManager.DeleteAsync(user);
            return roleResult;
        }

        return IdentityResult.Success;
    }


    //INICIO DE SESIÓN
    public async Task<(bool Success, string Token)> LoginUserAsync(LoginDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);
        if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
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
            Id = user.Id,
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
            Id = user.Id,
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


    public async Task<List<UserDetailsDto>> GetUsersByTeamMemberDtoAsync(List<TeamMemberDto> dtos)
    {
        var userDetails = new List<UserDetailsDto>();
        Console.WriteLine($"Recibidos {dtos.Count} DTOs de miembros del equipo.");

        foreach (var dto in dtos)
        {
            User? user = null;

            Console.WriteLine($"Buscando usuario por ID: {dto.Id}, Email: {dto.Email}, Teléfono: {dto.PhoneNumber}");

            // Buscar por ID si es un GUID válido
            if (!string.IsNullOrWhiteSpace(dto.Id) && Guid.TryParse(dto.Id, out _))
            {
                user = await _userManager.FindByIdAsync(dto.Id);
            }

            // Buscar por Email
            if (user == null && !string.IsNullOrWhiteSpace(dto.Email))
            {
                user = await _userManager.FindByEmailAsync(dto.Email);
            }

            // Buscar por Teléfono
            if (user == null && !string.IsNullOrWhiteSpace(dto.PhoneNumber))
            {
                user = await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == dto.PhoneNumber);
            }

            // Si no se encuentra, crearlo como invitado
            if (user == null)
            {
                var generatedEmail = dto.Email ?? $"{Guid.NewGuid()}@placeholder.local";
                user = new User
                {
                    FirstName = dto.FirstName ?? "Nombre desconocido",
                    LastName = dto.LastName ?? "Apellido desconocido",
                    Email = dto.Email ?? $"{Guid.NewGuid()}@placeholder.local",
                    PhoneNumber = dto.PhoneNumber,
                    UserName = generatedEmail // ✅ Este campo es obligatorio
                };
                Console.WriteLine("Creando usuario invitado...");
                Console.WriteLine(user);

                var result = await _userManager.CreateAsync(user);
                if (!result.Succeeded)
                {
                    Console.WriteLine($"❌ Error al crear el usuario: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                    continue; // Saltar este usuario si falló la creación
                }

                await _userManager.AddToRoleAsync(user, "Guest");
                Console.WriteLine($"✅ Usuario invitado creado: {user.Email}");
            }

            userDetails.Add(new UserDetailsDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            });
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

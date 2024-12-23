using Microsoft.AspNetCore.Mvc;

public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }


    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _userService.RegisterUserAsync(model);

        if (result.Succeeded)
        {
            return Ok(new { message = "Usuario registrado correctamente" });
        }

        return BadRequest(result.Errors);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var (success, token) = await _userService.LoginUserAsync(model);
        if (success)
        {
            return Ok(new { Token = token, Message = "Inicio de sesión exitoso" });
        }

        return Unauthorized(new { Message = "Correo o contraseña incorrectos" });
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        return Ok(new { Message = "No se requiere lógica del lado del servidor para cerrar sesión con JWT." });
    }


    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto model)
    {
        if (string.IsNullOrEmpty(model.Email))
            return BadRequest("El correo es requerido.");

        var result = await _userService.SendPasswordResetTokenAsync(model.Email);

        if (!result)
            return NotFound(new { message = "Usuario no encontrado." });

        return Ok(new { message = "Correo enviado exitosamente." });
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var decodedToken = Uri.UnescapeDataString(model.Token);

        var result = await _userService.ResetPasswordAsync(model.Email, model.Token, model.NewPassword);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        return Ok(new { message = "Contraseña restablecida exitosamente." });
    }



}
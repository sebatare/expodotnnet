using Microsoft.AspNetCore.Identity;

public interface IUserService
{
    Task<IdentityResult> RegisterUserAsync(RegisterDto model);
     Task<(bool Success, string Token)> LoginUserAsync(LoginDto model);
     //Task<IdentityResult> LogoutAsync();
     Task<bool> SendPasswordResetTokenAsync(string email);
    Task<IdentityResult> ResetPasswordAsync(string email, string token, string newPassword);
    Task<UserDetailsDto> GetUserDetailsAsync(string userId);

    Task<List<UserDetailsDto>> GetAllUsersAsync();
    

}
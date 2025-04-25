using Microsoft.AspNetCore.Identity;

public interface IUserService
{
    Task<IdentityResult> RegisterUserAsync(UserRegisterDto model);
    Task<(bool Success, string Token)> LoginUserAsync(LoginDto model);
    //Task<IdentityResult> LogoutAsync();
    Task<bool> SendPasswordResetTokenAsync(string email);
    Task<IdentityResult> ResetPasswordAsync(string email, string token, string newPassword);
    Task<UserDetailsDto> GetUserDetailsAsync(string userId);

    Task<List<UserDetailsDto>> GetAllUsersAsync();
    Task<UserDetailsDto> GetUserDetailsByEmail(string userEmail);

    Task<List<UserDetailsDto>> GetUsersByTeamMemberDtoAsync(List<TeamMemberDto> dtos);



}
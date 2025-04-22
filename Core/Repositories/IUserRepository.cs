
public interface IUserRepository
{
    Task<User?> GetUserByEmailAsync(string email);
    Task<User?> GetUserByPhoneNumberAsync(string phoneNumber);
    Task<List<UserDetailsDto>> GetUsersByIdentifiersAsync(List<string> identifiers);

    Task<bool> UserExistsAsync(string email, string phoneNumber);
    Task<bool> UserExistsAsync(string email, string phoneNumber, string idToExclude);
}
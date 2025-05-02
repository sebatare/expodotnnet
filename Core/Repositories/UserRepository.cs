using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using proyectodotnet.Core.Models;
using proyectodotnet.Data;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<User> _userManager;

    public UserRepository(ApplicationDbContext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetUserByPhoneNumberAsync(string phoneNumber)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber);
    }

    public async Task<List<UserDetailsDto>> GetUsersByIdentifiersAsync(List<string> identifiers)
    {
        var users = await _userManager.Users
            .Where(u => identifiers.Contains(u.Id))
            .ToListAsync();

        var result = new List<UserDetailsDto>();

        foreach (var user in users)
        {
            var roles = await _userManager.GetRolesAsync(user); // gracias al UserManager

            result.Add(new UserDetailsDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Roles = roles.ToList()
            });
        }

        return result;
    }

    public async Task<bool> UserExistsAsync(string email, string phoneNumber)
    {
        return await _context.Users
            .AnyAsync(u => u.Email == email || u.PhoneNumber == phoneNumber);
    }

    public async Task<bool> UserExistsAsync(string email, string phoneNumber, string idToExclude)
    {
        return await _context.Users
            .AnyAsync(u => (u.Email == email || u.PhoneNumber == phoneNumber) && u.Id != idToExclude);
    }
}
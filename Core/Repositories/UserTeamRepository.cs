using proyectodotnet.Core.Models;
using proyectodotnet.Data;
namespace proyectodotnet.Core.Repositories;

public class UserTeamRepository : IUserTeamRepository
{
    private readonly ApplicationDbContext _context;

    public UserTeamRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(UserTeam userTeam)
    {
        await _context.UserTeams.AddAsync(userTeam);

    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}

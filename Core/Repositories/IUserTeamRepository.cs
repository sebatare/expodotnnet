using proyectodotnet.Core.Models;

public interface IUserTeamRepository
{
    Task AddAsync(UserTeam userTeam);
    Task SaveChangesAsync();
}

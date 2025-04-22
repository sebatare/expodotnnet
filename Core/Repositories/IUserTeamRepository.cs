public interface IUserTeamRepository
{
    Task AddAsync(UserTeam userTeam);
    Task SaveChangesAsync();
}

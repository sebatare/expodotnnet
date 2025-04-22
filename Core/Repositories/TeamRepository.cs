public class TeamRepository : ITeamRepository
{
    private readonly ApplicationDbContext _context;

    public TeamRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Team> AddAsync(Team team)
    {
        await _context.Teams.AddAsync(team);  // Agregar el equipo al contexto
        return team;
    }

    // Puedes exponer SaveChangesAsync si lo necesitas
    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();  // Llamar al SaveChangesAsync del DbContext
    }
}
public class TeamService : ITeamService
{
    private readonly ITeamRepository _teamRepository;
    private readonly IUserRepository _userRepository;

    private readonly IUserTeamRepository _userTeamRepository;

    private readonly IUserService _userService;

    public TeamService(ITeamRepository teamRepository, IUserRepository userRepository, IUserTeamRepository userTeamRepository, IUserService userService)
    {
        _userService = userService;
        _userTeamRepository = userTeamRepository;
        _teamRepository = teamRepository;
        _userRepository = userRepository;
        _userTeamRepository = userTeamRepository;
    }

    public async Task<RegisterTeamDto> RegisterTeamAsync(RegisterTeamDto dto)
    {
        var usuarios = await _userRepository.GetUsersByIdentifiersAsync(dto.MiembrosIds);
        Console.WriteLine($"Usuarios encontrados: {usuarios.Count}");  // Verifica cuántos usuarios se encontraron
        var capitan = await _userService.GetUserDetailsAsync(dto.CapitanId);  // Cambiado a GetUserById para obtener el capitán por ID

        if (capitan == null)
            throw new Exception("El capitán no fue encontrado entre los usuarios");
        var nombreEquipo = dto.Nombre ?? $"{capitan.FirstName} {capitan.LastName} y amigotes";

        var equipo = new Team
        {
            Nombre = nombreEquipo,
            FechaCreacion = DateTime.UtcNow,
            ClubId = dto.ClubId,
            CapitanId = capitan.Id
        };

        // Primero guarda el equipo para que se genere el ID
        await _teamRepository.AddAsync(equipo);
        await _teamRepository.SaveChangesAsync();  // Guarda el equipo en la base de datos

        Console.WriteLine($"Equipo creado con ID: {equipo.Id}");  // Verifica que el ID se haya generado correctamente

        // Ahora que el equipo tiene un ID, podemos asignarlo a los UserTeam
        var usuarioEquipos = usuarios.Select(u => new UserTeam
        {
            UserId = u.Id,
            TeamId = equipo.Id,  // Ahora el ID del equipo está disponible
            Confirmado = false,  // Asignar Confirmado a true por defecto
        }).ToList();

        equipo.UserTeams = usuarioEquipos;

        // Luego guarda la relación entre usuario y equipo
        foreach (var usuario in usuarioEquipos)
        {
            await _userTeamRepository.AddAsync(usuario);
        }
        await _userTeamRepository.SaveChangesAsync();  // Guardar las relaciones

        return new RegisterTeamDto
        {
            Nombre = equipo.Nombre,
            ClubId = equipo.ClubId,
            MiembrosIds = equipo.UserTeams.Select(ut => ut.UserId).ToList(),
            CapitanId = equipo.CapitanId,
            FechaCreacion = equipo.FechaCreacion,
        };
    }



}

using proyectodotnet.Core.Models;

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

    public async Task<TeamRegisterDto> RegisterTeamAsync(TeamRegisterDto dto)
    {
        Console.WriteLine("Iniciando el registro del equipo...");
        Console.WriteLine($"Miembros recibidos: {string.Join(", ", dto.Miembros.Select(m => m.FirstName))}");

        // Buscar o registrar usuarios (invitados o existentes)
        var usuarios = await _userService.GetUsersByTeamMemberDtoAsync(dto.Miembros);
        Console.WriteLine($"Usuarios encontrados o creados: {usuarios.Count}");

        // Obtener información del capitán
        var capitan = await _userService.GetUserDetailsAsync(dto.CapitanId);
        if (capitan == null)
            throw new Exception("El capitán no fue encontrado entre los usuarios");

        // Nombre por defecto si no se especifica
        var nombreEquipo = dto.Nombre ?? $"{capitan.FirstName} {capitan.LastName} y amigotes";

        // Crear entidad del equipo
        var equipo = new Team
        {
            Nombre = nombreEquipo,
            FechaCreacion = DateTime.UtcNow,
            ClubId = dto.ClubId,
            CapitanId = capitan.Id
        };

        // Guardar equipo para obtener ID
        await _teamRepository.AddAsync(equipo);
        await _teamRepository.SaveChangesAsync();
        Console.WriteLine($"Equipo creado con ID: {equipo.Id}");

        // Crear relaciones Usuario-Equipo
        var usuarioEquipos = usuarios.Select(u => new UserTeam
        {
            UserId = u.Id,
            TeamId = equipo.Id,
            Confirmado = false
        }).ToList();

        equipo.UserTeams = usuarioEquipos;

        // Guardar relaciones UserTeam
        foreach (var usuario in usuarioEquipos)
        {
            await _userTeamRepository.AddAsync(usuario);
        }
        await _userTeamRepository.SaveChangesAsync();

        // Combinar UserTeam con info de usuario para poblar correctamente los miembros
        var miembrosDto = equipo.UserTeams
            .Join(usuarios,
                ut => ut.UserId,
                u => u.Id,
                (ut, u) => new TeamMemberDto
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    Confirmed = ut.Confirmado
                })
            .ToList();

        // Retornar DTO con datos completos del equipo
        Console.WriteLine("Registro de equipo completado.");
        return new TeamRegisterDto
        {   
            Id = equipo.Id,
            Nombre = equipo.Nombre,
            ClubId = equipo.ClubId,
            CapitanId = equipo.CapitanId,
            FechaCreacion = equipo.FechaCreacion,
            Miembros = miembrosDto
        };
    }




}

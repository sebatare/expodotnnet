public class ReservaService : IReservaService
{
    private readonly IReservaRepository _reservaRepository;
    private readonly ICanchaService _canchaService;

    //private readonly IEquipoRepository _equipoRepository;

    private readonly IUserService _userService;

    public ReservaService(IReservaRepository reservaRepository, ICanchaService canchaService, IUserService userService)
    {
        _reservaRepository = reservaRepository;
        _canchaService = canchaService;
        //_equipoRepository = equipoRepository;
        _userService = userService;
    }

    public async Task<CreateReservaDto> CreateReservaAsync(CreateReservaDto newReserva, string userId)
    {


        var user = await _userService.GetUserDetailsAsync(userId);
        if (user == null)
        {
            throw new Exception("User not found");
        }
        newReserva.UsuarioId = userId;

        var disponible = await _reservaRepository.CanchaDisponible(newReserva.CanchaId, newReserva.Fecha, newReserva.HoraInicio);

        if (!disponible)
        {
            throw new ReservaNoDisponibleException();
        }
        var reserva = new Reserva
        {
            FechaCreacion = DateTime.Now, // Fecha completa
            Fecha = newReserva.Fecha, // Hora de inicio
            HoraInicio = newReserva.HoraInicio, // Hora de término
            HoraTermino = newReserva.HoraTermino, // ID del usuario
            UsuarioId = userId, // ID del usuario
            CanchaId = 8 // ID de la cancha
        };
        await _reservaRepository.CreateReservaAsync(reserva);
        return newReserva;
    }
}
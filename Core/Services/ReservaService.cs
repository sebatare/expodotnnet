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
        Console.WriteLine("Iniciando el proceso de servicio de reserva...");

        var user = await _userService.GetUserDetailsAsync(userId);
        if (user == null)
        {
            throw new Exception("User not found");
        }

        var disponible = await _reservaRepository.CanchaDisponible(newReserva.CanchaId, newReserva.Fecha, newReserva.HoraInicio);

        if (!disponible)
        {
            // Obtener detalles de la reserva conflictiva
            var reservaExistente = await _reservaRepository.ObtenerReservaConflictiva(newReserva.CanchaId, newReserva.Fecha, newReserva.HoraInicio);
            throw new ReservaNoDisponibleException(
                reservaExistente.CanchaId,
                reservaExistente.Fecha,
                reservaExistente.HoraInicio,
                reservaExistente.HoraTermino
            );
        }

        var reserva = new Reserva
        {
            FechaCreacion = DateTime.Now,
            Fecha = newReserva.Fecha,
            HoraInicio = newReserva.HoraInicio,
            HoraTermino = newReserva.HoraTermino,
            UsuarioId = userId,
            CanchaId = newReserva.CanchaId,
            TeamId = newReserva.EquipoId
        };

        await _reservaRepository.CreateReservaAsync(reserva);
        return newReserva;
    }
}
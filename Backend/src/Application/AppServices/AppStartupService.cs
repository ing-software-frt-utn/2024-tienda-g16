using Domain.Abstractions;
using Domain.Aggregates.TiendaAggregate;
using Domain.RepositoriesContracts;

namespace Application.AppServices;

public class AppStartupService
{
    public static bool IsUp { get; private set; }
    private readonly ITiendaRepository _tiendaRepository;
    private readonly ISesionRepository _sesionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AppStartupService(
        ITiendaRepository tiendaRepository,
        ISesionRepository sesionRepository,
        IUnitOfWork unitOfWork)
    {
        _tiendaRepository = tiendaRepository;
        _sesionRepository = sesionRepository;
        _unitOfWork = unitOfWork;
    }

    public void Startup()
    {
        LoadSingletons();
        LogoutRemainingSessions();
        IsUp = true;
    }

    private void LoadSingletons()
    {
        Tienda.Instance.SetTienda(_tiendaRepository.GetTienda()!);
    }
    private void LogoutRemainingSessions()
    {
        _sesionRepository.RemoveAll();
        _unitOfWork.SaveChanges();
    }
}
